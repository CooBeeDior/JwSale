using JwSale.Model;
using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Response.UserRole;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsSaleService
{
    public interface IUserService : IService
    {
        Task<IList<FunctionTree>> GetUserFunctions(Guid userId);

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="functions"></param>
        /// <param name="functionTree"></param>
        /// <param name="permissions"></param>
        void GetFuntions(IEnumerable<FunctionInfo> functions, FunctionTree functionTree, IList<Permssion> permissions);




        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<BriefInfo>> GetPermissions(Guid userId);


        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <returns></returns>
        IList<FunctionInfo> InitFunctions();


        /// <summary>
        /// 初始化超级管理员用户权限
        /// </summary>
        void InitAdminUserAndRole();
    }
}
