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
        Task<IList<FunctionTreeResponse>> GetUserFunctions(string userId);

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="functions"></param>
        /// <param name="functionTree"></param>
        /// <param name="permissions"></param>
        void GetFuntions(IEnumerable<FunctionInfo> functions, FunctionTreeResponse functionTree, IList<PermssionResponse> permissions);




        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<BriefInfo>> GetPermissions(string userId);


        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <returns></returns>
        IList<FunctionInfo> InitFunctions();


        /// <summary>
        /// 初始化超级管理员用户权限
        /// </summary>
        void InitAdminUserAndRole();


        /// <summary>
        /// 给用户添加所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task SetUserAllPermission(string userId);



        /// <summary>
        /// 绑定微信用户信息
        /// </summary>
        /// <param name="bindWechatUser"></param>
        /// <returns></returns>
        Task<string> BindWechatUser(BindWechatUser bindWechatUser);

        /// <summary>
        /// 获取绑定微信用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        WechatUser GetWechatUser(string openId);

    }
}
