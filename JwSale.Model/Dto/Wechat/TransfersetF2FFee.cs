using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class TransferSetF2FFeeRequest : WechatRequestBase
    {

        /// <summary>
        /// 收固定款信息信息 金额单(分)）
        /// desc=vx%e8%ae%be%e7%bd%ae%e7%9a%84%e9%87%91%e9%a2%9d&fee=10000&fee_type=1
        /// </summary>
        public string describe { get; set; }
    }
}
