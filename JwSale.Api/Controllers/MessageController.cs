using JwSale.Api.Const;
using JwSale.Api.Http;
using JwSale.Api.Util;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Request.Wechat;
using JwSale.Model.Dto.Response.Wechat;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Attributes;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 消息管理
    /// </summary>
    [MoudleInfo("消息管理", 1)]
    public class MessageController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public MessageController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }

        #region 消息

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="newSendMsg"></param>
        /// <returns></returns>
        [HttpPost("api/Message/NewSendMsg")]
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
        /// 发送图片消息
        /// </summary>
        /// <param name="uploadMsgImg"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendImgMsg")]
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
        /// 发送消息CDN图片
        /// </summary>
        /// <param name="uploadMsgImgCdn"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendImgMsgCdn")]
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
        /// 发送语音消息
        /// </summary>
        /// <param name="uploadVoice"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SemdVoiceMsg")]
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
        /// 发送视频消息
        /// </summary>
        /// <param name="uploadVideo"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendViedoMsg")]
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
        /// 发送App消息
        /// </summary>
        /// <param name="appMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendAppMsg")]
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
        /// 发送分享消息
        /// </summary>
        /// <param name="appMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendShareMsg")]
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
        /// 发送名片消息
        /// </summary>
        /// <param name="cardMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendCardMsg")]
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
        /// 发送位置消息
        /// </summary>
        /// <param name="locationMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendLocationMsg")]
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
        /// 发送文件消息
        /// </summary>
        /// <param name="mediaMessage"></param>
        /// <returns></returns>
        [HttpPost("api/Message/SendMediaMsg")]
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
        /// 消息撤回
        /// </summary>
        /// <param name="revokeMsg"></param>
        /// <returns></returns>
        [HttpPost("api/Message/RevokeMsg")]
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
        /// 获取高清图片
        /// </summary>
        /// <param name="getMsgImg"></param>
        /// <returns></returns>
        [HttpPost("api/Message/GetMsgImg")]
        [MoudleInfo("获取高清图片")]
        public async Task<ActionResult<ResponseBase>> GetMsgImg(GetMsgImgRequest getMsgImg)
        {
            ResponseBase<object> response = new ResponseBase<object>();
            string cgiType = CGI_TYPE.CGI_GETMSGIMG;
            var url = WechatHelper.GetUrl(cgiType);
            var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getMsgImg);
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
    }
}
