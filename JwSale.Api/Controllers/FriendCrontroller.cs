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
    /// 
    /// </summary>
    [MoudleInfo("好友管理", 1)]
    public class FriendCrontroller : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public FriendCrontroller(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }
        #region 好友
        /// <summary>
        /// 获取联系人(通讯录群友)
        /// </summary>
        /// <param name="getContact"></param>
        /// <returns></returns>
        [HttpPost("api/Friend/GetContact")]
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
                response.Message =  "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }



        /// <summary>
        /// 搜索联系人(非好友)
        /// </summary>
        /// <param name="searchContact"></param>
        /// <returns></returns>
        [HttpPost("api/Friend/SearchContact")]
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
                response.Message =  "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 添加或同意联系人
        /// </summary>
        /// <param name="verifyUser"></param>
        /// <returns></returns>
        [HttpPost("api/Friend/VerifyUser")]
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
                response.Message =  "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }

        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="delContact"></param>
        /// <returns></returns>
        [HttpPost("api/Friend/DelContact")]
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
                response.Message =  "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }


        /// <summary>
        /// 修改好友备注
        /// </summary>
        /// <param name="setFriendRemarks"></param>
        /// <returns></returns>
        [HttpPost("api/Friend/SetFriendRemarks")]
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
                response.Message =  "执行失败";//$"{resp.message}{resp.describe}";
            }
            return response;
        }
        #endregion
    }
}
