using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.TaskInfo
{
    public class GetTaskInfoListRequest : RequestPageBase
    {
        /// <summary>
        /// 关键词 群名称和群Id
        /// </summary>
        public string KeyWords { get; set; }
    }
}
