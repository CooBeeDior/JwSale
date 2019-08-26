using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{
    /// <summary>
    /// 群信息
    /// </summary>
    [Table("ChatRoomInfo")]
    public class ChatRoomInfo : Entity
    {
        /// <summary>
        /// 所属微信Id
        /// </summary>
        public string BelongWxId { get; set; }

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
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 群群主微信Id
        /// </summary>
        public string  OwnerWxId { get; set; }

        /// <summary>
        /// 群群主微信昵称
        /// </summary>
        public string OwnerWxNickName { get; set; }

        /// <summary>
        /// 群群主头像
        /// </summary>
        public string OwnerWxHeadImgUrl { get; set; }

        /// <summary>
        /// 群成员最大数量
        /// </summary>
        public int ChatroomMaxCount { get; set; }

        /// <summary>
        /// 当前群成员数量
        /// </summary>
        public int ChatRoomMemberCount { get; set; }


    }
}
