using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class DelContactLabelRequest : WechatRequestBase
    {
        
        /// <summary>
        /// 获取获取标签包返回
        /// </summary>
        public string labelIdlist { get; set; }
    }
}
