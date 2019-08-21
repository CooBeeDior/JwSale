using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class SearchContactRequest : WechatRequestBase
    {
        /// <summary>
        /// 搜索帐号 :非好友搜索,通常通过该接口进行好友添加,不支持(公众号,wxid)支持(QQ,手机号,微信号)
        /// </summary>
        public string user { get; set; }

  
    }
}
