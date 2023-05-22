using JwSale.Model;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Response.UserRole;
using JwSale.Model.Dto.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsSaleService
{
    public interface IUserService : IService
    {
        /// <summary>
        /// 获取用户权限功能树
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<FunctionTreeResponse>> GetUserPermissionTree(string userId);




        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<BriefInfo>> GetUserPermissions(string userId);

        /// <summary>
        /// 初始化系统功能模块
        /// </summary>
        /// <param name="compulsory">是否强制执行</param>
        IList<FunctionInfo> InitFunctions(bool compulsory = false);


        /// <summary>
        /// 初始化超级管理员用户权限
        /// </summary>
        /// <param name="compulsory">是否强制执行</param>
        void InitAdminUserAndRole(bool compulsory = false); 


    }


}
