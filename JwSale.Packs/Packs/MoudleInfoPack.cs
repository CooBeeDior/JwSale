using JwSale.Model;
using JwSale.Packs.Attributes;
using JwSale.Packs.Pack;
using JwSale.Packs.Util;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using JwSale.Util.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JwSale.Packs.Packs
{
    [Pack("MoudleInfo模块")]
    public class MoudleInfoPack : JwSalePack
    {
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        protected override void UsePack(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetService<IJwSaleUnitOfWork>();

                unitOfWork.BeginOrUseTransaction();
                //初始化系统模块信息
                var repositoryFunctionInfo = scope.ServiceProvider.GetService<IJwSaleRepository<FunctionInfo>>();
                if (repositoryFunctionInfo.Count() == 0)
                {
                    DateTime dateNow = DateTime.Now;
                    Guid userId = DefaultUserInfo.UserInfo.Id;
                    string userRealName = DefaultUserInfo.UserInfo.RealName;
                    IList<FunctionInfo> functionInfos = new List<FunctionInfo>();
                    foreach (var assembly in AssemblyFinder.AllAssembly)
                    {
                        var filterTypes = assembly.GetTypes().Where(o => Attribute.IsDefined(o, typeof(MoudleInfoAttribute)) && o.IsClass && !o.IsAbstract && o.IsPublic).ToList();
                        foreach (var type in filterTypes)
                        {
                            var moudleInfoAttribute = type.GetCustomAttribute<MoudleInfoAttribute>();
                            var nameArr = moudleInfoAttribute.Name.Split('-', '/', '\\', '_');

                            Guid parentId = Guid.Empty;


                            var path = type.GetCustomAttribute<RouteAttribute>()?.Template ?? type.GetCustomAttribute<HttpGetAttribute>()?.Template ?? type.GetCustomAttribute<HttpPostAttribute>()?.Template ?? "";
                            foreach (var name in nameArr)
                            {
                                FunctionInfo functionInfo = new FunctionInfo()
                                {
                                    Id = Guid.NewGuid(),
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
                                    Id = Guid.NewGuid(),
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
                    repositoryFunctionInfo.BatchCreate(functionInfos);


                    var repositoryUserInfo = scope.ServiceProvider.GetService<IJwSaleRepository<UserInfo>>();
                    if (repositoryUserInfo.Count() == 0)
                    {
                        //初始化超级管理员
                        UserInfo userInfo = new UserInfo()
                        {
                            Id = userId,
                            UserName = DefaultUserInfo.UserInfo.UserName,
                            Password = DefaultUserInfo.UserInfo.Password.ToMd5(),
                            RealName = userRealName,
                            RealNamePin = userRealName.ToPinYin(),

                            ExpiredTime = dateNow.AddYears(100),

                            AddTime = dateNow,
                            AddUserId = userId,
                            AddUserRealName = userRealName,
                            UpdateTime = dateNow,
                            UpdateUserId = userId,
                            UpdateUserRealName = userRealName,
                        };
                        repositoryUserInfo.Create(userInfo);


                        var repositoryRoleInfo = scope.ServiceProvider.GetService<IJwSaleRepository<RoleInfo>>();
                        RoleInfo roleInfo = new RoleInfo()
                        {
                            Id = Guid.NewGuid(),
                            Name = "超级管理员组",
                            ParentId = Guid.Empty,

                            AddTime = dateNow,
                            AddUserId = userId,
                            AddUserRealName = userRealName,
                            UpdateTime = dateNow,
                            UpdateUserId = userId,
                            UpdateUserRealName = userRealName,
                        };
                        repositoryRoleInfo.Create(roleInfo);


                        var repositoryUserRoleInfo = scope.ServiceProvider.GetService<IJwSaleRepository<UserRoleInfo>>();
                        UserRoleInfo userRoleInfo = new UserRoleInfo()
                        {
                            Id = Guid.NewGuid(),
                            UserId = userInfo.Id,
                            RoleId = roleInfo.Id,

                            AddTime = dateNow,
                            AddUserId = userId,
                            AddUserRealName = userRealName,
                            UpdateTime = dateNow,
                            UpdateUserId = userId,
                            UpdateUserRealName = userRealName,

                        };
                        repositoryUserRoleInfo.Create(userRoleInfo);

                        var repositoryRolePermissionInfo = scope.ServiceProvider.GetService<IJwSaleRepository<RolePermissionInfo>>();
                        var repositoryUserPermissionInfo = scope.ServiceProvider.GetService<IJwSaleRepository<UserPermissionInfo>>();

                        IList<RolePermissionInfo> rolePermissionInfos = new List<RolePermissionInfo>();
                        IList<UserPermissionInfo> userPermissionInfos = new List<UserPermissionInfo>();
                        foreach (var funciton in functionInfos)
                        {
                            RolePermissionInfo rolePermissionInfo = new RolePermissionInfo()
                            {
                                Id = Guid.NewGuid(),
                                RoleId = roleInfo.Id,
                                FunctionId = funciton.Id,

                                AddTime = dateNow,
                                AddUserId = userId,
                                AddUserRealName = userRealName,
                                UpdateTime = dateNow,
                                UpdateUserId = userId,
                                UpdateUserRealName = userRealName,
                            };
                            rolePermissionInfos.Add(rolePermissionInfo);

                            UserPermissionInfo userPermissionInfo = new UserPermissionInfo()
                            {
                                Id = Guid.NewGuid(),
                                UserId = userInfo.Id,
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
                        repositoryRolePermissionInfo.BatchCreate(rolePermissionInfos);
                        repositoryUserPermissionInfo.BatchCreate(userPermissionInfos);
                    }
                }
                //--SELECT * FROM dbo.FunctionInfo
                //--SELECT* FROM UserInfo
                //--SELECT* FROM RoleInfo
                //--SELECT* FROM UserRoleInfo
                //--SELECT* FROM RolePermissionInfo
                //--SELECT* FROM UserPermissionInfo

                //--DELETE FROM dbo.FunctionInfo
                //--DELETE FROM UserInfo
                //--DELETE FROM RoleInfo
                //--DELETE FROM UserRoleInfo
                //--DELETE FROM RolePermissionInfo
                //--DELETE FROM UserPermissionInfo
                unitOfWork.Commit();



            }
        }
    }
}
