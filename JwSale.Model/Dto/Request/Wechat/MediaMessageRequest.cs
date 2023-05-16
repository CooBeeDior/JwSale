using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class MediaMessageRequest : WechatBase
    {
        /// <summary>
        /// 接收的微信ID
        /// </summary> 
        public string ToWxId { get; set; }
        /// <summary>
        /// 附件Id
        /// </summary> 
        public string AttachId { get; set; }
        /// <summary>
        /// 标题
        /// </summary> 
        public string Title { get; set; }


        /// <summary>
        /// 文件大小
        /// </summary> 
        public long Length { get; set; }

        /// <summary>
        /// 文件后缀名
        /// </summary> 
        public string FileExt { get; set; }


        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string ClientMsgId { get; set; }
    }
}
