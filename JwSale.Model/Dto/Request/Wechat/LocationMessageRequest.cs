using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class LocationMessageRequest : WechatBase
    {
        /// <summary>
        /// 接收的微信ID
        /// </summary> 
        public string ToWxId { get; set; }
        /// <summary>
        /// 经度
        /// </summary> 
        public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string ClientMsgId { get; set; }
    }
}
