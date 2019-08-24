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
    /// 支付管理
    /// </summary>
    [MoudleInfo("支付管理", 1)]
    public class PayController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public PayController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }

        #region 支付
        /// <summary>
        /// 获取收款码
        /// </summary>
        /// <param name="f2fQrCode"></param>
        /// <returns></returns>
        [HttpPost("api/Pay/F2fQrCode")]
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
        /// 获取金额收款码
        /// </summary>
        /// <param name="setF2FFee"></param>
        /// <returns></returns>
        [HttpPost("api/Pay/TransferSetF2FFee")]
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
        /// 点击红包
        /// </summary>
        /// <param name="receiveWxHb"></param>
        /// <returns></returns>
        [HttpPost("api/Pay/ReceiveWxHb")]
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
        /// 打开红包
        /// </summary>
        /// <param name="openWxHb"></param>
        /// <returns></returns>
        [HttpPost("api/Pay/OpenWxHb")]
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
        /// 签收转账
        /// </summary>
        /// <param name="transferOperation"></param>
        /// <returns></returns>
        [HttpPost("api/Pay/TransferOperation")]
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
