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
    /// 群管理
    /// </summary>
    [MoudleInfo("群管理", 1)]
    public class GroupController : JwSaleControllerBase
    {
        private IDistributedCache cache;
        private IHttpContextAccessor accessor;
        public GroupController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
        {
            this.cache = cache;
            this.accessor = accessor;
        }
        #region 群
        /// <summary>
        /// 扫码进群
        /// </summary>
        /// <param name="scanIntoChatRoom"></param>
        /// <returns></returns>
        [HttpPost("api/Group/ScanIntoChatRoom")]
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
        /// 创建群
        /// </summary>
        /// <param name="createChatRoom"></param>
        /// <returns></returns>
        [HttpPost("api/Group/CreateChatRoom")]
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
        /// 群成员列表
        /// </summary>
        /// <param name="memberDetail"></param>
        /// <returns></returns>
        [HttpPost("api/Group/MemberDetail")]
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
        /// 添加群成员（40内）
        /// </summary>
        /// <param name="addChatRoomMember"></param>
        /// <returns></returns>
        [HttpPost("api/Group/AddChatRoomMember")]
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
        /// 邀请群成员（40外）
        /// </summary>
        /// <param name="inviteChatRoomMember"></param>
        /// <returns></returns>
        [HttpPost("api/Group/InviteChatRoomMember")]
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
        /// 转让群主
        /// </summary>
        /// <param name="transferChatRoomOwner"></param>
        /// <returns></returns>
        [HttpPost("api/Group/TransferChatRoomOwner")]
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
        /// 退群
        /// </summary>
        /// <param name="quitChatRoom"></param>
        /// <returns></returns>
        [HttpPost("api/Group/QuitChatRoom")]
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
        /// 删除群成员
        /// </summary>
        /// <param name="delChatRoomMember"></param>
        /// <returns></returns>
        [HttpPost("api/Group/DelChatRoomMember")]
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
