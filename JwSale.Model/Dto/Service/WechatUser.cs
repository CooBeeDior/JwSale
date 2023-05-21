using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Service
{
    public class WechatUser
    {  

        /// <summary>
        /// 关联用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 真是姓名
        /// </summary>
        public string RealName { get; set; }
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
