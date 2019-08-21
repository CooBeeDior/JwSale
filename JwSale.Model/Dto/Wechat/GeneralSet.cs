using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class GeneralSetRequest : WechatRequestBase
    {
        /// <summary>
        /// 微信号
        /// </summary>
        public string setValue { get; set; }


    }
}
