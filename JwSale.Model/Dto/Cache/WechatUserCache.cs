using JwSale.Model.Dto.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Cache
{
    public class WechatUserCache : WechatUser
    {
        public string SessionKey { get; set; }
    }
}
