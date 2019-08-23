using JwSale.Api.Const;
using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Http;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Request;
using JwSale.Model.Dto.Request.Wechat;
using JwSale.Model.Dto.Response.Wechat;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 微信管理
    /// </summary>
    [MoudleInfo("微信管理", 1)]
    [NoPermissionRequired]
    public class WechatController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public WechatController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }

        #region 用户
        /// <summary>
        /// 获取登录二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetLoginQrCode")]
        [MoudleInfo("获取二维码")]
        public async Task<ActionResult<ResponseBase>> GetLoginQrCode()
        {
            ResponseBase<GetQrCodeResponse> response = new ResponseBase<GetQrCodeResponse>();
            var request = new WechatCreateRequest();
            var resp = await WechatHelper.WechatCreate(request);
            if (resp.code == "0")
            {
                GetLoginQrCodeRequest getLoginQrCodeRequest = new GetLoginQrCodeRequest()
                {
                    token = resp.token
                };
                string cgiType = CGI_TYPE.CGI_GETLOGINQRCODE;
                var url = WechatHelper.GetUrl(cgiType);
                var wechatResp = await HttpHelper.PostAsync<WechatResponseBase>(url, getLoginQrCodeRequest);
                if (wechatResp.code == "0")
                {
                    var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, wechatResp);
                    if (result.code == "0")
                    {
                        var image = result.message.ToObj<GetLoginQrCodeMsg>();
                        var imgBuffer = image.Image.StrToHexBuffer().ToBase64Img();

                        response.Data = new GetQrCodeResponse()
                        {
                            Base64 = imgBuffer,
                            TempToken = wechatResp.token
                        };

                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"{result.message}{result.describe}";
                    }

                }
                else
                {
                    response.Success = false;
                    response.Message = $"{wechatResp.message}{wechatResp.describe}";
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <param name="checkLoginQrCode"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/CheckLoginQrCode")]
        [MoudleInfo("检查是否登录")]
        public async Task<ActionResult<ResponseBase>> CheckLoginQrCode(CheckLoginQrCodeRequest checkLoginQrCode)
        {
            ResponseTokenBase<CheckLoginQrCodeResponse, ManualAuthResponse> response = new ResponseTokenBase<CheckLoginQrCodeResponse, ManualAuthResponse>();

            var url = WechatHelper.GetUrl(CGI_TYPE.CGI_CHECKLOGINQRCODE);
            var wechatResp = await HttpHelper.PostAsync<WechatResponseBase>(url, checkLoginQrCode);
            if (wechatResp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(CGI_TYPE.CGI_CHECKLOGINQRCODE, wechatResp);

                var resp = result?.message?.ToObj<CheckLoginQrCodeResponse>();
                if (resp.state == 2)
                {
                    WechatAnalysisResponse maResult = null;

                    while (maResult == null || maResult.code == "-301")
                    {
                        ManualAuthRequest manualAuthRequest = new ManualAuthRequest()
                        {
                            token = result.token
                        };
                        string cgiType = CGI_TYPE.CGI_MANUALAUTH;
                        var maUrl = WechatHelper.GetUrl(cgiType);
                        var maRechatResp = await HttpHelper.PostAsync<WechatResponseBase>(maUrl, manualAuthRequest);
                        if (maRechatResp.code == "0")
                        {
                            maResult = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, maRechatResp);
                            if (maResult.code == "0")
                            {
                                var maResp = maResult.message.ToObj<ManualAuthResponse>();
                                response.ExtensionData = maResp;
                                response.Token = maResult.token;


                                //缓存     
                                WechatCache wechatCache = new WechatCache()
                                {
                                    Token = maRechatResp.token,
                                    CheckLoginQrCode = resp,
                                    ManualAuth = maResp
                                };
                                await cache.SetStringAsync(CacheKeyHelper.GetWechatKey(maResp.wxid), wechatCache.ToJson());




                            }
                            else if (maResult.code == "-301")
                            { }
                            else
                            {
                                response.Success = false;
                                response.Message = $"{maResult.message}{maResult.describe}";
                            }
                        }
                        else
                        {
                            response.Success = false;
                            response.Message = $"{maResult.message}{maResult.describe}";
                        }

                    }
                }
                response.Data = resp;
            }
            else
            {
                response.Success = false;
                response.Message = $"{wechatResp.message}{wechatResp.describe}";
            }

            return response;

        }

        /// <summary>
        /// 62或A16登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/Login")]
        [MoudleInfo("62或A16登录")]
        public async Task<ActionResult<ResponseBase>> Login(WechatCreateRequest login)
        {
            ResponseTokenBase<CheckLoginQrCodeResponse, ManualAuthResponse> response = new ResponseTokenBase<CheckLoginQrCodeResponse, ManualAuthResponse>();

            var resp = await WechatHelper.WechatCreate(login);
            if (resp.code == "0")
            {
                WechatAnalysisResponse maResult = null;

                while (maResult == null || maResult.code == "-301")
                {
                    ManualAuthRequest manualAuthRequest = new ManualAuthRequest()
                    {
                        token = resp.token
                    };
                    string cgiType = CGI_TYPE.CGI_MANUALAUTH;
                    var maUrl = WechatHelper.GetUrl(cgiType);
                    var maRechatResp = await HttpHelper.PostAsync<WechatResponseBase>(maUrl, manualAuthRequest);
                    if (maRechatResp.code == "0")
                    {
                        maResult = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, maRechatResp);
                        if (maResult.code == "0")
                        {
                            var maResp = maResult.message.ToObj<ManualAuthResponse>();
                            response.ExtensionData = maResp;
                            response.Token = maRechatResp.token;


                            //缓存   
                            WechatCache wechatCache = new WechatCache()
                            {
                                Token = maRechatResp.token,
                                CheckLoginQrCode = new CheckLoginQrCodeResponse()
                                {
                                    wxid = maResp.wxid,
                                    nickName = maResp.nickName,
                                },
                                ManualAuth = maResp
                            };
                            await cache.SetStringAsync(CacheKeyHelper.GetWechatKey(maResp.wxid), wechatCache.ToJson());

                            //刷新数据

                        }
                        else if (maResult.code == "-301")
                        { }
                        else
                        {
                            response.Success = false;
                            response.Message = $"{maResult.message}{maResult.describe}";
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"{maRechatResp.message}{maRechatResp.describe}";
                    }
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }




        /// <summary>
        /// 二次登录
        /// </summary>
        /// <param name="autoAuth"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/AutoAuth")]
        [MoudleInfo("二次登录")]
        public async Task<ActionResult<ResponseBase>> AutoAuth(AutoAuthRequest autoAuth)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_AUTOAUTH;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, autoAuth);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="logOut"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/LogOut")]
        [MoudleInfo("退出登录")]
        public async Task<ActionResult<ResponseBase>> LogOut(LogOutRequest logOut)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_LOGOUT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, logOut);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }



        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="heartBeat"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/HeartBeat")]
        [MoudleInfo("心跳")]
        public async Task<ActionResult<ResponseBase>> HeartBeat(HeartBeatRequest heartBeat)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_HEARTBEAT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, heartBeat);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }

        /// <summary>
        /// 获取微信或群二维码
        /// </summary>
        /// <param name="getQrCode"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetQrCode")]
        [MoudleInfo("获取微信或群二维码")]
        public async Task<ActionResult<ResponseBase>> GetQrCode(GetQrCodeRequest getQrCode)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_GETQRCODE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getQrCode);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }





        /// <summary>
        /// 设置微信号
        /// </summary>
        /// <param name="generalSet"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GeneralSet")]
        [MoudleInfo("设置微信号")]
        public async Task<ActionResult<ResponseBase>> GeneralSet(GeneralSetRequest generalSet)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_GENERALSET;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, generalSet);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="uploadhdHeadImg"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/UploadhdHeadImg")]
        [MoudleInfo("设置头像")]
        public async Task<ActionResult<ResponseBase>> UploadhdHeadImg(UploadhdHeadImgRequest uploadhdHeadImg)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADHDHEADIMG;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadhdHeadImg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 设置昵称
        /// </summary>
        /// <param name="setNickName"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SetNickName")]
        [MoudleInfo("修改昵称")]
        public async Task<ActionResult<ResponseBase>> SetNickName(SetNickNameRequest setNickName)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_OPLOG;
            var url = WechatHelper.GetUrl(cgiType);

            WechatNickName wechatNickName = new WechatNickName()
            {
                bitFlag = "1",
                str = setNickName.NickName
            };

            OpLogRequest opLogRequest = new OpLogRequest()
            {
                cmdid = "64",
                cmdbuf = wechatNickName.ToJson(),
                token = setNickName.Token
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, opLogRequest);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }



        /// <summary>
        /// 修改资料     
        /// </summary>
        /// <param name="setProfile"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SetProfile")]
        [MoudleInfo("修改资料")]
        public async Task<ActionResult<ResponseBase>> SetProfile(SetProfileRequest setProfile)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_OPLOG;
            var url = WechatHelper.GetUrl(cgiType);

            WechatProfile wechatProfile = new WechatProfile()
            {
                bitFlag = "2178",
                sex = setProfile.sex,
                province = setProfile.province,
                city = setProfile.city,
                nickName = setProfile.nickName,
                country = setProfile.country,
                signature = setProfile.signature
            };

            OpLogRequest opLogRequest = new OpLogRequest()
            {
                cmdid = "1",
                cmdbuf = wechatProfile.ToJson(),
                token = setProfile.Token
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, opLogRequest);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="newVerifyPasswd"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/NewVerifyPasswd")]
        [MoudleInfo("确认密码")]
        public async Task<ActionResult<ResponseBase>> NewVerifyPasswd(NewVerifyPasswdRequest newVerifyPasswd)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_NEWVERIFYPASSWD;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newVerifyPasswd);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newSetPasswd"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/NewSetPasswd")]
        [MoudleInfo("修改密码")]
        public async Task<ActionResult<ResponseBase>> NewSetPasswd(NewSetPasswdRequest newSetPasswd)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_NEWSETPASSWD;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSetPasswd);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }



        /// <summary>
        /// 同步消息
        /// </summary>
        /// <param name="newSync"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/NewSync")]
        [MoudleInfo("同步消息")]
        public async Task<ActionResult<ResponseBase>> NewSync(NewSyncRequest newSync)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_NEWSYNC;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSync);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 同步消息标识
        /// </summary>
        /// <param name="newSyncKey"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/NewSyncKey")]
        [MoudleInfo("同步消息标识")]
        public async Task<ActionResult<ResponseBase>> NewSyncKey(NewSyncKeyRequest newSyncKey)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.SET_NEWSYNCKEY;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSyncKey);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 扫码登录设备请求
        /// </summary>
        /// <param name="extdeviceLoginconfirmGet"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ExtdeviceLoginconfirmGet")]
        [MoudleInfo("扫码登录设备请求")]
        public async Task<ActionResult<ResponseBase>> ExtdeviceLoginconfirmGet(ExtdeviceLoginconfirmGetRequest extdeviceLoginconfirmGet)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_EXTDEVICE_LOGINCONFIRM_GET;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, extdeviceLoginconfirmGet);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 确认登录设备请求
        /// </summary>
        /// <param name="extdeviceLoginconfirmOk"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ExtdeviceLoginconfirmOk")]
        [MoudleInfo("确认登录设备请求")]
        public async Task<ActionResult<ResponseBase>> ExtdeviceLoginconfirmOk(ExtdeviceLoginconfirmOkRequest extdeviceLoginconfirmOk)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_EXTDEVICE_LOGINCONFIRM_OK;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, extdeviceLoginconfirmOk);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        #endregion

        #region 好友
        /// <summary>
        /// 获取联系人(通讯录群友)
        /// </summary>
        /// <param name="getContact"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetContact")]
        [MoudleInfo("获取联系人")]
        public async Task<ActionResult<ResponseBase>> GetContract(GetContactRequest getContact)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_GETCONTACT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getContact);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }



        /// <summary>
        /// 搜索联系人(非好友)
        /// </summary>
        /// <param name="searchContact"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SearchContact")]
        [MoudleInfo("搜索联系人")]
        public async Task<ActionResult<ResponseBase>> SearchContact(SearchContactRequest searchContact)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_SEARCHCONTACT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, searchContact);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 添加或同意联系人
        /// </summary>
        /// <param name="verifyUser"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/VerifyUser")]
        [MoudleInfo("添加或同意联系人")]
        public async Task<ActionResult<ResponseBase>> VerifyUser(VerifyUserRequest verifyUser)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_VERIFYUSER;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, verifyUser);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="delContact"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/DelContact")]
        [MoudleInfo("删除联系人")]
        public async Task<ActionResult<ResponseBase>> DelContact(DelContactRequest delContact)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_DELCONTACT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, delContact);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 修改好友备注
        /// </summary>
        /// <param name="setFriendRemarks"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SetFriendRemarks")]
        [MoudleInfo("修改好友备注")]
        public async Task<ActionResult<ResponseBase>> SetFriendRemarks(SetFriendRemarksRequest setFriendRemarks)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_SETFRIENDREMARKS;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, setFriendRemarks);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }
        #endregion

        #region 群
        /// <summary>
        /// 扫码进群
        /// </summary>
        /// <param name="scanIntoChatRoom"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ScanIntoChatRoom")]
        [MoudleInfo("扫码进群")]
        public async Task<ActionResult<ResponseBase>> ScanIntoChatRoom(ScanIntoChatRoomRequest scanIntoChatRoom)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_A8KEY;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, scanIntoChatRoom);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 创建群
        /// </summary>
        /// <param name="createChatRoom"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/CreateChatRoom")]
        [MoudleInfo("创建群")]
        public async Task<ActionResult<ResponseBase>> CreateChatRoom(CreateChatRoomRequest createChatRoom)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_CREATECHATROOM;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, createChatRoom);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 群成员列表
        /// </summary>
        /// <param name="memberDetail"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MemberDetail")]
        [MoudleInfo("群成员列表")]
        public async Task<ActionResult<ResponseBase>> MemberDetail(MemberDetailRequest memberDetail)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MEMBERDETAIL;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, memberDetail);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 添加群成员（40内）
        /// </summary>
        /// <param name="addChatRoomMember"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/AddChatRoomMember")]
        [MoudleInfo("添加群成员")]
        public async Task<ActionResult<ResponseBase>> AddChatRoomMember(AddChatRoomMemberRequest addChatRoomMember)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_ADDCHATROOMMEMBER;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, addChatRoomMember);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 邀请群成员（40外）
        /// </summary>
        /// <param name="inviteChatRoomMember"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/InviteChatRoomMember")]
        [MoudleInfo("邀请群成员")]
        public async Task<ActionResult<ResponseBase>> InviteChatRoomMember(InviteChatRoomMemberRequest inviteChatRoomMember)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_INVITECHATROOMMEMBER;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, inviteChatRoomMember);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 转让群主
        /// </summary>
        /// <param name="transferChatRoomOwner"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/TransferChatRoomOwner")]
        [MoudleInfo("转让群主")]
        public async Task<ActionResult<ResponseBase>> TransferChatRoomOwner(TransferChatRoomOwnerRequest transferChatRoomOwner)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_TRANSFERCHATROOMOWNER;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, transferChatRoomOwner);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 退群
        /// </summary>
        /// <param name="quitChatRoom"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/QuitChatRoom")]
        [MoudleInfo("退群")]
        public async Task<ActionResult<ResponseBase>> QuitChatRoom(QuitChatRoomRequest quitChatRoom)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_QUITCHATROOM;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, quitChatRoom);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 删除群成员
        /// </summary>
        /// <param name="delChatRoomMember"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/DelChatRoomMember")]
        [MoudleInfo("删除群成员")]
        public async Task<ActionResult<ResponseBase>> DelChatRoomMember(DelChatRoomMemberRequest delChatRoomMember)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_DELCHATROOMMEMBER;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, delChatRoomMember);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }
        #endregion

        #region 消息

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="newSendMsg"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/NewSendMsg")]
        [MoudleInfo("发送文本消息")]
        public async Task<ActionResult<ResponseBase>> NewSendMsg(NewSendMsgRequest newSendMsg)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_NEWSENDMSG;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSendMsg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="uploadMsgImg"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendImgMsg")]
        [MoudleInfo("发送图片消息")]
        public async Task<ActionResult<ResponseBase>> SendImgMsg(UploadMsgImgRequest uploadMsgImg)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADMSGIMG;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadMsgImg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 发送消息CDN图片
        /// </summary>
        /// <param name="uploadMsgImgCdn"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendImgMsgCdn")]
        [MoudleInfo("发送消息图片CDN")]
        public async Task<ActionResult<ResponseBase>> SendImgMsgCdn(UploadMsgImgCdnRequest uploadMsgImgCdn)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADMSGIMGCDN;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadMsgImgCdn);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="uploadVoice"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SemdVoiceMsg")]
        [MoudleInfo("发送语音消息")]
        public async Task<ActionResult<ResponseBase>> SemdVoiceMsg(UploadVoiceRequest uploadVoice)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADVOICE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadVoice);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="uploadVideo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendViedoMsg")]
        [MoudleInfo("发送视频消息")]
        public async Task<ActionResult<ResponseBase>> SendViedoMsg(UploadVideoRequest uploadVideo)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADVIDEO;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadVideo);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送App消息
        /// </summary>
        /// <param name="appMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendAppMsg")]
        [MoudleInfo("发送App消息")]
        public async Task<ActionResult<ResponseBase>> SendAppMsg(AppMessageRequest appMessage)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_SENDAPPMSG;
            var url = WechatHelper.GetUrl(cgiType);

            string dataUrl = string.IsNullOrEmpty(appMessage.DataUrl) ? appMessage.Url : appMessage.DataUrl;
            string content = $"<appmsg appid=\"{appMessage.AppId}\" sdkver=\"0\"><title>{appMessage.Title}</title><des>{appMessage.Desc}</des><type>{appMessage.Type}</type><showtype>0</showtype><soundtype>0</soundtype><contentattr>0</contentattr><url>{appMessage.Url}</url><lowurl>{appMessage.Url}</lowurl><dataurl>{dataUrl}</dataurl><lowdataurl>{dataUrl}</lowdataurl> <thumburl>{appMessage.ThumbUrl}</thumburl></appmsg>";

            SendAppMsgRequest sendAppMsg = new SendAppMsgRequest()
            {
                recv_uin = appMessage.ToWxId,
                message = content,
                clientmsgid = appMessage.ClientMsgId,
                token = appMessage.Token
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, sendAppMsg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 发送分享消息
        /// </summary>
        /// <param name="appMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendShareMsg")]
        [MoudleInfo("发送分享消息")]
        public async Task<ActionResult<ResponseBase>> SendShareMsg(AppMessageRequest appMessage)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_SENDAPPMSG;
            var url = WechatHelper.GetUrl(cgiType);

            string dataUrl = string.IsNullOrEmpty(appMessage.DataUrl) ? appMessage.Url : appMessage.DataUrl;
            string content = $"<appmsg  sdkver=\"0\"><title>{appMessage.Title}</title><des>{appMessage.Desc}</des><type>{appMessage.Type}</type><showtype>0</showtype><soundtype>0</soundtype><contentattr>0</contentattr><url>{appMessage.Url}</url><lowurl>{appMessage.Url}</lowurl><dataurl>{dataUrl}</dataurl><lowdataurl>{dataUrl}</lowdataurl> <thumburl>{appMessage.ThumbUrl}</thumburl></appmsg>";

            SendShareMsgRequest sendShareMsg = new SendShareMsgRequest()
            {
                recv_uin = appMessage.ToWxId,
                message = content,
                clientmsgid = appMessage.ClientMsgId,
                token = appMessage.Token
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, sendShareMsg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 发送名片消息
        /// </summary>
        /// <param name="cardMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendCardMsg")]
        [MoudleInfo("发送名片消息")]
        public async Task<ActionResult<ResponseBase>> SendCardMsg(CardMessageRequest cardMessage)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_NEWSENDMSG;
            var url = WechatHelper.GetUrl(cgiType);

            cardMessage.CardNickName = string.IsNullOrEmpty(cardMessage.CardNickName) ? cardMessage.CardWxId : cardMessage.CardNickName;
            string content = $"<?xml version=\"1.0\"?>\n<msg bigheadimgurl=\"\" smallheadimgurl=\"\" username=\"{cardMessage.CardWxId}\" nickname=\"{cardMessage.CardNickName}\" fullpy=\"\" shortpy=\"\" alias=\"{cardMessage.CardAlias}\" imagestatus=\"0\" scene=\"17\" province=\"\" city=\"\" sign=\"\" sex=\"2\" certflag=\"0\" certinfo=\"\" brandIconUrl=\"\" brandHomeUrl=\"\" brandSubscriptConfigUrl=\"\" brandFlags=\"0\" regionCode=\"CN\" />\n";

            NewSendMsgRequest newSendMsg = new NewSendMsgRequest()
            {
                recv_uin = cardMessage.ToWxId,
                message_type = "42",
                message = content,
                clientmsgid = cardMessage.ClientMsgId,
                atuserlist = "",
                token = cardMessage.Token
            };
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSendMsg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送位置消息
        /// </summary>
        /// <param name="locationMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendLocationMsg")]
        [MoudleInfo("发送位置消息")]
        public async Task<ActionResult<ResponseBase>> SendLocationMsg(LocationMessageRequest locationMessage)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_NEWSENDMSG;
            var url = WechatHelper.GetUrl(cgiType);
            string content = $"<?xml version=\"1.0\"?>\n<msg>\n\t<location x=\"{locationMessage.Latitude}\" y=\"{locationMessage.Longitude}\" scale=\"16\" label=\"{locationMessage.Name}\" maptype=\"0\" poiname=\"[位置]{locationMessage.Name}\" poiid=\"\" />\n</msg>";

            NewSendMsgRequest newSendMsg = new NewSendMsgRequest()
            {
                recv_uin = locationMessage.ToWxId,
                message_type = "48",
                message = content,
                clientmsgid = locationMessage.ClientMsgId,
                atuserlist = "",
                token = locationMessage.Token
            };
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSendMsg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="mediaMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/SendMediaMsg")]
        [MoudleInfo("发送文件消息")]
        public async Task<ActionResult<ResponseBase>> SendMediaMsg(MediaMessageRequest mediaMessage)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_SENDAPPMSG;
            var url = WechatHelper.GetUrl(cgiType);

            string content = $"<?xml version=\"1.0\"?>\n<appmsg appid='' sdkver=''><title>{mediaMessage.Title}</title><des></des><action></action><type>6</type><content></content><url></url><lowurl></lowurl><appattach><totallen>{mediaMessage.Length}</totallen><attachid>{mediaMessage.AttachId}</attachid><fileext>{mediaMessage.FileExt}</fileext></appattach><extinfo></extinfo></appmsg>";
            SendMediaMsgRequest sendMediaMsg = new SendMediaMsgRequest()
            {
                recv_uin = mediaMessage.ToWxId,
                message = content,
                clientmsgid = mediaMessage.ClientMsgId,
                token = mediaMessage.Token
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, sendMediaMsg);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 消息撤回
        /// </summary>
        /// <param name="revokeMsg"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/RevokeMsg")]
        [MoudleInfo("消息撤回")]
        public async Task<ActionResult<ResponseBase>> RevokeMsg(RevokeMsgRequest revokeMsg)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_REVOKEMSG;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, revokeMsg);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }
        #endregion


        #region 朋友圈

        /// <summary>
        /// 获取朋友圈首页
        /// </summary>
        /// <param name="mmSnsTimeLine"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MmSnsTimeLine")]
        [MoudleInfo("获取朋友圈首页")]
        public async Task<ActionResult<ResponseBase>> MmSnsTimeLine(MmSnsTimeLineRequest mmSnsTimeLine)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MMSNSTIMELINE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, mmSnsTimeLine);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 获取指定朋友圈
        /// </summary>
        /// <param name="mmSnsUserpage"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MmSnsUserpage")]
        [MoudleInfo("获取指定朋友圈")]
        public async Task<ActionResult<ResponseBase>> MmSnsUserpage(MmSnsUserpageRequest mmSnsUserpage)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MMSNSUSERPAGE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, mmSnsUserpage);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 朋友圈点赞评论回复
        /// </summary>
        /// <param name="mmSnsComment"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MmSnsComment")]
        [MoudleInfo("朋友圈点赞评论回复")]
        public async Task<ActionResult<ResponseBase>> MmSnsComment(MmSnsCommentRequest mmSnsComment)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MMSNSCOMMENT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, mmSnsComment);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 朋友圈操作
        /// </summary>
        /// <param name="mmSnsObjectOp"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MmSnsObjectOp")]
        [MoudleInfo("朋友圈操作")]
        public async Task<ActionResult<ResponseBase>> MmSnsObjectOp(MmSnsObjectOpRequest mmSnsObjectOp)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MMSNSOBJECTOP;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, mmSnsObjectOp);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 朋友圈图片上传
        /// </summary>
        /// <param name="mmSnsUpload"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MmSnsUpload")]
        [MoudleInfo("朋友圈图片上传")]
        public async Task<ActionResult<ResponseBase>> MmSnsUpload(MmSnsUploadRequest mmSnsUpload)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MMSNSOBJECTOP;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, mmSnsUpload);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送朋友圈
        /// </summary>
        /// <param name="sendSns"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/MmSnsPost")]
        [MoudleInfo("发送朋友圈")]
        public async Task<ActionResult<ResponseBase>> MmSnsPost(SendSnsRequest sendSns)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MMSNSPOST;
            var url = WechatHelper.GetUrl(cgiType);

            string content = null;

            switch (sendSns.Type)
            {
                case 0: content = SendSnsConst.GetContentTemplate(sendSns.WxId, sendSns.Content, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 1: content = SendSnsConst.GetImageTemplate(sendSns.WxId, sendSns.Content, sendSns.MediaInfos, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 2: content = SendSnsConst.GetVideoTemplate(sendSns.WxId, sendSns.Content, sendSns.MediaInfos, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 3: content = SendSnsConst.GetLinkTemplate(sendSns.WxId, sendSns.Content, sendSns.MediaInfos, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 4: content = SendSnsConst.GetImageTemplate3(sendSns.WxId, sendSns.Content, sendSns.MediaInfos, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 5: content = SendSnsConst.GetImageTemplate4(sendSns.WxId, sendSns.Content, sendSns.MediaInfos, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 6: content = SendSnsConst.GetImageTemplate5(sendSns.WxId, sendSns.Content, sendSns.MediaInfos, sendSns.Title, sendSns.ContentUrl, sendSns.Description); break;
                case 7: content = sendSns.Content; break;
            }

            MmSnsPostRequest mmSnsPost = new MmSnsPostRequest()
            {
                token = sendSns.Token,
                clientmsgid = sendSns.ClientMsgId,
                message = content
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, mmSnsPost);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        #endregion

        #region 标签

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="addContactLabel"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/AddContactLabel")]
        [MoudleInfo("添加标签")]
        public async Task<ActionResult<ResponseBase>> AddContactLabel(AddContactLabelRequest addContactLabel)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_ADDCONTACTLABEL;
            var url = WechatHelper.GetUrl(cgiType);

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, addContactLabel);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }


        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="modifyContactLabelList"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ModifyContactLabelList")]
        [MoudleInfo("修改标签")]
        public async Task<ActionResult<ResponseBase>> ModifyContactLabelList(ModifyContactLabelListRequest modifyContactLabelList)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MODIFYCONTACTLABELLIST;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, modifyContactLabelList);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="delContactLabel"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/DelContactLabel")]
        [MoudleInfo("删除标签")]
        public async Task<ActionResult<ResponseBase>> DelContactLabel(DelContactLabelRequest delContactLabel)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_DELCONTACTLABEL;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, delContactLabel);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }

        /// <summary>
        /// 获取标签
        /// </summary>
        /// <param name="getContactLabelList"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetContactLabelList")]
        [MoudleInfo("获取标签")]
        public async Task<ActionResult<ResponseBase>> GetContactLabelList(GetContactLabelListRequest getContactLabelList)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_GETCONTACTLABELLIST;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getContactLabelList);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }
        #endregion


        #region 支付
        /// <summary>
        /// 获取收款码
        /// </summary>
        /// <param name="f2fQrCode"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/F2fQrCode")]
        [MoudleInfo("获取收款码")]
        public async Task<ActionResult<ResponseBase>> F2fQrCode(F2FQrCodeRequest f2fQrCode)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_F2FQRCODE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, f2fQrCode);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }


        /// <summary>
        /// 获取金额收款码
        /// </summary>
        /// <param name="setF2FFee"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/TransferSetF2FFee")]
        [MoudleInfo("获取金额收款码")]
        public async Task<ActionResult<ResponseBase>> TransferSetF2FFee(SetF2FFeeRequest setF2FFee)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_TRANSFERSETF2FFEE;
            var url = WechatHelper.GetUrl(cgiType);

            TransferSetF2FFeeRequest transferSetF2FFee = new TransferSetF2FFeeRequest()
            {
                describe = $"desc={setF2FFee.Desc}&fee={setF2FFee.Money}&fee_type=1",
                token = setF2FFee.Token,
            };

            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, transferSetF2FFee);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }


        /// <summary>
        /// 点击红包
        /// </summary>
        /// <param name="receiveWxHb"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ReceiveWxHb")]
        [MoudleInfo("点击红包")]
        public async Task<ActionResult<ResponseBase>> ReceiveWxHb(ReceiveWxHbRequest receiveWxHb)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_RECEIVEWXHB;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, receiveWxHb);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }

        /// <summary>
        /// 打开红包
        /// </summary>
        /// <param name="openWxHb"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/OpenWxHb")]
        [MoudleInfo("打开红包")]
        public async Task<ActionResult<ResponseBase>> OpenWxHb(OpenWxHbRequest openWxHb)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_OPENWXHB;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, openWxHb);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }

        /// <summary>
        /// 签收转账
        /// </summary>
        /// <param name="transferOperation"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/TransferOperation")]
        [MoudleInfo("签收转账")]
        public async Task<ActionResult<ResponseBase>> TransferOperation(TransferOperationRequest transferOperation)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_TRANSFEROPERATION;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, transferOperation);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;

        }
        #endregion



        #region 公共

        /// <summary>
        /// 上传通讯录
        /// </summary>
        /// <param name="uploadMContact"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/UploadMContact")]
        [MoudleInfo("上传通讯录")]
        public async Task<ActionResult<ResponseBase>> UploadMContact(UploadMContactRequest uploadMContact)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADMCONTACT;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadMContact);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 下载通讯录
        /// </summary>
        /// <param name="getMFriend"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetMFriend")]
        [MoudleInfo("下载通讯录")]
        public async Task<ActionResult<ResponseBase>> GetMFriend(GetMFriendRequest getMFriend)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_GETMFRIEND;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getMFriend);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 提交微信运动步数
        /// </summary>
        /// <param name="uploadDeviceStep"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/UploadDeviceStep")]
        [MoudleInfo("提交微信运动步数")]
        public async Task<ActionResult<ResponseBase>> UploadDeviceStep(UploadDeviceStepRequest uploadDeviceStep)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADDEVICESTEP;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, uploadDeviceStep);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 附近的人
        /// </summary>
        /// <param name="lbsFind"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/LbsFind")]
        [MoudleInfo("附近的人")]
        public async Task<ActionResult<ResponseBase>> LbsFind(LbsFindRequest lbsFind)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_LBSFIND;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, lbsFind);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result.code == "0")
                {
                    response.Data = result.message.ToObj();
                }
                else
                {
                    response.Data = result.message.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 摇一摇
        /// </summary>
        /// <param name="shakeReport"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ShakeReport")]
        [MoudleInfo("摇一摇")]
        public async Task<ActionResult<ResponseBase>> ShakeReport(ShakeReportRequest shakeReport)
        {
            ResponseBase<object> response = new ResponseBase<object>();

            string shakeCgiType = CGI_TYPE.CGI_SHAKEREPORT;
            var shakeUrl = WechatHelper.GetUrl(shakeCgiType);
            var shakeResp = await HttpHelper.PostAsync<WechatResponseBase>(shakeUrl, shakeReport);
            if (shakeResp.code == "0")
            {
                var shakeResult = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(shakeCgiType, shakeResp);
                if (shakeResult.code == "0")
                {
                    string cgiType = CGI_TYPE.CGI_SHAKEREGET;
                    var url = WechatHelper.GetUrl(cgiType);
                    ShakereGetRequest shakereGet = new ShakereGetRequest()
                    {
                        buffer = null
                    };
                    var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, shakereGet);
                    if (resp.code == "0")
                    {
                        var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                        if (result.code == "0")
                        {
                            response.Data = result.message.ToObj();
                        }
                        else
                        {
                            response.Data = result.message.ToObj();
                            response.Success = false;
                            response.Message = result.describe;
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = $"{resp.message}{resp.describe}";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = $"{shakeResult.message}{shakeResult.describe}";
                }
            }
            else
            {
                response.Success = false;
                response.Message = $"{shakeResp.message}{shakeResp.describe}";
            }






            return response;
        }
        #endregion




        //private async Task initWxInfo(string token, WechatCreateRequest wechatCreateRequest)
        //{
        //    var wxInfo = await DbContext.WxInfos.Where(o => o.WxId == manualAuthResponse.wxid).FirstOrDefaultAsync();
        //    if (wxInfo == null)
        //    {
        //        wxInfo = new WxInfo()
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = UserInfo.Id,
        //            UserName = wechatCreateRequest.user,
        //            Password = wechatCreateRequest.pass,
        //            Device = wechatCreateRequest.deviceID,



        //            AddTime = DateTime.Now,
        //            AddUserId = UserInfo.Id,
        //            AddUserRealName = UserInfo.RealName,
        //            UpdateTime = DateTime.Now,
        //            UpdateUserId = UserInfo.Id,
        //            UpdateUserRealName = UserInfo.RealName,
        //        };
        //        DbContext.Add(wxInfo);
        //    }
        //    else
        //    {
        //        await refreshWxInfo();
        //    }

        //}


        //private Task refreshWxInfo()
        //{


        //    WxInfo wxInfo = new WxInfo()
        //    {
        //        Id = Guid.NewGuid(),
        //        UserId = UserInfo.Id,




        //        AddTime = DateTime.Now,
        //        AddUserId = UserInfo.Id,
        //        AddUserRealName = UserInfo.RealName,
        //        UpdateTime = DateTime.Now,
        //        UpdateUserId = UserInfo.Id,
        //        UpdateUserRealName = UserInfo.RealName,
        //    };
        //    DbContext.Add(wxInfo);
        //}

    }
}
