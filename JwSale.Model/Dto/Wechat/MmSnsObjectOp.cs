using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class MmSnsObjectOpRequest : WechatRequestBase
    {
        /// <summary>
        /// 朋友圈ID 操作的朋友圈动态id
        /// </summary>
        public string maxid { get; set; } = "0";

        /// <summary>
        /// 评论ID 	操作的commentId 删除评论时需要
        /// </summary>
        public string commentId { get; set; }

        /// <summary>
        /// 操作类型 1删除朋友圈2设为隐私3设为公开4删除评论5取消点赞
        /// </summary>
        public string type { get; set; }
    }
}
