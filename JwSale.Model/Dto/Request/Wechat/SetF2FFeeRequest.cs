using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class SetF2FFeeRequest : WechatBase
    {
        /// <summary>
        /// 金额 分
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string Desc { get; set; }

    }
}
