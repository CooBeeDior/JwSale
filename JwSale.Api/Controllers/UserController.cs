using JsSaleService;
using JwSale.Api.Attributes;
using JwSale.Api.Events;
using JwSale.Api.Extensions;
using JwSale.Model;
using JwSale.Model.Dto;
using JwSale.Model.Dto.Cache;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Request;
using JwSale.Model.Dto.Response;
using JwSale.Packs.Options;
using JwSale.Util;
using JwSale.Util.Attributes;
using JwSale.Util.Enums;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RabbitmqCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwSale.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [MoudleInfo("用户管理", 1)]
    public class UserController : ManageControllerBase
    {
        private readonly IUserService _userService;
        private IDistributedCache _cache;
        private JwSaleOptions _jwSaleOptions;
        private readonly IRabbitmqPublisher<AddUserSucceedEvent> _rabbitmqPublisher;


        public UserController(IUserService userService, IDistributedCache cache, IOptions<JwSaleOptions> jwSaleOptions, IRabbitmqPublisher<AddUserSucceedEvent> rabbitmqPublisher)
        {
            _userService = userService;
            _cache = cache;
            _jwSaleOptions = jwSaleOptions.Value;
            _rabbitmqPublisher = rabbitmqPublisher;

        }

        /// <summary>
        /// 登录获取令牌（填入Authorize）
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [MoudleInfo("登录", false)]
        [NoAuthRequired]
        [HttpPost("api/user/login")]
        public async Task<ActionResult<ResponseBase<LoginResponse>>> Login(LoginRequest login)
        {
            ResponseBase<LoginResponse> response = new ResponseBase<LoginResponse>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.UserName == login.UserName).ToOneAsync();
            if (userinfo != null)
            {
                if (userinfo.Password.Equals(login.Password.ToMd5(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (userinfo.Status != 0)
                    {
                        response.Success = false;
                        response.Code = HttpStatusCode.Unauthorized;
                        response.Message = "用户被禁用，请联系管理员开启";
                    }
                    else if (userinfo.ExpiredTime == null || userinfo.ExpiredTime < DateTime.Now)
                    {
                        response.Success = false;
                        response.Code = HttpStatusCode.Unauthorized;
                        response.Message = "用户已过期";
                    }
                    else
                    {
                        UserToken userToken = new UserToken()
                        {
                            UserId = userinfo.Id,
                            UserName = userinfo.UserName,
                            Ip = HttpContext.Connection.RemoteIpAddress.ToString(),
                            ExpiredTime = userinfo.ExpiredTime,
                            AddTime = DateTime.Now
                        };
                        string token = userToken.GenerateToken(_jwSaleOptions.TokenKey);

                        var permissions = await _userService.GetUserPermissions(userinfo.Id);

                        LoginResponse loginResponse = new LoginResponse()
                        {
                            Token = token,
                            ExpiredTime = userinfo.ExpiredTime,
                            UserInfo = userinfo,
                            Permissions = permissions
                        };

                        UserCache userCache = new UserCache()
                        {
                            Token = token,
                            UserInfo = userinfo,
                            Permissions = permissions,
                            LoginDevice = HttpContext.LoginDevice(),
                            LoginTime = DateTime.Now
                        };

                        var cacheEntryOptions = new DistributedCacheEntryOptions() { AbsoluteExpiration = new DateTimeOffset(userinfo.ExpiredTime) };
                        await _cache.SetStringAsync(CacheKeyHelper.GetLoginUserKey(userinfo.UserName, HttpContext.LoginDevice()), userCache.ToJson(), cacheEntryOptions);
                        response.Data = loginResponse;
                    }
                }
                else
                {
                    response.Success = false;
                    response.Code = HttpStatusCode.Unauthorized;
                    response.Message = "密码错误";
                }
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.Unauthorized;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("退出", false)]
        [HttpPost("api/user/logout")]
        public async Task<ActionResult<ResponseBase>> Logout()
        {
            ResponseBase response = new ResponseBase();
            var loginDevice = HttpContext.LoginDevice();
            await _cache.RemoveAsync(CacheKeyHelper.GetLoginUserKey(CurrentUserInfo.UserName, loginDevice));
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取用户信息")]
        [HttpGet("api/user/getuserinfo")]
        public async Task<ActionResult<ResponseBase<UserInfo>>> GetUserInfo()
        {
            ResponseBase<UserInfo> response = new ResponseBase<UserInfo>();

            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == CurrentUserInfo.Id).ToOneAsync();
            if (userinfo != null)
            {
                response.Data = userinfo;
            }
            else
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户名不存在";
            }
            return await response.ToJsonResultAsync();
        }




        /// <summary>
        /// 获取用户角色权限
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("获取用户角色权限")]
        [HttpGet("api/user/getuserpermissions")]
        public async Task<ActionResult<ResponseBase<IList<BriefInfo>>>> GetUserPermissions()
        {
            ResponseBase<IList<BriefInfo>> response = new ResponseBase<IList<BriefInfo>>();

            var permissions = await _userService.GetUserPermissions(CurrentUserInfo.Id);
            response.Data = permissions;
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 获取用户角色权限树
        /// </summary> 
        /// <returns></returns>
        [MoudleInfo("获取用户角色权限树")]
        [HttpGet("api/userrole/getuserpermissiontree")]
        public async Task<ActionResult<ResponseBase<IList<UserPermissionTreeResponse>>>> GetUserPermissionTree(string userId)
        {
            ResponseBase<IList<UserPermissionTreeResponse>> response = new ResponseBase<IList<UserPermissionTreeResponse>>();
            bool isExsitUserInfo = await FreeSql.Select<UserInfo>().WhereIf(userId.IsNullOrWhiteSpace(), o => o.Id == userId).AnyAsync();
            if (isExsitUserInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"【{userId}】用户已存在";
            }
            response.Data = await _userService.GetUserPermissionTree(userId);
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("添加角色", Type = FunctionType.Add)]
        [HttpPost("api/user/addrole")]
        public async Task<ActionResult<ResponseBase<string>>> AddRole(AddRoleRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            bool isExsitRoleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Name == request.Name && o.ParentId == request.ParentId).AnyAsync();
            if (isExsitRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"【{request.Name}】角色已存在";
            }
            else if (!await FreeSql.Select<RoleInfo>().WhereIf(!request.ParentId.IsNullOrWhiteSpace(), o => o.Id == request.ParentId).AnyAsync())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"父角色不存在";
            }
            else
            {

                var roleInfo = new RoleInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Remark = request.Remark,
                    ParentId = request.ParentId
                };
                roleInfo.InitAddBaseEntityData(CurrentUserInfo);
                int count = await FreeSql.Insert(roleInfo).ExecuteAffrowsAsync();
                response.Data = roleInfo.Id;

            }
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("修改角色", Type = FunctionType.Update)]
        [HttpPost("api/user/updaterole")]
        public async Task<ActionResult<ResponseBase>> UpdateRole(UpdateRoleRequest request)
        {
            ResponseBase response = new ResponseBase();
            if (request.Id == request.ParentId)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"父级不能是自己";
                return await response.ToJsonResultAsync();
            }
            bool isExsitRoleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Id != request.Id).Where(o => o.Name == request.Name && o.ParentId == request.ParentId).AnyAsync();
            if (isExsitRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"【{request.Name}】角色已存在";
            }
            else if (!await FreeSql.Select<RoleInfo>().WhereIf(!request.ParentId.IsNullOrWhiteSpace(), o => o.Id == request.ParentId).AnyAsync())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"父角色不存在";
            }
            else
            {
                int count = await FreeSql.Update<RoleInfo>(request.Id)
                    .Set(o => o.Name == request.Name)
                    .Set(o => o.ParentId == request.ParentId)
                    .Set(o => o.Remark == request.Remark)
                    .InitUpdateBaseEntityData(CurrentUserInfo)
                    .ExecuteAffrowsAsync();
                if (count == 0)
                {
                    response.Success = false;
                    response.Code = HttpStatusCode.BadRequest;
                    response.Message = $"【{request.Name}】角色不存在";
                }
            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [MoudleInfo("删除角色", Type = FunctionType.Delete)]
        [HttpPost("api/user/deleterole")]
        public async Task<ActionResult<ResponseBase<int>>> DeleteRole(string id)
        {

            ResponseBase<int> response = new ResponseBase<int>();
            if (id.IsNullOrWhiteSpace())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "id不能为空";
                return await response.ToJsonResultAsync(); ;
            }
            var roleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Id == id).ToOneAsync();
            if (roleInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "角色不存在";
            }
            else
            {
                int count = await FreeSql.Delete<RoleInfo>().Where(o => o.Id == id).ExecuteAffrowsAsync();
                response.Data = count;

            }
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("添加用户角色", Type = FunctionType.Add)]
        [HttpPost("api/user/adduserrole")]
        public async Task<ActionResult<ResponseBase<string>>> AddUserRole(AddUserRoleRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            var isExsitUserInfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == request.UserId).AnyAsync();
            if (!isExsitUserInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"用户不存在";
                return await response.ToJsonResultAsync(); ;
            }
            var isExsitRoleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Id == request.RoleId).AnyAsync();
            if (!isExsitRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"角色不存在";
                return await response.ToJsonResultAsync(); ;
            }
            var isExsitUserRoleInfo = await FreeSql.Select<UserRoleInfo>().Where(o => o.RoleId == request.RoleId && o.UserId == request.UserId).AnyAsync();
            if (isExsitUserRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"用户角色已存在";
                return await response.ToJsonResultAsync(); ;
            }
            UserRoleInfo userRoleInfo = new UserRoleInfo()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                RoleId = request.RoleId
            };
            userRoleInfo.InitAddBaseEntityData(CurrentUserInfo);
            int count = await FreeSql.Insert(userRoleInfo).ExecuteAffrowsAsync();
            response.Data = userRoleInfo.Id;
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="id">用户角色id</param>
        /// <returns></returns>
        [MoudleInfo("删除用户角色", Type = FunctionType.Delete)]
        [HttpPost("api/user/deleteuserrole")]
        public async Task<ActionResult<ResponseBase<int>>> DeleteUserRole(string id)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            if (id.IsNullOrWhiteSpace())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "id不能为空";
                return await response.ToJsonResultAsync(); ;
            }
            var roleInfo = await FreeSql.Select<UserRoleInfo>().Where(o => o.Id == id).ToOneAsync();
            if (roleInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户角色不存在";
            }
            else
            {
                int count = await FreeSql.Delete<UserRoleInfo>().Where(o => o.Id == id).ExecuteAffrowsAsync();
                response.Data = count;

            }
            return await response.ToJsonResultAsync();
        }






        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("添加角色权限", Type = FunctionType.Add)]
        [HttpPost("api/user/addrolepermission")]
        public async Task<ActionResult<ResponseBase<string>>> AddRolePermission(AddRolePermissionRequest request)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            var isExsitRolePermissionInfo = await FreeSql.Select<RolePermissionInfo>().Where(o => o.RoleId == request.RoleId && o.FunctionId == request.FunctionId).AnyAsync();
            if (isExsitRolePermissionInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"角色权限已存在";
                return await response.ToJsonResultAsync(); ;
            }
            var isExsitRoleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Id == request.RoleId).AnyAsync();
            if (!isExsitRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "角色不存在";
                return await response.ToJsonResultAsync(); ;
            }
            var isExsitFunctionInfo = await FreeSql.Select<FunctionInfo>().Where(o => o.Id == request.FunctionId).AnyAsync();
            if (!isExsitFunctionInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "系统功能不存在";
                return await response.ToJsonResultAsync(); ;
            }
            var rolePermissionInfo = new RolePermissionInfo()
            {
                Id = Guid.NewGuid().ToString(),
                FunctionId = request.FunctionId,
                RoleId = request.RoleId
            };
            rolePermissionInfo.InitAddBaseEntityData(CurrentUserInfo);
            int count = await FreeSql.Insert(rolePermissionInfo).ExecuteAffrowsAsync();
            response.Data = rolePermissionInfo.Id;
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 批量添加角色权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("批量添加角色权限", Type = FunctionType.Add)]
        [HttpPost("api/user/batchaddrolepermission")]
        public async Task<ActionResult<ResponseBase<IList<string>>>> BatchAddRolePermission(BatchAddRolePermissionRequest request)
        {
            ResponseBase<IList<string>> response = new ResponseBase<IList<string>>();
            IList<string> list = new List<string>();
            var isExsitRoleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Id == request.RoleId).AnyAsync();
            if (!isExsitRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户角色不存在";
                return await response.ToJsonResultAsync(); ;
            }
            int count = FreeSql.Delete<RolePermissionInfo>().Where(o => o.RoleId == request.RoleId).ExecuteAffrows();
            var functionIds = FreeSql.Select<FunctionInfo>().Where(o => request.FunctionIds.Contains(o.Id)).ToList(o => o.Id);
            foreach (var functionId in functionIds)
            {
                var rolePermissionInfo = new RolePermissionInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    FunctionId = functionId,
                    RoleId = request.RoleId
                };
                rolePermissionInfo.InitAddBaseEntityData(CurrentUserInfo);
                list.Add(rolePermissionInfo.Id);
                count = await FreeSql.Insert(rolePermissionInfo).ExecuteAffrowsAsync();
            }

            response.Data = list;
            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 添加角色所有权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [MoudleInfo("添加角色所有权限", Type = FunctionType.Add)]
        [HttpPost("api/user/addroleallpermission")]
        public async Task<ActionResult<ResponseBase<IList<string>>>> AddRoleAllPermission(AddRoleAllPermissionRequest request)
        {
            ResponseBase<IList<string>> response = new ResponseBase<IList<string>>();
            IList<string> list = new List<string>();
            var isExsitRoleInfo = await FreeSql.Select<RoleInfo>().Where(o => o.Id == request.RoleId).AnyAsync();
            if (!isExsitRoleInfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户角色不存在";
                return await response.ToJsonResultAsync(); ;
            }
            int count = FreeSql.Delete<RolePermissionInfo>().Where(o => o.RoleId == request.RoleId).ExecuteAffrows();

            var functionIds = await FreeSql.Select<FunctionInfo>().ToListAsync(o => o.Id);

            foreach (var functionId in functionIds)
            {
                var rolePermissionInfo = new RolePermissionInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    FunctionId = functionId,
                    RoleId = request.RoleId
                };
                rolePermissionInfo.InitAddBaseEntityData(CurrentUserInfo);
                list.Add(rolePermissionInfo.Id);
                count = await FreeSql.Insert(rolePermissionInfo).ExecuteAffrowsAsync();
            }

            response.Data = list;
            return await response.ToJsonResultAsync();
        }
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MoudleInfo("删除角色权限", Type = FunctionType.Delete)]
        [HttpPost("api/user/deleterolepermission")]
        public async Task<ActionResult<ResponseBase<int>>> DeleteRolePermission(string id)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            if (id.IsNullOrWhiteSpace())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "id不能为空";
                return await response.ToJsonResultAsync(); ;
            }
            var rolePermissionInfo = await FreeSql.Select<RolePermissionInfo>().Where(o => o.Id == id).ToOneAsync();
            if (rolePermissionInfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "角色权限不存在";
            }
            else
            {
                int count = await FreeSql.Delete<RolePermissionInfo>().Where(o => o.Id == id).ExecuteAffrowsAsync();
                response.Data = count;

            }
            return await response.ToJsonResultAsync();
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/user/adduser")]
        [MoudleInfo("添加用户")]
        public async Task<ActionResult<ResponseBase<string>>> AddUser(AddUserRequest addUser)
        {
            ResponseBase<string> response = new ResponseBase<string>();
            bool isExsitUser = await FreeSql.Select<UserInfo>().Where(o => o.Phone == addUser.Phone).WhereIf(!addUser.UserName.IsNullOrWhiteSpace(), o => o.UserName == addUser.UserName).AnyAsync();
            if (isExsitUser)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = $"【{addUser.UserName}】用户名已存在";
            }
            else
            {
                UserInfo userInfo = new UserInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = addUser.UserName.IsNullOrWhiteSpace() ? addUser.Phone : addUser.UserName,
                    Password = DefaultPassword.PASSWORD.ToMd5(),
                    RealName = addUser.RealName,
                    RealNamePin = addUser.RealName?.ToPinYin(),
                    Email = addUser.Email,
                    Sex = (int)(addUser.Sex ?? SexType.UnKnown),
                    Status = 0,
                    Phone = addUser.Phone,
                    IdCard = addUser.IdCard,
                    BirthDay = addUser.BirthDay,
                    Province = addUser.Province,
                    City = addUser.City,
                    Area = addUser.Area,
                    Address = addUser.Address,
                    Remark = addUser.Remark,
                    Qq = addUser.Qq,
                    WxNo = addUser.WxNo,
                    TelPhone = addUser.TelPhone,
                    PositionName = addUser.PositionName,
                    HeadImageUrl = addUser.HeadImageUrl,

                    Type = addUser.Type,
                    ExpiredTime = addUser.ExpiredTime?.Date ?? DateTime.Now.AddYears(1),



                    AddTime = DateTime.Now,
                    AddUserId = CurrentUserInfo.Id,
                    AddUserRealName = CurrentUserInfo.RealName,
                    UpdateTime = DateTime.Now,
                    UpdateUserId = CurrentUserInfo.Id,
                    UpdateUserRealName = CurrentUserInfo.RealName,
                };
                int count = await FreeSql.Insert<UserInfo>(userInfo).ExecuteAffrowsAsync();
                if (count > 0)
                {
                    _rabbitmqPublisher.Publish(new AddUserSucceedEvent(userInfo));
                    response.Data = userInfo.Id;
                }
            }

            return await response.ToJsonResultAsync();
        }


        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="setUserStatus"></param>
        /// <returns></returns>
        [HttpPost("api/user/setuserstatus")]
        [MoudleInfo("设置用户状态", Type = FunctionType.Update)]
        public async Task<ActionResult<ResponseBase>> SetUserStatus(SetUserStatusRequest setUserStatus)
        {
            ResponseBase response = new ResponseBase();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == setUserStatus.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Update<UserInfo>(setUserStatus.UserId)
                      .Set(a => a.Status == (int)setUserStatus.Status)
                      .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();

            }
            return await response.ToJsonResultAsync();
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="resetUserPwd"></param>
        /// <returns></returns>
        [HttpPost("api/user/resetuserpwd")]
        [MoudleInfo("重置用户密码", Type = FunctionType.Update)]
        public async Task<ActionResult<ResponseBase<int>>> ResetUserPwd(ResetUserPwdRequest resetUserPwd)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == resetUserPwd.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Update<UserInfo>(CurrentUserInfo.Id)
                       .Set(a => a.Password == resetUserPwd.NewPassword.ToMd5())
                       .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();
                response.Data = count;
            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 修改用户简介
        /// </summary>
        /// <param name="setUserProfile"></param>
        /// <returns></returns>
        [HttpPost("api/user/setuserprofile")]
        [MoudleInfo("修改用户简介", Type = FunctionType.Update)]
        public async Task<ActionResult<ResponseBase<int>>> SetUserProfile(SetUserProfileRequest setUserProfile)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == setUserProfile.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                string pinyin = setUserProfile.RealName?.ToPinYin();
                int count = await FreeSql.Update<UserInfo>(CurrentUserInfo.Id)
                     .Set(a => a.Phone == setUserProfile.Phone)
                     .Set(a => a.Email == setUserProfile.Email)
                     .Set(a => a.Sex == (int)(setUserProfile.Sex ?? SexType.UnKnown))
                     .Set(a => a.RealName == setUserProfile.RealName)
                     .Set(a => a.RealNamePin == pinyin)
                     .Set(a => a.Qq == setUserProfile.Qq)
                     .Set(a => a.WxNo == setUserProfile.WxNo)
                     .Set(a => a.TelPhone == setUserProfile.TelPhone)
                     .Set(a => a.PositionName == setUserProfile.PositionName)
                     .Set(a => a.Province == setUserProfile.Province)
                     .Set(a => a.City == setUserProfile.City)
                     .Set(a => a.Area == setUserProfile.Area)
                     .Set(a => a.Address == setUserProfile.Address)
                     .Set(a => a.HeadImageUrl == setUserProfile.HeadImageUrl)
                     .Set(a => a.Remark == setUserProfile.Remark)
                     .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();

                response.Data = count;

            }
            return await response.ToJsonResultAsync();

        }



        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="setUserAuth"></param>
        /// <returns></returns>
        [HttpPost("api/user/setuserauth")]
        [MoudleInfo("修改用户授权", Type = FunctionType.Update)]
        public async Task<ActionResult<ResponseBase<int>>> SetUserAuth(SetUserAuthRequest setUserAuth)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            var userinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == setUserAuth.UserId).ToOneAsync();
            if (userinfo == null)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Update<UserInfo>(CurrentUserInfo.Id)
                                       .SetIf(setUserAuth.Type != null, a => a.Type == setUserAuth.Type)
                                       .SetIf(setUserAuth.ExpiredTime != null, a => a.ExpiredTime == setUserAuth.ExpiredTime)
                                       .InitUpdateBaseEntityData(CurrentUserInfo).ExecuteAffrowsAsync();

                response.Data = count;

            }
            return await response.ToJsonResultAsync();

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("api/user/deleteuser")]
        [MoudleInfo("删除用户", Type = FunctionType.Delete)]
        public async Task<ActionResult<ResponseBase<int>>> DeleteUser(string id)
        {
            ResponseBase<int> response = new ResponseBase<int>();
            if (id.IsNullOrWhiteSpace())
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "id不能为空";
                return await response.ToJsonResultAsync(); ;
            }
            var isExistUserinfo = await FreeSql.Select<UserInfo>().Where(o => o.Id == id).AnyAsync();
            if (!isExistUserinfo)
            {
                response.Success = false;
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "用户不存在";
            }
            else
            {
                int count = await FreeSql.Delete<UserInfo>().Where(o => o.Id == id).ExecuteAffrowsAsync();
                var userRoleInfos = await FreeSql.Delete<UserRoleInfo>().Where(o => o.UserId == id).ExecuteDeletedAsync();
                count += await FreeSql.Delete<UserRoleInfo>().Where(o => userRoleInfos.Select(s => s.Id).Contains(o.RoleId)).ExecuteAffrowsAsync();
                response.Data = count;

            }
            return await response.ToJsonResultAsync();

        }










    }
}