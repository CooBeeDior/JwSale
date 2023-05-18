using JwSale.Model;
using JwSale.Model.DbModel;
using JwSale.Model.Dto.Service;
using JwSale.Repository.Context;
using JwSale.Util;
using System;
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



        /// <summary>
        /// 获取用户医生信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public async Task<UserDoctorInfo> GetUserDoctorInfo(string openId, string hospitalId)
        {
            var result = await FreeSql.Select<UserInfo, Doctor, Epartmene, Hospital>()
                     .LeftJoin((u, d, e, h) => u.Id == d.UserId)
                     .LeftJoin((u, d, e, h) => d.EpartmeneId == e.Id)
                     .LeftJoin((u, d, e, h) => e.HospitalId == h.Id)
                     .Where((u, d, e, h) => u.WxOpenId == openId && d.BelongHospitalId == hospitalId)
                     .ToOneAsync((u, d, e, h) => new UserDoctorInfo()
                     {
                         UserId = u.Id,
                         UserName = u.UserName,
                         Phone = u.Phone,
                         Email = u.Email,
                         RealName = u.RealName,
                         Sex = u.Sex,
                         BirthDay = u.BirthDay,
                         IdCard = u.IdCard,
                         RealNamePin = u.RealNamePin,
                         Qq = u.Qq,
                         TelPhone = u.TelPhone,
                         PositionName = u.PositionName,
                         Province = u.Province,
                         City = u.City,
                         Area = u.Area,
                         Address = u.Address,
                         HeadImageUrl = u.HeadImageUrl,
                         Remark = u.Remark,
                         DoctorId = d.Id,
                         Professional = d.Professional,
                         EpartmeneId = e.Id,
                         EpartmeneName = e.Name,
                         HospitalId = h.Id,
                         HospitalCode = h.Code,
                         HospitalName = h.Name,
                         HospitalFullName = h.FullName
                     });
            return result;

        }

        /// <summary>
        /// 初始化医院信息
        /// </summary>
        /// <returns></returns>
        public int InitHospital()
        {
            int count = 0;
            if (!FreeSql.Select<Hospital>().Any())
            {
                var dateNow = DateTime.Now;
                Hospital hospital = new Hospital()
                {
                    Id = DefaultHospital.Hospital.Id,
                    Name = DefaultHospital.Hospital.Name,
                    FullName = DefaultHospital.Hospital.FullName,
                    Code = DefaultHospital.Hospital.Code,
                    Remark = DefaultHospital.Hospital.Remark,
                    AddTime = dateNow,
                    AddUserId = DefaultUserInfo.UserInfo.Id,
                    AddUserRealName = DefaultUserInfo.UserInfo.RealName,
                    UpdateTime = dateNow,
                    UpdateUserId = DefaultUserInfo.UserInfo.Id,
                    UpdateUserRealName = DefaultUserInfo.UserInfo.RealName,
                };
                count = FreeSql.Insert<Hospital>(hospital).ExecuteAffrows();
            }

            return count;

        }
    }
}
