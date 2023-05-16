using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{
    /// <summary>
    /// 拉群微信Id
    /// </summary>
    [Table("WxNoTaskInfo")]
    public class WxNoTaskInfo : Entity
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid ChatRoomTaskInfoId { get; set; }


        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }
    }
}
