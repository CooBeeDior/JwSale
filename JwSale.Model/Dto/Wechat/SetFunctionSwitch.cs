using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    /// <summary>
    /// 设置好友加我验证
    /// </summary>
    public class SetFunctionSwitchRequest : WechatRequestBase
    {
        /// <summary>
        /// 功能Id 4:加我为朋友是需要验证 7向我推荐通讯录朋友 ,
        /// </summary>
        public string functionid { get; set; }

        /// <summary>
        /// 1， 2 开启或关闭 
        /// </summary>
        public string switchvalue { get; set; }
    }


  
}
