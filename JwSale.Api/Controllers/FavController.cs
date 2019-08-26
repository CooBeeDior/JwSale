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
    /// 标签管理
    /// </summary>
    [MoudleInfo("标签管理", 1)]
    public class FavController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public FavController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }

        #region 标签

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="addContactLabel"></param>
        /// <returns></returns>
        [HttpPost("api/Fav/AddContactLabel")]
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
                if (result?.code == "0")
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
        [HttpPost("api/Fav/ModifyContactLabelList")]
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
                if (result?.code == "0")
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
        [HttpPost("api/Fav/DelContactLabel")]
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
                if (result?.code == "0")
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
        [HttpPost("api/Fav/GetContactLabelList")]
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
                if (result?.code == "0")
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
    }
}
