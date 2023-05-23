using FeignCore.Apis;
using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.DbModel;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Request;
using JwSale.Model.Dto.Service;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Options;
using JwSale.Util;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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

        private readonly IUserService _userService;

        public WechatController(IWxMiniProgram wxMiniProgram,
            IOptions<JwSaleOptions> jwSaleOptions, IDistributedCache cache, IUserService userService)
        {
            _wxMiniProgram = wxMiniProgram;
            _jwSaleOptions = jwSaleOptions.Value;
            _cache = cache;
            _userService = userService;


        }

        /// <summary>
        /// 微信小程序授权登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [MoudleInfo("微信小程序授权登录", false)]
        [HttpPost("api/wechat/login")]
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


                WechatLoginCache wxLoginCache = new WechatLoginCache();
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
        /// 绑定微信小程序
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("绑定微信小程序", false)]
        [HttpPost("api/wechat/bindwechatuser")]
        [WechatAuthRequired]
        public async Task<ActionResult<ResponseBase>> BindWechatUser(BindWechatUserRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();


            var wxLoginCacheStr = await _cache.GetStringAsync(CacheKeyHelper.GetWxLoginTokenKey(HttpContext.WechatOpenId()));

            var wxLoginCache = wxLoginCacheStr?.ToObj<WechatLoginCache>();
            if (wxLoginCache == null)
            {
                response.Message = "绑定失败";
                response.Code = HttpStatusCode.BadRequest;
                response.Success = false;
            }
            else
            {
                var userInfo = await FreeSql.Select<UserInfo>().Where(o => o.Phone == request.PhoneNumer).ToOneAsync();
                if (userInfo != null)
                {
                    bool isExsitWechatUser = await FreeSql.Select<WechatUser>().Where(o => o.OpenId == HttpContext.WechatOpenId() && o.UserId == userInfo.Id).AnyAsync();
                    if (!isExsitWechatUser)
                    {
                        WechatUserInfo wechatUserInfo = new WechatUserInfo()
                        {
                            Id = Guid.NewGuid().ToString(),
                            OpenId = HttpContext.WechatOpenId(),
                            NickName = request.NickName,
                            UnionId = wxLoginCache.UnionId,
                            UserId = userInfo.Id
                        };
                        wechatUserInfo.InitAddBaseEntityData();
                        int count = await FreeSql.Insert(wechatUserInfo).ExecuteAffrowsAsync();
                    }
                    else
                    {
                        Logger.LogInformation($"微信Openid:【{HttpContext.WechatOpenId()}】已绑定用户【{userInfo.Id}】小程序");
                    }

                }
                else
                {
                    FreeSql.Transaction(() =>
                    {
                        userInfo = new UserInfo()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = request.PhoneNumer,
                            Password = DefaultPassword.PASSWORD.ToMd5(),
                            RealName = request.PhoneNumer,
                            RealNamePin = request.PhoneNumer.ToPinYin(),
                            Phone = request.PhoneNumer,
                            HeadImageUrl = request.HeadImageUrl,
                            Status = 0,
                            Type = 1,
                            ExpiredTime = DateTime.Now.AddYears(1),

                        };
                        userInfo.InitAddBaseEntityData();
                        int count = FreeSql.Insert<UserInfo>(userInfo).ExecuteAffrows();

                        WechatUserInfo wechatUserInfo = new WechatUserInfo()
                        {
                            Id = Guid.NewGuid().ToString(),
                            OpenId = HttpContext.WechatOpenId(),
                            NickName = request.NickName,
                            UnionId = wxLoginCache.UnionId
                        };
                        wechatUserInfo.InitAddBaseEntityData();
                        count = FreeSql.Insert(wechatUserInfo).ExecuteAffrows();
                    });

                }
                await _cache.RemoveAsync(CacheKeyHelper.GetWxLoginTokenKey(HttpContext.WechatOpenId()));
            }


            return response;


        }




        /// <summary>
        /// 解绑微信小程序
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("解绑微信小程序", false)]
        [HttpPost("api/wechat/unbindwechatuser")]
        public async Task<ActionResult<ResponseBase>> UnBindWechatUser(UnBindWechatUserRequest request)
        {
            ResponseBase response = new ResponseBase();
            var wechatUserInfo = await FreeSql.Select<WechatUserInfo>().Where(o => o.OpenId == HttpContext.WechatOpenId()).ToOneAsync();
            if (wechatUserInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "小程序用户不存在";
            }
            else
            {
                int count = await FreeSql.Delete<WechatUserCache>(wechatUserInfo.Id).ExecuteAffrowsAsync();
            }
            return await response.ToJsonResultAsync();
        }

    }
}
