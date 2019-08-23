using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class CardMessageRequest : WechatBase
    {
        /// <summary>
        /// 接收的微信ID
        /// </summary> 
        public string ToWxId { get; set; }
        /// <summary>
        /// 发送的微信Id
        /// </summary> 
        public string CardWxId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string CardNickName { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string CardAlias { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string ClientMsgId { get; set; }
    }
}
