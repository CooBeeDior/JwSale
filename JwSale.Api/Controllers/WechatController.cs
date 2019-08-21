using JwSale.Api.Const;
using JwSale.Api.Extensions;
using JwSale.Api.Filters;
using JwSale.Api.Http;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Response.Wechat;
using JwSale.Model.Dto.Wechat;
using JwSale.Packs.Attributes;
using JwSale.Packs.Options;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
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

        private JwSaleOptions jwSaleOptions;

        private IHttpContextAccessor accessor;
        public WechatController(JwSaleDbContext context, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.jwSaleOptions = jwSaleOptions.Value;
            this.accessor = accessor;

        }


        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/Wechat/GetQrCode")]
        [MoudleInfo("获取二维码")]
        public async Task<ActionResult<ResponseBase>> GetQrCode()
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

                var url = WechatHelper.GetUrl(CGI_TYPE.CGI_GETLOGINQRCODE);
                var qrResp = await HttpHelper.PostAsync<WechatResponseBase>(url, getLoginQrCodeRequest);
                if (qrResp.code == "0")
                {
                    var packetResp = await HttpHelper.PostPacketAsync(qrResp.url, qrResp.packet);

                    AnalysisData analysisData = new AnalysisData(resp.token, packetResp);

                    var analysisUrl = WechatHelper.GetUrl(CGI_TYPE.CGI_GETLOGINQRCODE.ToAnalysis());

                    var result = await HttpHelper.PostAsync<WechatAnalysisResponse>(analysisUrl, analysisData);
                    if (result.code == "0")
                    {
                        var image = result.message.ToObj<GetLoginQrCodeMsg>();
                        var imgBuffer = image.Image.StrToHexBuffer().ToBase64Img();

                        response.Data = new GetQrCodeResponse()
                        {
                            Token = resp.token,
                            Base64 = imgBuffer
                        };
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = result.message;
                    }

                }
                else
                {
                    response.Success = false;
                    response.Message = qrResp.message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = resp.message;
            }
            return response;

        }
    }
}
