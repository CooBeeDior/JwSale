using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class GetWxFriendListRequest : RequestPageBase
    {
        /// <summary>
        /// 所有微信Id
        /// </summary>
        public string WxId { get; set; }
        /// <summary>
        /// 关键词 微信Id 微信昵称
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 性别 0:无 1:男 2：女
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 省 例如 ZheJiang
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市  例如HangZhou
        /// </summary>
        public string City { get; set; }
        

  

    }
}
