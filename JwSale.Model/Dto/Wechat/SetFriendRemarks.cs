using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SetFriendRemarksRequest : WechatRequestBase
    {
        /// <summary>
        /// 被修改帐号
        /// </summary>
        public string wxid { get; set; }
        /// <summary>
        /// 新备注
        /// </summary>
        public string remark { get; set; }
    }
}
