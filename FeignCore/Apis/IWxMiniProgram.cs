using FeignCore.Actions;
using FeignCore.Filters;
using JwSale.Model.Dto.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;

namespace FeignCore.Apis
{
    [HttpHost("https://api.weixin.qq.com")]
    [Service("wechat")]
    [Log]
    public interface IWxMiniProgram : IHttpApi
    {
        [HttpGet("/sns/jscode2session")]
        [JsonReturn]
        ITask<WxMiniProgramLoginResp> Login(string appid,string secret,string js_code,string grant_type= "authorization_code");
    }



    public class WxMiniProgramLoginResp
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
        [JsonProperty("errcode")]
        public int ErrCode { get; set; }
        /// <summary>
        /// 错误码	错误码取值	解决方案
        /// 40029	code 无效 js_code无效
        /// 45011	api minute-quota reach limit mustslower  retry next minute API 调用太频繁，请稍候再试
        /// 40226	code blocked    高风险等级用户，小程序登录拦截 。风险等级详见用户安全解方案
        /// -1	system error    系统繁忙，此时请开发者稍候再试
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrMsg { get; set; }
    }





}
