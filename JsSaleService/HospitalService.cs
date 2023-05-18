using JwSale.Model;
using JwSale.Model.DbModel;
using JwSale.Model.Dto.Service;
using JwSale.Repository.Context;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JsSaleService
{
    /// <summary>
    /// 医院服务
    /// </summary>
    public class HospitalService : Service, IHospitalService
    {
        public HospitalService(JwSaleDbContext dbContext, IFreeSql freeSql) : base(dbContext, freeSql)
        {

        }
        /// <summary>
        /// 根据手机号获取微信用户信息
        /// </summary>
        /// <param name="phoneNumer"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public async Task<WechatUser> GetWechatUser(string phoneNumer, string hospitalId)
        {

            var wechatUser = await FreeSql.Select<Doctor, UserInfo>().LeftJoin((d, u) => d.UserId == u.Id && d.BelongHospitalId == hospitalId)
                   .Where((d, u) => u.Phone == phoneNumer).ToOneAsync((d, u) =>
                  new WechatUser
                  {
                      DoctorId = d.Id,
                      EpartmeneId = d.EpartmeneId,
                      DoctorName = u.RealName ?? u.UserName,
                      HospitalId = d.BelongHospitalId,
                      Phone = u.Phone,
                      UserId = u.Id,
                      WxOpenId = u.WxOpenId,
                      WxUnionId = u.WxUnionId
                  });
            return wechatUser;
        }


        /// <summary>
        /// 根据Openid获取微信用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public async Task<WechatUser> GetWechatUserOpenId(string openId, string hospitalId)
        {

            var wechatUser = await FreeSql.Select<Doctor, UserInfo>().LeftJoin((d, u) => d.UserId == u.Id && d.BelongHospitalId == hospitalId)
                   .Where((d, u) => u.WxOpenId == openId).ToOneAsync((d, u) =>
                  new WechatUser
                  {
                      DoctorId = d.Id,
                      EpartmeneId = d.EpartmeneId,
                      DoctorName = u.RealName ?? u.UserName,
                      HospitalId = d.BelongHospitalId,
                      Phone = u.Phone,
                      UserId = u.Id,
                      WxOpenId = u.WxOpenId,
                      WxUnionId = u.WxUnionId
                  });
            return wechatUser;
        }

        /// <summary>
        /// 绑定微信用户信息
        /// </summary>
        /// <param name="bindWechatUser"></param>
        /// <returns></returns>
        public async Task<string> BindWechatUser(BindWechatUser bindWechatUser)
        {
            var userInfo = await FreeSql.Select<UserInfo>().Where(o => o.Phone == bindWechatUser.PhoneNumer).ToOneAsync();
            if (userInfo != null)
            {
                userInfo.WxOpenId = bindWechatUser.WxOpenId;
                userInfo.WxUnionId = bindWechatUser.WxUnionId;
                userInfo.HeadImageUrl = bindWechatUser.HeadImageUrl;
                userInfo.WxNo = bindWechatUser.WxNo;
                userInfo.UpdateTime = DateTime.Now;
                var count = FreeSql.Update<UserInfo>(userInfo).ExecuteAffrowsAsync();
            }
            return userInfo.Id;
        }
    }
}
