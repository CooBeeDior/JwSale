using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class MmSnsCommentRequest : WechatRequestBase
    {
        /// <summary>
        /// 要点评的对象 好友时填他wxid反之填自己的wxid
        /// </summary>
        public string wxid { get; set; }

        /// <summary>
        /// 朋友圈ID 操作的朋友圈动态id
        /// </summary>
        public string maxid { get; set; }

        /// <summary>
        /// 朋友圈操作类型 1点赞2评论
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 评论内容 点赞时为空
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 消息ID	每次发消息自增1，发送成功会返回该值 【必须是正整数】
        /// </summary>
        public string clientmsgid { get; set; }
    }
}
