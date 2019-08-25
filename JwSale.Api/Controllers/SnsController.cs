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
    /// 朋友圈管理
    /// </summary>
    [MoudleInfo("朋友圈管理", 1)]
    public class SnsController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public SnsController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }

        #region 朋友圈

        /// <summary>
        /// 获取朋友圈首页
        /// </summary>
        /// <param name="mmSnsTimeLine"></param>
        /// <returns></returns>
        [HttpPost("api/Sns/MmSnsTimeLine")]
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
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 获取指定朋友圈
        /// </summary>
        /// <param name="mmSnsUserpage"></param>
        /// <returns></returns>
        [HttpPost("api/Sns/MmSnsUserpage")]
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
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 朋友圈点赞评论回复
        /// </summary>
        /// <param name="mmSnsComment"></param>
        /// <returns></returns>
        [HttpPost("api/Sns/MmSnsComment")]
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
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 朋友圈操作
        /// </summary>
        /// <param name="mmSnsObjectOp"></param>
        /// <returns></returns>
        [HttpPost("api/Sns/MmSnsObjectOp")]
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
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 朋友圈图片上传
        /// </summary>
        /// <param name="mmSnsUpload"></param>
        /// <returns></returns>
        [HttpPost("api/Sns/MmSnsUpload")]
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
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 发送朋友圈
        /// </summary>
        /// <param name="sendSns"></param>
        /// <returns></returns>
        [HttpPost("api/Sns/MmSnsPost")]
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
                response.Message = $"{resp.message}{resp.describe}";
            }
            return response;
        }

        #endregion
    }
}
