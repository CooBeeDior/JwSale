//using JwSale.Api.Const;
//using JwSale.Api.Extensions;
//using JwSale.Api.Http;
//using JwSale.Api.Util;
//using JwSale.Model;
//using JwSale.Model.Dto;
//using JwSale.Model.Dto.Request.TaskInfo;
//using JwSale.Model.Dto.Wechat;
//using JwSale.Packs.Attributes;
//using JwSale.Repository.Context;
//using JwSale.Util.Extensions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Caching.Distributed;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace JwSale.Api.Controllers
//{

//    /// <summary>
//    /// 任务管理
//    /// </summary>
//    [MoudleInfo("任务管理", 1)]
//    public class TaskInfoController : JwSaleControllerBase
//    {
//        private IDistributedCache cache;
//        private IHttpContextAccessor accessor;
//        public TaskInfoController(JwSaleDbContext context, IDistributedCache cache, IHttpContextAccessor accessor) : base(context)
//        {
//            this.cache = cache;
//            this.accessor = accessor;
//        }

//        /// <summary>
//        /// 创建任务
//        /// </summary>
//        /// <param name="createTaskInfo"></param>
//        /// <returns></returns>
//        [HttpPost("api/TaskInfo/CreateTaskInfo")]
//        [MoudleInfo("创建任务")]
//        public async Task<ActionResult<ResponseBase>> CreateTaskInfo(CreateTaskInfoRequest createTaskInfo)
//        {
//            ResponseBase response = new ResponseBase();
//            var chatRoomInfo = await DbContext.ChatRoomInfos.Where(o => o.ChatRoomId == createTaskInfo.ChatRoomId).AsNoTracking().FirstOrDefaultAsync();
//            var toWxInfo = await DbContext.WxInfos.Where(o => o.WxId == createTaskInfo.ToWxId).AsNoTracking().FirstOrDefaultAsync();
//            var chatRoomTaskInfo = await DbContext.ChatRoomTaskInfos.Where(o => o.ChatRoomId == createTaskInfo.ChatRoomId).FirstOrDefaultAsync();
//            if (chatRoomTaskInfo == null)
//            {
//                chatRoomTaskInfo = new ChatRoomTaskInfo()
//                {
//                    Id = Guid.NewGuid(),

//                    ChatRoomId = createTaskInfo.ChatRoomId,
//                    ChatRoomName = chatRoomInfo?.ChatRoomName,
//                    ChatRoomHeadImgUrl = chatRoomInfo?.HeadImgUrl,
//                    CurrentMemberCount = chatRoomInfo.ChatRoomMemberCount,

//                    ToWxId = createTaskInfo.ToWxId,
//                    ToWxNickName = toWxInfo?.NickName ?? "",
//                    ToWxHeadImgUrl = toWxInfo?.HeadImgUrl ?? "",
//                    TargetMemberCount = createTaskInfo.TargetMemberCount,

//                    AddTime = DateTime.Now,
//                    AddUserId = UserInfo.Id,
//                    AddUserRealName = UserInfo.RealName,
//                    UpdateTime = DateTime.Now,
//                    UpdateUserId = UserInfo.Id,
//                    UpdateUserRealName = UserInfo.RealName,
//                };
//                DbContext.Add(chatRoomTaskInfo);

//            }
//            else
//            {
//                chatRoomTaskInfo.ToWxId = createTaskInfo.ToWxId;
//                chatRoomTaskInfo.ToWxNickName = toWxInfo?.NickName ?? "";
//                chatRoomTaskInfo.ToWxHeadImgUrl = toWxInfo?.HeadImgUrl ?? "";
//                chatRoomTaskInfo.TargetMemberCount = createTaskInfo.TargetMemberCount;

//                chatRoomTaskInfo.UpdateTime = DateTime.Now;
//                chatRoomTaskInfo.UpdateUserId = UserInfo.Id;
//                chatRoomTaskInfo.UpdateUserRealName = UserInfo.RealName;
//            }


//            foreach (var wxid in createTaskInfo.WxIds)
//            {
//                var wxInfo = await DbContext.WxInfos.Where(o => o.WxId == wxid).FirstOrDefaultAsync();
//                WxTaskInfo wxTaskInfo = new WxTaskInfo()
//                {
//                    Id = Guid.NewGuid(),

//                    ChatRoomTaskInfoId = chatRoomTaskInfo.Id,
//                    WxId = wxid,
//                    NickName = wxInfo?.NickName ?? "",
//                    HeadImgUrl = wxInfo?.HeadImgUrl ?? "",
//                    Status = createTaskInfo.Status,

//                    AddTime = DateTime.Now,
//                    AddUserId = UserInfo.Id,
//                    AddUserRealName = UserInfo.RealName,
//                    UpdateTime = DateTime.Now,
//                    UpdateUserId = UserInfo.Id,
//                    UpdateUserRealName = UserInfo.RealName,
//                };
//                DbContext.Add(wxTaskInfo);
//            };

//            await DbContext.SaveChangesAsync();

//            return response;
//        }

//        /// <summary>
//        /// 修改任务
//        /// </summary>
//        /// <param name="updateTaskInfo"></param>
//        /// <returns></returns>
//        [HttpPost("api/TaskInfo/UpdateTaskInfo")]
//        [MoudleInfo("修改任务")]
//        public async Task<ActionResult<ResponseBase>> UpdateTaskInfo(UpdateTaskInfoRequest updateTaskInfo)
//        {
//            ResponseBase response = new ResponseBase();
//            var chatRoomInfo = await DbContext.ChatRoomInfos.Where(o => o.ChatRoomId == updateTaskInfo.ChatRoomId).AsNoTracking().FirstOrDefaultAsync();
//            var toWxInfo = await DbContext.WxInfos.Where(o => o.WxId == updateTaskInfo.ToWxId).AsNoTracking().FirstOrDefaultAsync();

//            var chatRoomTaskInfo = await DbContext.ChatRoomTaskInfos.Where(o => o.ChatRoomId == updateTaskInfo.ChatRoomId).FirstOrDefaultAsync();
//            if (chatRoomTaskInfo == null)
//            {

//                response.Success = false;
//                response.Message = "任务不存在";
//            }
//            else
//            {
//                chatRoomTaskInfo.ToWxId = updateTaskInfo.ToWxId;
//                chatRoomTaskInfo.ToWxNickName = toWxInfo?.NickName ?? "";
//                chatRoomTaskInfo.ToWxHeadImgUrl = toWxInfo?.HeadImgUrl ?? "";
//                chatRoomTaskInfo.TargetMemberCount = updateTaskInfo.TargetMemberCount;

//                chatRoomTaskInfo.UpdateTime = DateTime.Now;
//                chatRoomTaskInfo.UpdateUserId = UserInfo.Id;
//                chatRoomTaskInfo.UpdateUserRealName = UserInfo.RealName;

//                var wxTaskInfos = await DbContext.WxTaskInfos.Where(o => o.ChatRoomTaskInfoId == chatRoomTaskInfo.Id).ToListAsync();
//                foreach (var wxid in updateTaskInfo.WxIds)
//                {
//                    var wxTaskInfo = wxTaskInfos.Where(o => o.WxId == wxid).FirstOrDefault();
//                    if (wxTaskInfo == null)
//                    {
//                        var wxInfo = await DbContext.WxInfos.Where(o => o.WxId == wxid).FirstOrDefaultAsync();
//                        wxTaskInfo = new WxTaskInfo()
//                        {
//                            Id = Guid.NewGuid(),

//                            ChatRoomTaskInfoId = chatRoomTaskInfo.Id,
//                            WxId = wxid,
//                            NickName = wxInfo?.NickName ?? "",
//                            HeadImgUrl = wxInfo?.HeadImgUrl ?? "",
//                            Status = updateTaskInfo.Status,

//                            AddTime = DateTime.Now,
//                            AddUserId = UserInfo.Id,
//                            AddUserRealName = UserInfo.RealName,
//                            UpdateTime = DateTime.Now,
//                            UpdateUserId = UserInfo.Id,
//                            UpdateUserRealName = UserInfo.RealName,
//                        };

//                        DbContext.Add(wxTaskInfo);
//                    }
//                    else
//                    {
//                        wxTaskInfo.Status = updateTaskInfo.Status;
//                        wxTaskInfo.UpdateTime = DateTime.Now;
//                        wxTaskInfo.UpdateUserId = UserInfo.Id;
//                        wxTaskInfo.UpdateUserRealName = UserInfo.RealName;

//                    }

//                };


//            }




//            await DbContext.SaveChangesAsync();

//            return response;
//        }


//        /// <summary>
//        /// 获取任务列表
//        /// </summary>
//        /// <param name="getTaskInfoList"></param>
//        /// <returns></returns>
//        [HttpPost("api/TaskInfo/GetTaskInfoList")]
//        [MoudleInfo("获取任务列表")]
//        public async Task<ActionResult<ResponseBase>> GetTaskInfoList(GetTaskInfoListRequest getTaskInfoList)
//        {
//            PageResponseBase<IList<(ChatRoomTaskInfo, IList<WxTaskInfo>)>> response = new PageResponseBase<IList<(ChatRoomTaskInfo, IList<WxTaskInfo>)>>();
//            IList<(ChatRoomTaskInfo, IList<WxTaskInfo>)> list = new List<(ChatRoomTaskInfo, IList<WxTaskInfo>)>();
//            var chatRoomTaskInfos = DbContext.ChatRoomTaskInfos.AsNoTracking().AsQueryable(UserInfo.Id);
//            if (!string.IsNullOrEmpty(getTaskInfoList.KeyWords))
//            {
//                chatRoomTaskInfos = chatRoomTaskInfos.Where(o => o.ChatRoomId.Contains(getTaskInfoList.KeyWords) || o.ChatRoomName.Contains(getTaskInfoList.KeyWords));
//            }
//            chatRoomTaskInfos = chatRoomTaskInfos.ToPage(getTaskInfoList);
//            response.TotalCount = chatRoomTaskInfos.Count();

//            var wxTaskInfos = await DbContext.WxTaskInfos.AsNoTracking().ToListAsync();
//            foreach (var item in chatRoomTaskInfos)
//            {
//                var currentWxTaskInfos = wxTaskInfos.Where(o => o.ChatRoomTaskInfoId == item.Id).ToList();
//                list.Add((item, currentWxTaskInfos));
//            }
//            response.Data = list;


//            return response;
//        }





//    }
//}
