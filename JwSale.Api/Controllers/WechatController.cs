﻿using JwSale.Api.Const;
using JwSale.Api.Events;
using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Http;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Request.Wechat;
using JwSale.Model.Dto.Response.Wechat;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Attributes;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    public class LongContenct
    {

        private static ConcurrentDictionary<string, TcpClient> clientDic = new ConcurrentDictionary<string, TcpClient>();




        public static async Task AddClientAsync(string longUrl, string token)
        {
            var ipPort = longUrl.ToIpPort();
            TcpClient tcpClient = new TcpClient(ipPort.Item1, ipPort.Item2);
            tcpClient = clientDic.GetOrAdd(token, tcpClient);

            if (!tcpClient.Connected)
            {
                tcpClient.Connect(ipPort.Item1, ipPort.Item2);
            }
            var sm = tcpClient.GetStream();
            string cgiType = CGI_TYPE.CGI_HEARTBEAT;
            var url = WechatHelper.GetUrl(cgiType);
            HeartBeatRequest heartBeat = new HeartBeatRequest()
            {
                token = token
            };
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, heartBeat);

            int length = resp.packet.Length / 2 + 16;
            var buffer1 = Encoding.UTF8.GetBytes(length.ToString()).Reverse();
            var buffer2 = ("00100001000000EE00000000" + resp.packet).StrToHexBuffer();
            List<byte> bufferList = new List<byte>();
            bufferList.AddRange(buffer1);
            bufferList.AddRange(buffer2);
            var buffer = bufferList.ToArray();

            sm.Write(buffer, 0, buffer.Length);
            await sm.FlushAsync();
            Receive(token, tcpClient, sm);



        }

        private static void Receive(string token, TcpClient client, NetworkStream sm)
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        int bufSize = client.ReceiveBufferSize;
                        byte[] buffer = new byte[bufSize];
                        sm.Read(buffer, 0, buffer.Length);

                        //处理buffer
                    }
                }
                catch (Exception ex)
                {
                    clientDic.Remove(token, out client);
                }
            });

        }



    }



    /// <summary>
    /// 微信管理
    /// </summary>
    [MoudleInfo("微信管理", 1)]
    public class WechatController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        private IMediator mediator;
        private IModel _channel;
        public WechatController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor, IMediator mediator, Func<string, IModel> modelFuc) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
            this.mediator = mediator;
            _channel = modelFuc(QueueConst.RefreshWxInfoName);
        }

        #region 用户

 

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="proxyInfo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetLoginQrCode")]
        [MoudleInfo("获取二维码")]
        public async Task<ActionResult<ResponseBase>> GetLoginQrCode(ProxyInfo proxyInfo)
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
                    var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, wechatResp, proxyInfo);
                    if (result?.code == "0")
                    {
                        var image = result.message.ToObj<GetLoginQrCodeMsg>();
                        var imgBuffer = image.Image.StrToHexBuffer().ToBase64Img();

                        response.Data = new GetQrCodeResponse()
                        {
                            Base64 = imgBuffer,
                            TempToken = wechatResp.token
                        };

                        WechatCache wechatCache = new WechatCache()
                        {
                            Token = wechatResp.token,
                            ProxyInfo = proxyInfo
                        };
                        await cache.SetStringAsync(CacheKeyHelper.GetUserTokenKey(wechatResp.token), wechatCache.ToJson());

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
                    response.Message = "执行失败";//$"{wechatResp.message}{wechatResp.describe}";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
            var wechatResp = await HttpHelper.PostAsync<WechatResponseBase>(url, new { token = checkLoginQrCode.TempToken });
            if (wechatResp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(CGI_TYPE.CGI_CHECKLOGINQRCODE, wechatResp);

                var resp = result?.message?.ToObj<CheckLoginQrCodeResponse>();
                if (resp?.state == 2)
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
                            if (maResult?.code == "0")
                            {
                                var maResp = maResult.message.ToObj<ManualAuthResponse>();
                                response.ExtensionData = maResp;
                                response.Token = maResult.token;

                                var wechatCacheStr = await cache.GetStringAsync(CacheKeyHelper.GetUserTokenKey(checkLoginQrCode.TempToken));
                                var wechatCache = wechatCacheStr?.ToObj<WechatCache>();
                                //缓存     
                                wechatCache = new WechatCache()
                                {
                                    Token = maRechatResp.token,
                                    CheckLoginQrCode = resp,
                                    ManualAuth = maResp,
                                    ProxyInfo = wechatCache.ProxyInfo
                                };
                                await cache.SetStringAsync(CacheKeyHelper.GetUserTokenKey(maRechatResp.token), wechatCache.ToJson());

                                //await RefreshWxInoAsync(maResult.token, maResp.wxid, maResp.wxid, "", resp.device);

                                var refreshWxInfoEvent = new RefreshWxInfoEvent()
                                {
                                    Token = maResult.token,
                                    WxId = maResp.wxid,
                                    UserName = maResp.wxid,
                                    Password = "",
                                    Device = resp.device,
                                    UserInfo = UserInfo
                                };

                                //await mediator.Publish(refreshWxInfoEvent);

                                //发布消息
                                var buffer = Encoding.UTF8.GetBytes(refreshWxInfoEvent.ToJson());                      
                                _channel.BasicPublish("", QueueConst.RefreshWxInfoName, null, buffer);

                                //await LongContenct.AddClientAsync(maResp.fsUrl, maResult.token);


                            }
                            else if (maResult.code == "-301")
                            { }
                            else
                            {
                                response.Success = false;
                                response.Message = "执行失败";//$"{maResult.message}{maResult.describe}";
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
                response.Message = "执行失败";//$"{wechatResp.message}{wechatResp.describe}";
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
                        maResult = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, maRechatResp, login.proxyInfo);
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
                                ManualAuth = maResp,
                                ProxyInfo = login.proxyInfo

                            };
                            await cache.SetStringAsync(CacheKeyHelper.GetUserTokenKey(maRechatResp.token), wechatCache.ToJson());

                            //刷新数据
                            //await RefreshWxInoAsync(maResult.token, maResp.wxid, login.user, login.pass, login.deviceID);
                            var refreshWxInfoEvent = new RefreshWxInfoEvent()
                            {
                                Token = maResult.token,
                                WxId = maResp.wxid,
                                UserName = login.user,
                                Password = login.pass,
                                Device = login.deviceID,
                                UserInfo = UserInfo

                            };
                            //await mediator.Publish(refreshWxInfoEvent);

                            //发布消息
                            var buffer = Encoding.UTF8.GetBytes(refreshWxInfoEvent.ToJson());
                            _channel.BasicPublish("", QueueConst.RefreshWxInfoName, null, buffer);
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
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }


            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = true;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
        public async Task<ActionResult<ResponseBase>> UploadDeviceStep(UploadDeviceStepModelRequest uploadDeviceStep)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_UPLOADDEVICESTEP;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, new UploadDeviceStepRequest()
            {
                token = uploadDeviceStep.Token,
                stepCount = uploadDeviceStep.stepCount,
                fromTime = DateTime.Now.To10TimeStamp().ToString(),
                toTime = DateTime.Now.To10TimeStamp().ToString()

            });
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                if (shakeResult?.code == "0")
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
                        if (result?.code == "0")
                        {
                            response.Data = result.message?.ToObj();
                        }
                        else
                        {
                            response.Data = result.message?.ToObj();
                            response.Success = false;
                            response.Message = result.describe;
                        }
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "执行失败";//$"{resp.message}{resp.describe}";
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
                response.Message = "执行失败";//$"{shakeResp.message}{shakeResp.describe}";
            }


            return response;
        }


        /// <summary>
        /// 关注公众号
        /// </summary>
        /// <param name="forkOfficialAccount"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/ForkOfficialAccount")]
        [MoudleInfo("关注公众号")]
        public async Task<ActionResult<ResponseBase>> ForkOfficialAccount(ForkOfficialAccountRequest forkOfficialAccount)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_VERIFYUSER;
            var url = WechatHelper.GetUrl(cgiType);
            VerifyUserRequest verifyUser = new VerifyUserRequest()
            {
                token = forkOfficialAccount.token,
                verifyUserOpCode = "1",
                v1 = forkOfficialAccount.AppId,
                scene = "0",
                v2 = "",
                verifyContent = ""

            };


            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, verifyUser);

            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// GetA8Key（阅读 扫码进群）
        /// </summary>
        /// <param name="getA8Key"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetA8Key")]
        [MoudleInfo("GetA8Key")]
        public async Task<ActionResult<ResponseBase>> GetA8Key(GetA8KeyRequest getA8Key)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_A8KEY;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getA8Key);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }



        /// <summary>
        /// GetMpA8Key（授权登录）
        /// </summary>
        /// <param name="getA8Key"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetMpA8Key")]
        [MoudleInfo("GetMpA8Key")]
        public async Task<ActionResult<ResponseBase>> GetMpA8Key(GetA8KeyRequest getA8Key)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_MPA8KEY;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getA8Key);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 获取小程序授权
        /// </summary>
        /// <param name="jsVerify"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/JsVerify")]
        [MoudleInfo("获取小程序授权")]
        public async Task<ActionResult<ResponseBase>> JsVerify(JsVerifyRequest jsVerify)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_JSOPERATEWXDATA;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, jsVerify);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 小程序登录
        /// </summary>
        /// <param name="jsLogin"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/JsLogin")]
        [MoudleInfo("小程序登录")]
        public async Task<ActionResult<ResponseBase>> JsLogin(JsLoginRequest jsLogin)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_OAUTHAUTHORIZE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, jsLogin);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 获取安全设备
        /// </summary>
        /// <param name="getSafetyInfo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetSafetyInfo")]
        [MoudleInfo("获取安全设备")]
        public async Task<ActionResult<ResponseBase>> GetSafetyInfo(GetSafetyInfoRequest getSafetyInfo)
        {

            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_TYPE_GETSAFETYINFO;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getSafetyInfo);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 删除安全设备
        /// </summary>
        /// <param name="delSafeDevice"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/DelSafeDevice")]
        [MoudleInfo("删除安全设备")]
        public async Task<ActionResult<ResponseBase>> DelSafeDevice(DelSafeDeviceRequest delSafeDevice)
        {

            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_TYPE_DELSAFEDEVICE;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, delSafeDevice);
            if (resp.code == "0")
            {
                var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                if (result?.code == "0")
                {
                    response.Data = result.message?.ToObj();
                }
                else
                {
                    response.Data = result.message?.ToObj();
                    response.Success = false;
                    response.Message = result.describe;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }






        #endregion





        #region  微信相关信息

        /// <summary>
        /// 获取微信列表
        /// </summary>
        /// <param name="getWxInfoList"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetWxInfoList")]
        [MoudleInfo("获取微信列表")]
        public async Task<ActionResult<ResponseBase>> GetWxInfoList(GetWxInfoListRequest getWxInfoList)
        {
            PageResponseBase<IList<WxInfo>> response = new PageResponseBase<IList<WxInfo>>();

            var wxinfos = DbContext.WxInfos.AsQueryable(UserInfo.Id);
            if (!string.IsNullOrEmpty(getWxInfoList.KeyWords))
            {
                wxinfos = wxinfos.Where(o => o.WxId.Contains(getWxInfoList.KeyWords) || o.NickName.Contains(getWxInfoList.KeyWords));

            }
            int totalCount = wxinfos.Count();
            wxinfos = wxinfos.ToPage(getWxInfoList);
            response.Data = await wxinfos.ToArrayAsync();
            response.TotalCount = totalCount;
            return response;
        }


        /// <summary>
        /// 获取微信详情
        /// </summary>
        /// <param name="getWxInfo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetWxInfo")]
        [MoudleInfo("获取微信详情")]
        public async Task<ActionResult<ResponseBase>> GetWxInfo(GetWxInfoRequest getWxInfo)
        {
            ResponseBase<WxInfo> response = new ResponseBase<WxInfo>();

            var wxinfo = await DbContext.WxInfos.Where(o => o.WxId == getWxInfo.WxId).FirstOrDefaultAsync();
            if (wxinfo == null)
            {
                response.Success = false;
                response.Message = "信息不存在";
            }
            else
            {
                response.Data = wxinfo;
            }
            return response;
        }



        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="getWxFriendList"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetWxFriendList")]
        [MoudleInfo("获取好友列表")]
        public async Task<ActionResult<ResponseBase>> GetWxFriendList(GetWxFriendListRequest getWxFriendList)
        {
            PageResponseBase<IList<WxFriendInfo>> response = new PageResponseBase<IList<WxFriendInfo>>();

            var wxFriendInfos = DbContext.WxFriendInfos.Where(o => o.BelongWxId == getWxFriendList.WxId).AsQueryable(UserInfo.Id);
            if (!string.IsNullOrEmpty(getWxFriendList.KeyWords))
            {
                wxFriendInfos = wxFriendInfos.Where(o => o.WxId.Contains(getWxFriendList.KeyWords) || o.NickName.Contains(getWxFriendList.KeyWords));
            }
            if (getWxFriendList.Sex != null)
            {
                wxFriendInfos = wxFriendInfos.Where(o => o.Sex == getWxFriendList.Sex);

            }
            if (!string.IsNullOrEmpty(getWxFriendList.Province))
            {
                wxFriendInfos = wxFriendInfos.Where(o => o.Province == getWxFriendList.Province);
            }
            if (!string.IsNullOrEmpty(getWxFriendList.City))
            {
                wxFriendInfos = wxFriendInfos.Where(o => o.City == getWxFriendList.City);
            }

            int totalCount = wxFriendInfos.Count();
            wxFriendInfos = wxFriendInfos.ToPage(getWxFriendList);
            response.Data = await wxFriendInfos.ToArrayAsync();
            response.TotalCount = totalCount;
            return response;
        }



        /// <summary>
        /// 获取微信好友详情
        /// </summary>
        /// <param name="getWxFriendInfo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetWxFriendInfo")]
        [MoudleInfo("获取微信好友详情")]
        public async Task<ActionResult<ResponseBase>> GetWxFriendInfo(GetWxFriendInfoRequest getWxFriendInfo)
        {
            ResponseBase<WxFriendInfo> response = new ResponseBase<WxFriendInfo>();

            var wxFriendInfo = await DbContext.WxFriendInfos.Where(o => o.BelongWxId == getWxFriendInfo.BelongWxId && o.WxId == getWxFriendInfo.WxId).FirstOrDefaultAsync();
            if (wxFriendInfo == null)
            {
                response.Success = false;
                response.Message = "信息不存在";
            }
            else
            {
                response.Data = wxFriendInfo;
            }
            return response;
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <param name="getChatRoomList"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetChatRoomList")]
        [MoudleInfo("获取群列表")]
        public async Task<ActionResult<ResponseBase>> GetChatRoomList(GetChatRoomListRequest getChatRoomList)
        {
            PageResponseBase<IList<ChatRoomInfo>> response = new PageResponseBase<IList<ChatRoomInfo>>();

            var chatRoomInfos = DbContext.ChatRoomInfos.Where(o => o.BelongWxId == getChatRoomList.WxId).AsQueryable(UserInfo.Id);
            if (!string.IsNullOrEmpty(getChatRoomList.KeyWords))
            {
                chatRoomInfos = chatRoomInfos.Where(o => o.ChatRoomId.Contains(getChatRoomList.KeyWords) || o.ChatRoomName.Contains(getChatRoomList.KeyWords));

            }
            int totalCount = chatRoomInfos.Count();
            chatRoomInfos = chatRoomInfos.ToPage(getChatRoomList);
            response.Data = await chatRoomInfos.ToArrayAsync();
            response.TotalCount = totalCount;
            return response;
        }



        /// <summary>
        /// 获取群详情
        /// </summary>
        /// <param name="getChatRoomInfo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetChatRoomInfo")]
        [MoudleInfo("获取群详情")]
        public async Task<ActionResult<ResponseBase>> GetChatRoomInfo(GetChatRoomInfoRequest getChatRoomInfo)
        {
            ResponseBase<ChatRoomInfo> response = new ResponseBase<ChatRoomInfo>();

            var chatRoomInfos = await DbContext.ChatRoomInfos.Where(o => o.BelongWxId == getChatRoomInfo.BelongWxId && o.ChatRoomId == getChatRoomInfo.ChatRoomId).FirstOrDefaultAsync();
            if (chatRoomInfos == null)
            {
                response.Success = false;
                response.Message = "信息不存在";
            }
            else
            {
                response.Data = chatRoomInfos;
            }
            return response;
        }



        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="getChatRoomInfo"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetChatRoomMemberList")]
        [MoudleInfo("获取群成员列表")]
        public async Task<ActionResult<ResponseBase>> GetChatRoomMemberList(GetChatRoomInfoRequest getChatRoomInfo)
        {
            ResponseBase<IList<ChatRoomMemberInfo>> response = new ResponseBase<IList<ChatRoomMemberInfo>>();

            var chatRoomMemberInfo = await DbContext.ChatRoomMemberInfos.Where(o => o.ChatRoomId == getChatRoomInfo.ChatRoomId).ToListAsync();
            if (chatRoomMemberInfo == null)
            {
                response.Success = false;
                response.Message = "信息不存在";
            }
            else
            {
                response.Data = chatRoomMemberInfo;
            }
            return response;
        }


        /// <summary>
        /// 获取公众号列表
        /// </summary>
        /// <param name="getGhInfoList"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetGhInfoList")]
        [MoudleInfo("获取公众号列表")]
        public async Task<ActionResult<ResponseBase>> GetGhInfoList(GetGhInfoListRequest getGhInfoList)
        {
            PageResponseBase<IList<GhInfo>> response = new PageResponseBase<IList<GhInfo>>();

            var ghInfos = DbContext.GhInfos.Where(o => o.BelongWxId == getGhInfoList.WxId).AsQueryable(UserInfo.Id);
            if (!string.IsNullOrEmpty(getGhInfoList.KeyWords))
            {
                ghInfos = ghInfos.Where(o => o.GhId.Contains(getGhInfoList.KeyWords) || o.NickName.Contains(getGhInfoList.KeyWords));

            }
            int totalCount = ghInfos.Count();
            ghInfos = ghInfos.ToPage(getGhInfoList);
            response.Data = await ghInfos.ToArrayAsync();
            response.TotalCount = totalCount;
            return response;
        }
        /// <summary>
        /// 刷新微信通讯录列表（好友 群  公众号）
        /// </summary>
        /// <param name="refreshWxInfoRequest"></param>
        /// <returns></returns>
        [HttpPost("api/Wechat/RefreshWxInfo")]
        [MoudleInfo("刷新微信数据")]
        public async Task<ActionResult<ResponseBase>> RefreshWxInfo(RefreshWxInfoRequest refreshWxInfoRequest)
        {

            ResponseBase response = new ResponseBase();
            var wxinfo = await DbContext.WxInfos.Where(o => o.WxId == refreshWxInfoRequest.WxId).FirstOrDefaultAsync();
            if (wxinfo == null)
            {
                response.Success = false;
                response.Message = "微信信息不存在";
            }
            else
            {
                var refreshWxInfoEvent = new RefreshWxInfoEvent()
                {
                    Token = wxinfo.Token,
                    WxId = wxinfo.WxId,
                    UserInfo = UserInfo,
                    IsRefresh = true
                };
                //await mediator.Publish(refreshWxInfoEvent);

                //发布消息
                var buffer = Encoding.UTF8.GetBytes(refreshWxInfoEvent.ToJson());
                _channel.BasicPublish("", QueueConst.RefreshWxInfoName, null, buffer);

            }


            return response;
        }

        #endregion

    }
}
