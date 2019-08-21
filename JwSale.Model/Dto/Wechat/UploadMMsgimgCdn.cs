using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class UploadMMsgimgCdnRequest : WechatRequestBase
    {
        /// <summary>
        /// 接收人wxid
        /// </summary>
        public string recv_uin { get; set; }

        /// <summary>
        /// 图片内容  图片XML
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
