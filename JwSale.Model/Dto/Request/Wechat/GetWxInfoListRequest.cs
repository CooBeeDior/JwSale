using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class GetWxInfoListRequest : RequestPageBase
    {
        /// <summary>
        /// 搜索关键词 wxid和nickname
        /// </summary>
        public string KeyWords { get; set; }
    }
}
