using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{

    public class MpA8KeyRequest : WechatRequestBase
    {
        /// <summary>
        /// 二维码转文字后的地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 固定空
        /// </summary>
        public string cookie { get; set; }
    }
}
