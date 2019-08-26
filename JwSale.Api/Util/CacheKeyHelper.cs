﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Util
{
    public class CacheKeyHelper
    {
        public static string GetUserTempTokenKey(string tempToken)
        {
            return $"wechat/temptoken/{tempToken}";
        }
        public static string GetUserTokenKey(string token)
        {
            return $"wechat/temptoken/{token}";
        }

        public static string GetWechatKey(string wxid)
        {
            return $"wechat/wxid/{wxid}";

        }
        public static string GetHttpContextUserKey()
        {
            return "currentUser";
        }
    }
}
