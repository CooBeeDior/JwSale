using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class SetNickNameRequest : WechatBase
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
