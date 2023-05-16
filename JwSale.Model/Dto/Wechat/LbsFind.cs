using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class LbsFindRequest : WechatRequestBase
    {
        /// <summary>
        /// 	固定"1"
        /// </summary>
        public string opCode { get; set; } = "1";

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }
    }
}
