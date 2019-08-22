using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Util
{
    public class CacheKeyHelper
    {
        public static string GetUserTokenKey(string userName)
        {
            return $"token_{userName}";

        }

        public static string GetWechatKey(string wxid)
        {
            return $"wechat_{wxid}";

        }
        public static string GetHttpContextUserKey()
        {
            return "currentUser";
        }
    }
}
