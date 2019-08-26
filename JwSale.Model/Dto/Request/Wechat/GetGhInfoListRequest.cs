using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class GetGhInfoListRequest : RequestPageBase
    {
        /// <summary>
        /// 所属微信号列表
        /// </summary>
        public string WxId { get; set; }
        /// <summary>
        /// 搜索关键词 公众号Id或公众号名称
        /// </summary>
        public string KeyWords { get; set; }
    }
}
