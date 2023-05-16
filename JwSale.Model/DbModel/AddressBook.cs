using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JwSale.Model
{

    /// <summary>
    /// 通讯录信息
    /// </summary>
    [Table("AddressBook")]
    public class AddressBook : Entity
    {

        /// <summary>
        /// 所属微信Id
        /// </summary>
        public string BelongWxId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string WxNickName { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string WxHeadImgUrl{ get; set; }

        /// <summary>
        /// 状态 0:导入成功  2：打招呼成功  4:通过好友成功  8：拉群成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 群Id
        /// </summary>
        public string ChatRoomId { get; set; }

        /// <summary>
        /// 群名称
        /// </summary>
        public string ChatRoomName { get; set; }



    }
}