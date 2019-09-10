using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using JwSale.Util.Extensions;
using JwSale.Api.Extensions;
using JwSale.Model.Dto.Wechat;
using JwSale.Api.Util;
using System.Text.Encodings.Web;
using System.Net;
using JwSale.Model.Dto.Common;
using JwSale.Util.Dependencys;
using Microsoft.Extensions.Caching.Distributed;
using JwSale.Model.Dto.Cache;

namespace JwSale.Api.Http
{
    public class HttpHelper
    {
        public static async Task<string> PostPacketAsync(string url, string packet, ProxyInfo proxyInfo = null)
        {
            var client = CreateHttpClient(proxyInfo);
            var content = packet.StrToHexBuffer().ToByteArrayContent();
            var res = await client.PostAsync(url, content);
            var result = await res.Content.ReadAsByteArrayAsync();
            return result.HexBufferToStr();
        }



        public static async Task<T> PostAsync<T>(string url, object data)
        {
            var client = CreateHttpClient();
            string value = data.ToJson();
            var content = new StringContent(value, Encoding.GetEncoding("gb2312"), "application/json");

            var res = await client.PostAsync(url, content);
            var result = await res.Content.ReadAsStringAsync();
            return result.ToObj<T>();

        }


        public static async Task<T> GetAsync<T>(string url)
        {
            var client = CreateHttpClient();
            var res = await client.GetAsync(url);
            var result = await res.Content.ReadAsStringAsync();
            return result.ToObj<T>();

        }

        public static async Task<T> PostVxApiAsync<T>(string cgiType, WechatResponseBase wechatResponseBase, ProxyInfo proxyInfo = null) where T : new()
        {
            if (string.IsNullOrEmpty(wechatResponseBase.url))
            {
                return new T();
            }
            if (proxyInfo == null)
            {
                var cache = ServiceLocator.Instance.GetService<IDistributedCache>();
                var wechatCacheStr = await cache.GetStringAsync(CacheKeyHelper.GetUserTokenKey(wechatResponseBase.token));
                var wechatCache = wechatCacheStr?.ToObj<WechatCache>();
                proxyInfo = wechatCache?.ProxyInfo;
            }
            var packetResp = await PostPacketAsync(wechatResponseBase.url, wechatResponseBase.packet, proxyInfo);
            AnalysisData analysisData = new AnalysisData(wechatResponseBase.token, packetResp);
            var analysisUrl = WechatHelper.GetUrl(cgiType.ToAnalysis());
            var result = await HttpHelper.PostAsync<T>(analysisUrl, analysisData);
            return result;

        }
        public static HttpClient CreateHttpClient(ProxyInfo proxyInfo = null)
        {
            IWebProxy webProxy = null;
            if (proxyInfo != null && !string.IsNullOrEmpty(proxyInfo.Ip))
            {
                webProxy = new WebProxy(proxyInfo.Ip, proxyInfo.Port);
                if (!string.IsNullOrEmpty(proxyInfo.UserName) && !string.IsNullOrEmpty(proxyInfo.PassWord))
                {
                    webProxy.Credentials = new NetworkCredential(proxyInfo.UserName, proxyInfo.PassWord);
                }



            }

            var handler = new HttpClientHandler() { Proxy = webProxy };
            return new HttpClient(handler);
        }
    }
}
