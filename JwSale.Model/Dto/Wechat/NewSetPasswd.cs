using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class NewSetPasswdRequest : WechatRequestBase
    {
        /// <summary>
        /// 新密码
        /// </summary>
        public string pass { get; set; }

        /// <summary>
        /// ticket
        /// </summary>
        public string ticket { get; set; }

    }
}
