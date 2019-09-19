using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{
    /// <summary>
    /// 公众号信息
    /// </summary>
    [Table("GhInfo")]
    public class GhInfo : Entity
    {
        /// <summary>
        /// 所属微信Id
        /// </summary>
        public string BelongWxId { get; set; }

        /// <summary>
        /// 公众号Id
        /// </summary>
        public string GhId { get; set; }

        /// <summary>
        /// 公众号昵称
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
