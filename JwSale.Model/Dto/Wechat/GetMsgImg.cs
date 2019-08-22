using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class GetMsgImg : WechatRequestBase
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 图片大小 消息详情里中获取
        /// </summary>
        public string length { get; set;}
        /// <summary>
        /// 位置  本次要下载的数据开头位置
        /// </summary>
        public string startPos { get; set; }
        /// <summary>
        /// 消息ID 消息详情里中获取
        /// </summary>
        public string newMsgId { get; set; }


    }
}
