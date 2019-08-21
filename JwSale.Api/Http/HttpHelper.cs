﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using JwSale.Util.Extensions;
using JwSale.Api.Extensions;

namespace JwSale.Api.Http
{
    public class HttpHelper
    {
        public static async Task<string> PostPacketAsync(string url, string packet)
        {
            var client = CreateHttpClient();
            var content = packet.StrToHexBuffer().ToByteArrayContent();
            var res = await client.PostAsync(url, content);
            var result = await res.Content.ReadAsByteArrayAsync();
            return result.HexBufferToStr();
        }

 

        public static async Task<T> PostAsync<T>(string url, object data)
        {
            var client = CreateHttpClient();
            var content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json");

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


        public static HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }
    }
}
