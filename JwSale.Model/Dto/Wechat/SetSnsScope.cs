using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SetSnsScopeRequest : WechatRequestBase
    {
        /// <summary>
        /// //2177 :全部 2689 :半年 3201: 三天
        /// </summary>
        public string snsFlagEx { get; set; }
    }
}
