using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Service
{
    public class BindWechatUser
    {
        public string HospitalId { get; set; }


        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WxOpenId { get; set; }

        /// <summary>
        /// 微信UnionId
        /// </summary>
        public string WxUnionId { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImageUrl { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
   
        public string PhoneNumer { get; set; }

        
    }
}
