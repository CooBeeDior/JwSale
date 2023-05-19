using JwSale.Model;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Response.UserRole;
using JwSale.Model.Enums;
using JwSale.Repository.Context;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly IJwSaleRepository<UserInfo> _repositoryUserInfo;
        private readonly IJwSaleRepository<FunctionInfo> _repositoryFunctionInfo;
        private readonly IJwSaleRepository<RoleInfo> _repositoryRoleInfo;
        private readonly IJwSaleRepository<UserRoleInfo> _repositoryUserRoleInfo;

        private readonly IJwSaleRepository<RolePermissionInfo> _repositoryRolePermissionInfo;
        private readonly IJwSaleRepository<UserPermissionInfo> _repositoryUserPermissionInfo;
        private readonly IJwSaleUnitOfWork _jwSaleUnitOfWork;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(JwSaleDbContext jwSaleDbContext, IFreeSql freeSql, IJwSaleRepository<UserInfo> repositoryUserInfo,
            IJwSaleRepository<FunctionInfo> repositoryFunctionInfo, IJwSaleRepository<RoleInfo> repositoryRoleInfo,
            IJwSaleRepository<UserRoleInfo> repositoryUserRoleInfo, IJwSaleRepository<RolePermissionInfo> repositoryRolePermissionInfo,
             IJwSaleRepository<UserPermissionInfo> repositoryUserPermissionInfo, IJwSaleUnitOfWork jwSaleUnitOfWork,
             IHttpContextAccessor httpContextAccessor) : base(jwSaleDbContext, freeSql)
        {

            _repositoryUserInfo = repositoryUserInfo;
            _repositoryFunctionInfo = repositoryFunctionInfo;
            _repositoryRoleInfo = repositoryRoleInfo;
            _repositoryUserRoleInfo = repositoryUserRoleInfo;
            _repositoryRolePermissionInfo = repositoryRolePermissionInfo;
            _repositoryUserPermissionInfo = repositoryUserPermissionInfo;
            _jwSaleUnitOfWork = jwSaleUnitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IList<FunctionTreeResponse>> GetUserFunctions(string userId)
        {

            var functions = DbContext.FunctionInfos.OrderBy(o => o.Order).AsEnumerable();
            var permissions = await (
                        from u in DbContext.UserInfos.AsNoTracking()
                        join ur in DbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
                        join r in DbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
                        join rp in DbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
                        join f in DbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
                        where u.Id == userId
                        select new PermssionResponse()
                        {
                            Id = f.Id,
                            Code = f.Code,
                            Name = f.Name
                        }).Union(
                            from u in DbContext.UserInfos.AsNoTracking()
                            join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                            join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                            where u.Id == userId && up.Type == (short)PermissionType.Increase
                            select new PermssionResponse()
                            {
                                Id = f.Id,
                                Code = f.Code,
                                Name = f.Name
                            }).Except(
                              from u in DbContext.UserInfos.AsNoTracking()
                              join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                              join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                              where u.Id == userId && up.Type == (short)PermissionType.Decut
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
            GetFuntions(functions, functionTree, permissions);
            return functionTree.Tree;
        }



        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="functions"></param>
        /// <param name="functionTree"></param>
        /// <param name="permissions"></param>
        public void GetFuntions(IEnumerable<FunctionInfo> functions, FunctionTreeResponse functionTree, IList<PermssionResponse> permissions)
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
                GetFuntions(functions, item, permissions);
            }
        }



        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<BriefInfo>> GetPermissions(string userId)
        {

            var ss = from u in DbContext.UserInfos.AsNoTracking()
                     join ur in DbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
                     join r in DbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
                     join rp in DbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
                     join f in DbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
                     where u.Id == userId
                     select new BriefInfo()
                     {
                         Id = f.Id,
                         Code = f.Code,
                         Path = f.Path,
                         Name = f.Name,
                         ParentId = f.ParentId
                     };
            var kk = ss.ToList();

            var adda = from u in DbContext.UserInfos.AsNoTracking()
                       join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                       join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                       where u.Id == userId && up.Type == (short)PermissionType.Decut
                       select new BriefInfo()
                       {
                           Id = f.Id,
                           Code = f.Code,
                           Path = f.Path,
                           Name = f.Name,
                           ParentId = f.ParentId
                       };
            var faafaf = adda.ToList();


            var dfa = (from u in DbContext.UserInfos.AsNoTracking()
                       join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                       join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                       where u.Id == userId && up.Type == (short)PermissionType.Decut
                       select new BriefInfo()
                       {
                           Id = f.Id,
                           Code = f.Code,
                           Path = f.Path,
                           Name = f.Name,
                           ParentId = f.ParentId
                       }).ToList();



            return await (
                 from u in DbContext.UserInfos.AsNoTracking()
                 join ur in DbContext.UserRoleInfos.AsNoTracking() on u.Id equals ur.UserId
                 join r in DbContext.RoleInfos.AsNoTracking() on ur.RoleId equals r.Id
                 join rp in DbContext.RolePermissionInfos.AsNoTracking() on ur.RoleId equals rp.RoleId
                 join f in DbContext.FunctionInfos.AsNoTracking() on rp.FunctionId equals f.Id
                 where u.Id == userId
                 select new BriefInfo()
                 {
                     Id = f.Id,
                     Code = f.Code,
                     Path = f.Path,
                     Name = f.Name,
                     ParentId = f.ParentId
                 }).Union(
                     from u in DbContext.UserInfos.AsNoTracking()
                     join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                     join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                     where u.Id == userId && up.Type == (short)PermissionType.Increase
                     select new BriefInfo()
                     {
                         Id = f.Id,
                         Code = f.Code,
                         Path = f.Path,
                         Name = f.Name,
                         ParentId = f.ParentId
                     }).Except(
                       from u in DbContext.UserInfos.AsNoTracking()
                       join up in DbContext.UserPermissionInfos.AsNoTracking() on u.Id equals up.UserId
                       join f in DbContext.FunctionInfos.AsNoTracking() on up.FunctionId equals f.Id
                       where u.Id == userId && up.Type == (short)PermissionType.Decut
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
        /// 初始化模块
        /// </summary>
        /// <returns></returns>
        public IList<FunctionInfo> InitFunctions()
        {
            _jwSaleUnitOfWork.BeginOrUseTransaction();
            _repositoryFunctionInfo.Delete(s => true);

            IList<FunctionInfo> functionInfos = new List<FunctionInfo>();

            DateTime dateNow = DateTime.Now;
            string userId = DefaultUserInfo.UserInfo.Id;
            string userRealName = DefaultUserInfo.UserInfo.RealName;
            foreach (var assembly in AssemblyFinder.AllAssembly)
            {
                var filterTypes = assembly.GetTypes().Where(o => Attribute.IsDefined(o, typeof(MoudleInfoAttribute)) && o.IsClass && !o.IsAbstract && o.IsPublic).ToList();
                foreach (var type in filterTypes)
                {
                    var moudleInfoAttribute = type.GetCustomAttribute<MoudleInfoAttribute>();
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
                    }

                    var methods = type.GetMethods().Where(o => Attribute.IsDefined(o, typeof(MoudleInfoAttribute)) && !o.IsAbstract && o.IsPublic);
                    foreach (var method in methods)
                    {
                        var methodMoudleInfoAttribute = method.GetCustomAttribute<MoudleInfoAttribute>();
                        if (!methodMoudleInfoAttribute.IsFunction || functionInfos.Where(o => o.Name == methodMoudleInfoAttribute.Name && o.ParentId == parentId).Count() > 0)
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
                    }

                }
            }
            _repositoryFunctionInfo.BatchCreate(functionInfos);

            _jwSaleUnitOfWork.Commit();
            return functionInfos;

        }

        /// <summary>
        /// 初始化超级管理员用户权限
        /// </summary>
        public void InitAdminUserAndRole()
        {
            _jwSaleUnitOfWork.BeginOrUseTransaction();

            DateTime dateNow = DateTime.Now;
            string userId = DefaultUserInfo.UserInfo.Id;
            string userRealName = DefaultUserInfo.UserInfo.RealName;


            if (_repositoryUserInfo.Count(o => o.Id == userId) == 0)
            {
                var functionInfos = InitFunctions();
                //初始化超级管理员
                UserInfo userInfo = new UserInfo()
                {
                    Id = userId,
                    UserName = DefaultUserInfo.UserInfo.UserName,
                    Password = DefaultUserInfo.UserInfo.Password.ToMd5(),
                    RealName = userRealName,
                    RealNamePin = userRealName.ToPinYin(),
                    Phone = DefaultUserInfo.UserInfo.Phone,
                    ExpiredTime = dateNow.AddYears(100),

                    AddTime = dateNow,
                    AddUserId = userId,
                    AddUserRealName = userRealName,
                    UpdateTime = dateNow,
                    UpdateUserId = userId,
                    UpdateUserRealName = userRealName,
                };
                _repositoryUserInfo.Create(userInfo);

                //RoleInfo roleInfo = new RoleInfo()
                //{
                //    Id = Guid.NewGuid(),
                //    Name = "超级管理员组",
                //    ParentId = Guid.Empty,

                //    AddTime = dateNow,
                //    AddUserId = userId,
                //    AddUserRealName = userRealName,
                //    UpdateTime = dateNow,
                //    UpdateUserId = userId,
                //    UpdateUserRealName = userRealName,
                //};
                //_repositoryRoleInfo.Create(roleInfo);



                //UserRoleInfo userRoleInfo = new UserRoleInfo()
                //{
                //    Id = Guid.NewGuid(),
                //    UserId = userId,
                //    RoleId = roleInfo.Id,

                //    AddTime = dateNow,
                //    AddUserId = userId,
                //    AddUserRealName = userRealName,
                //    UpdateTime = dateNow,
                //    UpdateUserId = userId,
                //    UpdateUserRealName = userRealName,

                //};
                //_repositoryUserRoleInfo.Create(userRoleInfo);


                //IList<RolePermissionInfo> rolePermissionInfos = new List<RolePermissionInfo>();

                _repositoryUserPermissionInfo.Delete(o => o.UserId == userId);
                IList<UserPermissionInfo> userPermissionInfos = new List<UserPermissionInfo>();
                foreach (var funciton in functionInfos)
                {
                    //RolePermissionInfo rolePermissionInfo = new RolePermissionInfo()
                    //{
                    //    Id = Guid.NewGuid(),
                    //    RoleId = roleInfo.Id,
                    //    FunctionId = funciton.Id,

                    //    AddTime = dateNow,
                    //    AddUserId = userId,
                    //    AddUserRealName = userRealName,
                    //    UpdateTime = dateNow,
                    //    UpdateUserId = userId,
                    //    UpdateUserRealName = userRealName,
                    //};
                    //rolePermissionInfos.Add(rolePermissionInfo);

                    UserPermissionInfo userPermissionInfo = new UserPermissionInfo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        FunctionId = funciton.Id,
                        Type = 0,

                        AddTime = dateNow,
                        AddUserId = userId,
                        AddUserRealName = userRealName,
                        UpdateTime = dateNow,
                        UpdateUserId = userId,
                        UpdateUserRealName = userRealName,
                    };
                    userPermissionInfos.Add(userPermissionInfo);
                }
                //_repositoryRolePermissionInfo.BatchCreate(rolePermissionInfos);
                _repositoryUserPermissionInfo.BatchCreate(userPermissionInfos);
            }





            _jwSaleUnitOfWork.Commit();

        }

        /// <summary>
        /// 给用户添加所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task SetUserAllPermission(string userId)
        {
            _jwSaleUnitOfWork.BeginOrUseTransaction();
            var functionInfos = await DbContext.FunctionInfos.ToListAsync();

            await DbContext.UserPermissionInfos.Where(o => o.UserId == userId).DeleteAsync();


            IList<UserPermissionInfo> userPermissionInfos = new List<UserPermissionInfo>();

            var currentUserInfo = _httpContextAccessor.HttpContext.Items[CacheKeyHelper.CURRENTUSER] as UserInfo;
            foreach (var funciton in functionInfos)
            {
                UserPermissionInfo userPermissionInfo = new UserPermissionInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    FunctionId = funciton.Id,
                    Type = 0,

                    AddTime = DateTime.Now,
                    AddUserId = currentUserInfo?.Id ?? DefaultUserInfo.UserInfo.Id,
                    AddUserRealName = currentUserInfo?.RealName ?? DefaultUserInfo.UserInfo.RealName,
                    UpdateTime = DateTime.Now,
                    UpdateUserId = currentUserInfo?.Id ?? DefaultUserInfo.UserInfo.Id,
                    UpdateUserRealName = currentUserInfo?.RealName ?? DefaultUserInfo.UserInfo.RealName,
                };
                userPermissionInfos.Add(userPermissionInfo);
            }
            await DbContext.AddRangeAsync(userPermissionInfos);

            await DbContext.SaveChangesAsync();
            _jwSaleUnitOfWork.Commit();

        }
    }
}
