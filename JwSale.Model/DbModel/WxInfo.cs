using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{


    [Table("WxInfo")]
    public class WxInfo : Entity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }


        /// <summary>
        /// 微信账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 微信密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 微信设备信息
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }
 

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 性别 0:无 1:男 2：女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

 
    }
}
