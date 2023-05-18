using JwSale.Model.Dto.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JsSaleService
{
    /// <summary>
    /// 医院服务
    /// </summary>
    public interface IHospitalService : IService
    {
        /// <summary>
        /// 根据手机号获取微信用户信息
        /// </summary>
        /// <param name="phoneNumer"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        Task<WechatUser> GetWechatUser(string phoneNumer, string hospitalId);
        /// <summary>
        /// 根据Openid获取微信用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        Task<WechatUser> GetWechatUserOpenId(string openId, string hospitalId);

        /// <summary>
        /// 绑定微信用户信息
        /// </summary>
        /// <param name="bindWechatUser"></param>
        /// <returns></returns>
        Task<string> BindWechatUser(BindWechatUser bindWechatUser);

        /// <summary>
        /// 获取用户医生信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        Task<UserDoctorInfo> GetUserDoctorInfo(string openId, string hospitalId);

        /// <summary>
        /// 初始化医院信息
        /// </summary> 
        /// <returns></returns>
        int InitHospital();
    }
}
