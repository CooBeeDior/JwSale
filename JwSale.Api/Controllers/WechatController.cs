using FeignCore.Apis;
using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Request.User;
using JwSale.Model.Dto.Request.Wechat;
using JwSale.Model.Dto.Service;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 微信小程序
    /// </summary>

    [MoudleInfo("微信小程序", 1, IsFunction = false)]
    public class WechatController : WechatControllerBase
    {
        private readonly IWxMiniProgram _wxMiniProgram;
        private readonly JwSaleOptions _jwSaleOptions;
        private readonly IDistributedCache _cache;
        private readonly IFreeSql _freeSql;
        private readonly IUserService _userService;
 
        public WechatController(JwSaleDbContext context, IWxMiniProgram wxMiniProgram,
            IOptions<JwSaleOptions> jwSaleOptions, IDistributedCache cache, IFreeSql freeSql, IUserService userService) : base(context)
        {
            _wxMiniProgram = wxMiniProgram;
            _jwSaleOptions = jwSaleOptions.Value;
            _cache = cache;
            _freeSql = freeSql;
            _userService = userService;


        }

        /// <summary>
        /// 微信小程序授权登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [MoudleInfo("微信小程序授权登录", false)]
        [HttpPost("api/Wechat/Login")]
        [WechatNoAuthRequired]
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
                wxLoginCache.OpenId = result.OpenId;
                wxLoginCache.UnionId = result.UnionId;
                wxLoginCache.SessionKey = result.SessionKey;
                await _cache.SetStringAsync(CacheKeyHelper.GetWxLoginTokenKey(result.OpenId), wxLoginCache.ToJson());
                 
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
            }
            response.Message = result.ErrMsg;
            return response;


        }

        /// <summary>
        /// 绑定微信用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("绑定微信用户信息", false)]
        [HttpPost("api/Wechat/BindWechatUser")]
        [WechatAuthRequired]
        public async Task<ActionResult<ResponseBase>> BindWechatUser(BindWechatUserRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();


            var wxLoginCacheStr = await _cache.GetStringAsync(CacheKeyHelper.GetWxLoginTokenKey(HttpContext.WxOpenId()));

            var wxLoginCache = wxLoginCacheStr?.ToObj<WxLoginCache>();
            if (wxLoginCache == null)
            {
                response.Message = "绑定失败";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            else
            {
                BindWechatUser bindWechatUser = new BindWechatUser()
                { 
                    WxNo = request.WxNo,
                    PhoneNumer = request.PhoneNumer,
                    WxOpenId = HttpContext.WxOpenId(),
                    WxUnionId = wxLoginCache.UnionId,
                    HeadImageUrl = request.HeadImageUrl,
                };
                var userId = await _userService.BindWechatUser(bindWechatUser);
                response.Data = userId;

                await _cache.RemoveAsync(CacheKeyHelper.GetWxLoginTokenKey(bindWechatUser.WxOpenId));
            }


            return response;


        }


 


    }
}
