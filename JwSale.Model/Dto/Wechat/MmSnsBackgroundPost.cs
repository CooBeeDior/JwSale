using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    /// <summary>
    /// 朋友圈背景图片
    /// </summary>
    public class MmSnsBackgroundPostRequest: WechatRequestBase
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string ClientMsgId { get; set; }


    }
}
