using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{

    /// <summary>
    /// 任务列表
    /// </summary>
    [Table("ChatRoomTaskInfo")]
    public class ChatRoomTaskInfo : Entity
    {

        /// <summary>
        /// 群Id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string ChatRoomName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string ChatRoomHeadImgUrl { get; set; }


        /// <summary>
        /// 当前人数
        /// </summary>
        public int CurrentMemberCount { get; set; }


        /// <summary>
        /// 目标人数
        /// </summary>
        public int TargetMemberCount { get; set; }

        /// <summary>
        /// 移交微信Id
        /// </summary>
        public string ToWxId { get; set; }

        /// <summary>
        /// 移交微信昵称
        /// </summary>
        public string ToWxNickName { get; set; }

        /// <summary>
        /// 移交微信头像
        /// </summary>
        public string ToWxHeadImgUrl { get; set; }


    }
}
