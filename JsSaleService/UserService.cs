using JwSale.Model;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Response;
using JwSale.Util;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace JsSaleService
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : Service, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IFreeSql freeSql, IHttpContextAccessor httpContextAccessor) : base(freeSql)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 获取用户权限功能树
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<FunctionTreeResponse>> GetUserPermissionTree(string userId)
        {

            var functions = await FreeSql.Select<FunctionInfo>().OrderBy(o => o.Order).ToListAsync();

            var result = FreeSql.Select<UserInfo, UserRoleInfo, RoleInfo, RolePermissionInfo, FunctionInfo>()
                 .InnerJoin((u, ur, r, rp, f) => u.Id == ur.UserId)
                 .InnerJoin((u, ur, r, rp, f) => ur.RoleId == r.Id)
                 .InnerJoin((u, ur, r, rp, f) => ur.RoleId == rp.RoleId)
                 .InnerJoin((u, ur, r, rp, f) => rp.FunctionId == f.Id)
                 .Where((u, ur, r, rp, f) => u.Id == userId).ToSql();
            //.ToListAsync((u, ur, r, rp, f) => new PermssionResponse
            //{
            //    Id = f.Id,
            //    Code = f.Code,
            //    Name = f.Name
            //});
            var ada = (
                        from u in FreeSql.Select<UserInfo>()
                        join ur in FreeSql.Select<UserRoleInfo>() on u.Id equals ur.UserId
                        join r in FreeSql.Select<RoleInfo>() on ur.RoleId equals r.Id
                        join rp in FreeSql.Select<RolePermissionInfo>() on ur.RoleId equals rp.RoleId
                        join f in FreeSql.Select<FunctionInfo>() on rp.FunctionId equals f.Id
                        where u.Id == userId
                        select new PermssionResponse()
                        {
                            Id = f.Id,
                            Code = f.Code,
                            Name = f.Name
                        }).ToSql();
            var permissions = await (
                        from u in FreeSql.Select<UserInfo>()
                        join ur in FreeSql.Select<UserRoleInfo>() on u.Id equals ur.UserId
                        join r in FreeSql.Select<RoleInfo>() on ur.RoleId equals r.Id
                        join rp in FreeSql.Select<RolePermissionInfo>() on ur.RoleId equals rp.RoleId
                        join f in FreeSql.Select<FunctionInfo>() on rp.FunctionId equals f.Id
                        where u.Id == userId
                        select new PermssionResponse()
                        {
                            Id = f.Id,
                            Code = f.Code,
                            Name = f.Name
                        }).ToListAsync();
            FunctionTreeResponse functionTree = new FunctionTreeResponse()
            {
                Id = string.Empty,
                Code = "Root",
                Name = "根节点"
            };
            toUserFuntionTree(functions, functionTree, permissions);
            return functionTree.Tree;
        }



        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<BriefInfo>> GetUserPermissions(string userId)
        {
            return await (from u in FreeSql.Select<UserInfo>()
                          join ur in FreeSql.Select<UserRoleInfo>() on u.Id equals ur.UserId
                          join r in FreeSql.Select<RoleInfo>() on ur.RoleId equals r.Id
                          join rp in FreeSql.Select<RolePermissionInfo>() on ur.RoleId equals rp.RoleId
                          join f in FreeSql.Select<FunctionInfo>() on rp.FunctionId equals f.Id
                          where u.Id == userId
                          select new BriefInfo()
                          {
                              Id = f.Id,
                              Code = f.Code,
                              Path = f.Path,
                              Name = f.Name,
                              ParentId = f.ParentId
                          }).ToListAsync();
        }

        /// <summary>
        /// 初始化系统功能模块
        /// </summary>
        /// <param name="compulsory">是否强制执行</param>
        public IList<FunctionInfo> InitFunctions(bool compulsory = false)
        {
            IList<FunctionInfo> functionInfos = new List<FunctionInfo>();
            if (compulsory || !FreeSql.Select<FunctionInfo>().Any())
            {
                FreeSql.Transaction(() =>
                {
                    int count = FreeSql.Delete<FunctionInfo>().Where(o => true).ExecuteAffrows();

                    DateTime dateNow = DateTime.Now;
                    string userId = DefaultUserInfo.UserInfo.Id;
                    string userRealName = DefaultUserInfo.UserInfo.RealName;
                    foreach (var assembly in AssemblyFinder.AllAssembly)
                    {
                        var filterTypes = assembly.GetTypes().Where(o => Attribute.IsDefined(o, typeof(MoudleInfoAttribute)) && o.IsClass && !o.IsAbstract && o.IsPublic).ToList();
                        foreach (var type in filterTypes)
                        {
                            var moudleInfoAttribute = type.GetCustomAttribute<MoudleInfoAttribute>();
                            if (!moudleInfoAttribute.IsFunction || functionInfos.Where(o => o.Name == moudleInfoAttribute.Name && o.ParentId == string.Empty).Any())
                            {
                                continue;
                            }
                            var nameArr = moudleInfoAttribute.Name.Split('-', '/', '\\', '_');

                            string parentId = string.Empty;


                            var path = type.GetCustomAttribute<RouteAttribute>()?.Template ?? type.GetCustomAttribute<HttpGetAttribute>()?.Template ?? type.GetCustomAttribute<HttpPostAttribute>()?.Template ?? "";
                            foreach (var name in nameArr)
                            {
                                FunctionInfo functionInfo = new FunctionInfo()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = name,
                                    Code = string.IsNullOrEmpty(moudleInfoAttribute.Code) ? name.ToPinYin() : moudleInfoAttribute.Code,
                                    ParentId = parentId,
                                    Order = moudleInfoAttribute.Order,
                                    Path = path,
                                    AddTime = dateNow,
                                    AddUserId = userId,
                                    AddUserRealName = userRealName,
                                    UpdateTime = dateNow,
                                    UpdateUserId = userId,
                                    UpdateUserRealName = userRealName,
                                };
                                parentId = functionInfo.Id;
                                functionInfos.Add(functionInfo);
                                count = FreeSql.Insert(functionInfo).ExecuteAffrows();
                            }

                            var methods = type.GetMethods().Where(o => Attribute.IsDefined(o, typeof(MoudleInfoAttribute)) && !o.IsAbstract && o.IsPublic);
                            foreach (var method in methods)
                            {
                                var methodMoudleInfoAttribute = method.GetCustomAttribute<MoudleInfoAttribute>();
                                if (!methodMoudleInfoAttribute.IsFunction || functionInfos.Where(o => o.Name == methodMoudleInfoAttribute.Name && o.ParentId == parentId).Any())
                                {
                                    continue;
                                }
                                var methodPath = method.GetCustomAttribute<RouteAttribute>()?.Template ?? method.GetCustomAttribute<HttpGetAttribute>()?.Template ?? method.GetCustomAttribute<HttpPostAttribute>()?.Template ?? "";
                                FunctionInfo functionInfo = new FunctionInfo()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = methodMoudleInfoAttribute.Name,
                                    Code = string.IsNullOrEmpty(methodMoudleInfoAttribute.Code) ? methodMoudleInfoAttribute.Name.ToPinYin() : methodMoudleInfoAttribute.Code,
                                    ParentId = parentId,
                                    Order = moudleInfoAttribute.Order,
                                    Path = methodPath,
                                    AddTime = dateNow,
                                    AddUserId = userId,
                                    AddUserRealName = userRealName,
                                    UpdateTime = dateNow,
                                    UpdateUserId = userId,
                                    UpdateUserRealName = userRealName,
                                };
                                functionInfos.Add(functionInfo);
                                count = FreeSql.Insert(functionInfo).ExecuteAffrows();
                            }

                        }
                    }



                });
            }

            return functionInfos;
        }

        /// <summary>
        /// 初始化超级管理员用户权限
        /// </summary>
        /// <param name="compulsory">是否强制执行</param>
        public void InitAdminUserAndRole(bool compulsory = false)
        {
            if (compulsory || !FreeSql.Select<UserInfo>().Where(o => o.Id == DefaultUserInfo.UserInfo.Id).Any())
            {
                FreeSql.Transaction(() =>
                {
                    var dateNow = DateTime.Now;
                    int count = FreeSql.Delete<UserInfo>().Where(o => o.Id == DefaultUserInfo.UserInfo.Id).ExecuteAffrows();
                    count += FreeSql.Delete<RoleInfo>().Where(o => o.Id == DefaultRoleInfo.RoleInfo.Id).ExecuteAffrows();
                    count += FreeSql.Delete<UserRoleInfo>().Where(o => o.UserId == DefaultUserInfo.UserInfo.Id).ExecuteAffrows();
                    count += FreeSql.Delete<RolePermissionInfo>().Where(o => o.RoleId == DefaultRoleInfo.RoleInfo.Id).ExecuteAffrows();
                    var functionInfos = InitFunctions(compulsory);
                    //初始化超级管理员
                    UserInfo userInfo = new UserInfo()
                    {
                        Id = DefaultUserInfo.UserInfo.Id,
                        UserName = DefaultUserInfo.UserInfo.UserName,
                        Password = DefaultUserInfo.UserInfo.Password.ToMd5(),
                        RealName = DefaultUserInfo.UserInfo.RealName,
                        RealNamePin = DefaultUserInfo.UserInfo.RealName.ToPinYin(),
                        Phone = DefaultUserInfo.UserInfo.Phone,
                        ExpiredTime = dateNow.AddYears(100),
                        Remark = DefaultUserInfo.UserInfo.Remark,
                        AddTime = dateNow,
                        AddUserId = DefaultUserInfo.UserInfo.Id,
                        AddUserRealName = DefaultUserInfo.UserInfo.RealName,
                        UpdateTime = dateNow,
                        UpdateUserId = DefaultUserInfo.UserInfo.Id,
                        UpdateUserRealName = DefaultUserInfo.UserInfo.RealName,
                    };
                    count += FreeSql.Insert(userInfo).ExecuteAffrows();


                    RoleInfo roleInfo = new RoleInfo()
                    {
                        Id = DefaultRoleInfo.RoleInfo.Id,
                        Name = DefaultRoleInfo.RoleInfo.Name,
                        ParentId = string.Empty,
                        Remark = DefaultRoleInfo.RoleInfo.Remark,
                        AddTime = dateNow,
                        AddUserId = DefaultUserInfo.UserInfo.Id,
                        AddUserRealName = DefaultUserInfo.UserInfo.RealName,
                        UpdateTime = dateNow,
                        UpdateUserId = DefaultUserInfo.UserInfo.Id,
                        UpdateUserRealName = DefaultUserInfo.UserInfo.RealName,
                    };
                    count += FreeSql.Insert(roleInfo).ExecuteAffrows();

                    UserRoleInfo userRoleInfo = new UserRoleInfo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = DefaultRoleInfo.RoleInfo.Id,
                        UserId = DefaultUserInfo.UserInfo.Id,

                        AddTime = dateNow,
                        AddUserId = DefaultUserInfo.UserInfo.Id,
                        AddUserRealName = DefaultUserInfo.UserInfo.RealName,
                        UpdateTime = dateNow,
                        UpdateUserId = DefaultUserInfo.UserInfo.Id,
                        UpdateUserRealName = DefaultUserInfo.UserInfo.RealName,
                    };
                    count += FreeSql.Insert(userRoleInfo).ExecuteAffrows();
                    IList<RolePermissionInfo> rolePermissionInfos = new List<RolePermissionInfo>();
                    foreach (var funciton in functionInfos)
                    {
                        RolePermissionInfo rolePermissionInfo = new RolePermissionInfo()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FunctionId = funciton.Id,
                            RoleId = DefaultRoleInfo.RoleInfo.Id,
                            AddTime = dateNow,
                            AddUserId = DefaultUserInfo.UserInfo.Id,
                            AddUserRealName = DefaultUserInfo.UserInfo.RealName,
                            UpdateTime = dateNow,
                            UpdateUserId = DefaultUserInfo.UserInfo.Id,
                            UpdateUserRealName = DefaultUserInfo.UserInfo.RealName,
                        };
                        rolePermissionInfos.Add(rolePermissionInfo);
                        count = FreeSql.Insert(rolePermissionInfo).ExecuteAffrows();
                    }


                });

            }






        }




        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="functions"></param>
        /// <param name="functionTree"></param>
        /// <param name="permissions"></param>
        private void toUserFuntionTree(IEnumerable<FunctionInfo> functions, FunctionTreeResponse functionTree, IList<PermssionResponse> permissions)
        {
            var filterFunctions = functions.Where(o => o.ParentId == functionTree.Id).Select(o => new FunctionTreeResponse
            {
                Id = o.Id,
                Name = o.Name,
                Code = o.Code,
                Path = o.Path
            }).ToList();
            functionTree.Tree = filterFunctions;
            foreach (var item in filterFunctions)
            {
                if (permissions.Any(o => o.Id == item.Id))
                {
                    item.IsPermission = true;
                }
                toUserFuntionTree(functions, item, permissions);
            }
        }
    }
}
