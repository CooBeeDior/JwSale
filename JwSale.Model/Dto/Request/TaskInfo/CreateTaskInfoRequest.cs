using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.TaskInfo
{
    public class CreateTaskInfoRequest
    {
        /// <summary>
        /// 群Id
        /// </summary>
        public string ChatRoomId { get; set; } 


        /// <summary>
        /// 目标人数
        /// </summary>
        public int TargetMemberCount { get; set; }

        /// <summary>
        /// 移交微信Id
        /// </summary>
        public string ToWxId { get; set; }


        /// <summary>
        /// 打群的微信Id列表
        /// </summary>
        public IList<string> WxIds { get; set; }

        /// <summary>
        /// 状态 0：未开启  1：开启
        /// </summary>
        public int Status { get; set; }
    }
}
