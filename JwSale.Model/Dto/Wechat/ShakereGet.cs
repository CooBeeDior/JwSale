using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ShakereGetRequest : WechatRequestBase
    {
        /// <summary>
        /// 摇一摇提交包返回"buffer"
        /// </summary>
        public string buffer { get; set; }
    }
}
