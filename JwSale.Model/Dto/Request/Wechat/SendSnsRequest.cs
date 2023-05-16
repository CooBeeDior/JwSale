using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class SendSnsRequest : WechatBase
    {
        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }
        /// <summary>
        /// 0:文字 1：图片 2视频 3：链接
        /// </summary>
        public int Type { get; set; }
 

        /// <summary>
        /// media列表
        /// </summary>
        public IList<MediaInfo> MediaInfos { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容的链接
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 内容
        /// </summary>     
        public string Content { get; set; }


        public string ClientMsgId { get; set; }
    }

    public class MediaInfo
    {

        /// <summary>
        /// 数据链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 高度
        /// </summary> 
        public decimal Width { get; set; } = 300;
        /// <summary>
        /// 宽度
        /// </summary>
        public decimal Height { get; set; } = 300;
        /// <summary>
        /// 总大小
        /// </summary>
        public decimal TotalSize { get; set; }


    }
}
