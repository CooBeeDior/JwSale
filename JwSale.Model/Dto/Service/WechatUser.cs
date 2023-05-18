using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Service
{
    public class WechatUser
    {
        public string DoctorId { get; set; }

        public string DoctorName { get; set; }


        /// <summary>
        /// 所属医院
        /// </summary>
        public string HospitalId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string EpartmeneId { get; set; }

        /// <summary>
        /// 关联用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WxOpenId { get; set; }

        /// <summary>
        /// 微信UnionId
        /// </summary>
        public string WxUnionId { get; set; }
    }
}
