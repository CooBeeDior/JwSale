using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{


    /// <summary>
    /// 群成员信息
    /// </summary>
    [Table("ChatRoomMemberInfo")]
    public class ChatRoomMemberInfo : Entity
    {

        /// <summary>
        /// 群Id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName { get; set; }


        /// <summary>
        /// 别名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }

    }
}
