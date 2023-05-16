using System.Collections.Generic;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class AppMessageRequest : WechatBase
    { 
        /// <summary>
       /// 接收的微信ID
       /// </summary> 
        public string ToWxId { get; set; }

        /// <summary>
        /// appId
        /// </summary> 
        public string AppId { get; set; }

        /// <summary>
        /// 标题
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        public string Desc { get; set; }

        /// <summary>
        /// app类型 3：音乐  4：小app  5：大app
        /// </summary>
        public int Type { get; set; }


        public int ShowType { get; set; } = 0;

        /// <summary>
        /// 链接
        /// </summary> 
        public string Url { get; set; }

        /// <summary>
        /// 数据Url
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary> 
        public string ThumbUrl { get; set; }

        /// <summary>
        /// 消息ID 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string ClientMsgId { get; set; }

    }
}
