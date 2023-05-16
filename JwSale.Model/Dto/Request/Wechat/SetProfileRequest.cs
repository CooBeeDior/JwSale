using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class SetProfileRequest:WechatBase
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 省 ZheJiang
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 性别 0:无 1:男 2：女
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 城市 HangZhou
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 国家 CN
        /// </summary>
        public string country { get; set; } = "CN";
    }
}
