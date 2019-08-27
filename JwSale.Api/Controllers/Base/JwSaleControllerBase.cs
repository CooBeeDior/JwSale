using JwSale.Api.Const;
using JwSale.Api.Filters;
using JwSale.Api.Http;
using JwSale.Api.Util;
using JwSale.Model;
using JwSale.Model.Dto.Wechat;
using JwSale.Repository.Context;
using JwSale.Util.Dependencys;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 基类
    /// </summary> 
    /// 
    [TypeFilter(typeof(PermissionRequiredAttribute))]
    [TypeFilter(typeof(ValidationModelAttribute))]
    [TypeFilter(typeof(ExceptionAttribute))]
    [TypeFilter(typeof(LogAttribute))]
    [ApiController]
    //[NoPermissionRequired]
    public class JwSaleControllerBase : ControllerBase
    {
        protected JwSaleDbContext DbContext { get; }

        protected UserInfo UserInfo { get; private set; }

        public JwSaleControllerBase(JwSaleDbContext jwSaleDbContext)
        {
            DbContext = jwSaleDbContext;
            var accessor = ServiceLocator.Instance.GetService<IHttpContextAccessor>();
            UserInfo = accessor.HttpContext.Items[CacheKeyHelper.GetHttpContextUserKey()] as UserInfo;
        }




        protected async Task<bool> RefreshWxInoAsync(string token, string wxId, string username = null, string password = null, string device = null)
        {
            bool flag = false;
            GetContactRequest getContact = new GetContactRequest()
            {
                token = token,
                wxid = wxId
            };
            {
                //初始化用户信息
                string cgiType = CGI_TYPE.CGI_GETCONTACT;
                var url = WechatHelper.GetUrl(cgiType);
                var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, getContact);
                if (resp.code == "0")
                {
                    var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                    if (result?.code == "0")
                    {
                        var obj = result.message?.ToObj<GetContactResponse>();

                        if (obj != null && obj.baseResponse.ret == 0)
                        {
                            var contact = obj.contactList[0];

                            var wxInfo = await DbContext.WxInfos.Where(o => o.WxId == wxId).FirstOrDefaultAsync();
                            if (wxInfo == null)
                            {
                                wxInfo = new WxInfo()
                                {
                                    Id = Guid.NewGuid(),

                                    UserName = username,
                                    Password = password,
                                    Device = device,
                                    WxId = wxId,
                                    NickName = contact.nickName.str ?? "",
                                    HeadImgUrl = contact.smallHeadImgUrl,
                                    Alias = contact.alias,
                                    Sex = contact.sex,
                                    Uin = "",
                                    Email = "",
                                    Mobile = "",
                                    Country = contact.country,
                                    Province = contact.province,
                                    City = contact.city,
                                    Signature = contact.signature,
                                    Status = 0,
                                    Token = token,
                                    AddTime = DateTime.Now,
                                    AddUserId = UserInfo.Id,
                                    AddUserRealName = UserInfo.RealName,
                                    UpdateTime = DateTime.Now,
                                    UpdateUserId = UserInfo.Id,
                                    UpdateUserRealName = UserInfo.RealName,

                                };

                                DbContext.Add(wxInfo);

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(username))
                                {
                                    wxInfo.UserName = username;
                                }
                                if (!string.IsNullOrEmpty(password))
                                {
                                    wxInfo.Password = password;
                                }
                                if (!string.IsNullOrEmpty(device))
                                {
                                    wxInfo.Device = device;
                                }

                                wxInfo.WxId = wxId;
                                wxInfo.NickName = contact.nickName.str ?? "";
                                wxInfo.HeadImgUrl = contact.smallHeadImgUrl;
                                wxInfo.Alias = contact.alias;
                                wxInfo.Sex = contact.sex;
                                wxInfo.Uin = "";
                                wxInfo.Email = "";
                                wxInfo.Mobile = "";
                                wxInfo.Country = contact.country;
                                wxInfo.Province = contact.province;
                                wxInfo.City = contact.city;
                                wxInfo.Signature = contact.signature;
                                wxInfo.Status = 0;
                                wxInfo.Token = token;
                                wxInfo.UpdateTime = DateTime.Now;
                                wxInfo.UpdateUserId = UserInfo.Id;
                                wxInfo.UpdateUserRealName = UserInfo.RealName;

                            }

                            flag = true;


                        }
                    }

                }
            }

            {
                //初始化好友和群信息
                NewSyncRequest newSync = new NewSyncRequest()
                {
                    selector = "5",
                    token = token
                };
                var chatroomInfos = await DbContext.ChatRoomInfos.Where(o => o.BelongWxId == wxId).AsNoTracking().ToListAsync();
                var wxFriendInfos = await DbContext.WxFriendInfos.Where(o => o.BelongWxId == wxId).AsNoTracking().ToListAsync();
                var ghInfos = await DbContext.GhInfos.Where(o => o.BelongWxId == wxId).AsNoTracking().ToListAsync();

                while (true)
                {
                    string cgiType = CGI_TYPE.CGI_NEWSYNC;
                    var url = WechatHelper.GetUrl(cgiType);
                    var resp = await HttpHelper.PostAsync<WechatResponseBase>(url, newSync);
                    if (resp.code == "0")
                    {
                        var result = await HttpHelper.PostVxApiAsync<WechatAnalysisResponse>(cgiType, resp);
                        if (result?.code == "0")
                        {
                            var obj = result.message?.ToObj<NewSyncResponse>();
                            if (obj.count > 0 && obj.list != null)
                            {
                                foreach (var item in obj.list)
                                {
                                    //群
                                    if (item.personalCard == 0 && !string.IsNullOrEmpty(item.chatRoomOwner))
                                    {
                                        if (chatroomInfos.Where(o => o.ChatRoomId == item.userName.str).Count() == 0)
                                        {
                                            var ownerWxInfo = item.newChatroomData?.chatRoomMember?.Where(o => o.userName == item.chatRoomOwner)?.FirstOrDefault();
                                            ChatRoomInfo chatRoomInfo = new ChatRoomInfo()
                                            {
                                                Id = Guid.NewGuid(),
                                                BelongWxId = wxId,
                                                ChatRoomId = item.userName.str,
                                                ChatRoomName = item.nickName.str ?? "",
                                                HeadImgUrl = item.smallHeadImgUrl,
                                                OwnerWxId = item.chatRoomOwner,
                                                OwnerWxNickName = ownerWxInfo.nickName ?? "",
                                                OwnerWxHeadImgUrl = ownerWxInfo.smallHeadImgUrl ?? "",
                                                ChatroomMaxCount = item.chatroomMaxCount,
                                                ChatRoomMemberCount = item.newChatroomData?.memberCount ?? 0,
                                                AddTime = DateTime.Now,
                                                AddUserId = UserInfo.Id,
                                                AddUserRealName = UserInfo.RealName,
                                                UpdateTime = DateTime.Now,
                                                UpdateUserId = UserInfo.Id,
                                                UpdateUserRealName = UserInfo.RealName,
                                            };
                                            DbContext.Add(chatRoomInfo);


                                        }

                                        if (item.newChatroomData?.chatRoomMember != null)
                                        {
                                            await DbContext.ChatRoomMemberInfos.Where(o => o.ChatRoomId == item.userName.str).DeleteAsync();
                                            foreach (var memmber in item.newChatroomData.chatRoomMember)
                                            {
                                                ChatRoomMemberInfo chatRoomMemberInfo = new ChatRoomMemberInfo()
                                                {
                                                    Id = Guid.NewGuid(),

                                                    ChatRoomId = item.userName.str,
                                                    WxId = memmber.userName,
                                                    NickName = memmber.nickName,
                                                    DisplayName = memmber.displayName,
                                                    HeadImgUrl = item.smallHeadImgUrl,
                                                    AddTime = DateTime.Now,
                                                    AddUserId = UserInfo.Id,
                                                    AddUserRealName = UserInfo.RealName,
                                                    UpdateTime = DateTime.Now,
                                                    UpdateUserId = UserInfo.Id,
                                                    UpdateUserRealName = UserInfo.RealName,

                                                };
                                                DbContext.Add(chatRoomMemberInfo);


                                            }
                                        }


                                    }
                                    //好友
                                    else if (item.personalCard == 1)
                                    {
                                        if (wxFriendInfos.Where(o => o.WxId == item.userName.str).Count() == 0)
                                        {
                                            WxFriendInfo wxFriendInfo = new WxFriendInfo()
                                            {
                                                Id = Guid.NewGuid(),
                                                BelongWxId = wxId,
                                                WxId = item.userName.str,
                                                NickName = item.nickName.str ?? "",
                                                HeadImgUrl = item.smallHeadImgUrl,
                                                Alias = item.alias,
                                                Sex = item.sex,
                                                Country = item.country,
                                                Province = item.province,
                                                City = item.city,
                                                Signature = item.signature,
                                                AddTime = DateTime.Now,
                                                AddUserId = UserInfo.Id,
                                                AddUserRealName = UserInfo.RealName,
                                                UpdateTime = DateTime.Now,
                                                UpdateUserId = UserInfo.Id,
                                                UpdateUserRealName = UserInfo.RealName,

                                            };
                                            DbContext.Add(wxFriendInfo);

                                        }
                                    }
                                    //公众号等
                                    else
                                    {
                                        if (ghInfos.Where(o => o.GhId == item.userName.str).Count() == 0)
                                        {
                                            GhInfo ghInfo = new GhInfo()
                                            {
                                                Id = Guid.NewGuid(),
                                                BelongWxId = wxId,
                                                GhId = item.userName.str,
                                                NickName = item.nickName.str ?? "",
                                                HeadImgUrl = item.smallHeadImgUrl,
                                                Alias = item.alias,
                                                Sex = item.sex,
                                                Country = item.country,
                                                Province = item.province,
                                                City = item.city,
                                                Signature = item.signature,
                                                AddTime = DateTime.Now,
                                                AddUserId = UserInfo.Id,
                                                AddUserRealName = UserInfo.RealName,
                                                UpdateTime = DateTime.Now,
                                                UpdateUserId = UserInfo.Id,
                                                UpdateUserRealName = UserInfo.RealName,
                                            };
                                            DbContext.Add(ghInfo);

                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                    }
                }
            }
            await DbContext.SaveChangesAsync();
            return flag;



        }

    }
}
