using FeignCore.Apis;
using JwSale.Api.Filters;
using JwSale.Api.Util;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Request.Wechat;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 微信小程序
    /// </summary>
    [MoudleInfo("微信小程序", 1)]
    [NoAuthRequired]
    public class WechatController : JwSaleControllerBase
    {
        private readonly IWxMiniProgram _wxMiniProgram;
        private readonly JwSaleOptions _jwSaleOptions;
        private readonly IDistributedCache _cache;
        public WechatController(JwSaleDbContext context, IWxMiniProgram wxMiniProgram, IOptions<JwSaleOptions> jwSaleOptions, IDistributedCache cache) : base(context)
        {
            _wxMiniProgram = wxMiniProgram;
            _jwSaleOptions = jwSaleOptions.Value;
            _cache = cache;
        }

        /// <summary>
        /// 微信小程序授权登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [MoudleInfo("微信小程序授权登录", false)]
        [NoAuthRequired]
        [HttpPost("api/Wechat/Login")]
        public async Task<ActionResult<ResponseBase<WxLoginResponse>>> Login(WechatLoginRequest login)
        {
            ResponseBase<WxLoginResponse> response = new ResponseBase<WxLoginResponse>();
            var result = await _wxMiniProgram.Login(login.Code, _jwSaleOptions.WxMiniProgram.AppId, _jwSaleOptions.WxMiniProgram.AppSecret);
            if (result.ErrCode == 0)
            {
                WxLoginResponse wxLoginResponse = new WxLoginResponse();
                wxLoginResponse.OpenId = result.OpenId;
                wxLoginResponse.UnionId = result.UnionId;
                response.Data = wxLoginResponse;


                WxLoginCache wxLoginCache = new WxLoginCache();
                wxLoginCache.OpenId= result.OpenId;
                wxLoginCache.UnionId = result.UnionId;
                wxLoginCache.SessionKey = result.SessionKey;             
                await _cache.SetStringAsync(CacheKeyHelper.GetWxLoginTokenKey(result.OpenId), wxLoginCache.ToJson());
            }
            else
            {
                response.Code = HttpStatusCode.BadRequest;
            }
            response.Message = result.ErrMsg;
            return response;


        }
    }
}
