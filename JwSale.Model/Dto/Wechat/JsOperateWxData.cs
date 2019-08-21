using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class JsOperateWxDataRequest : WechatRequestBase
    {
        /// <summary>
        /// 小程序ID
        /// </summary>
        public string appID { get; set; }
    }
}
