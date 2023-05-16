using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Util
{
    public class CacheKeyHelper
    {
        public static string GetUserTempTokenKey(string tempToken)
        {
            return $"temptoken:{tempToken}";
        }

        public static string GetUserTokenKey(string username)
        {
            return $"token:{username}";
        }
        public static string GetUserTokenKey(string userName, string loginType)
        {
            return $"token:{userName}_{loginType}";
        }


        public static string GetHttpContextUserKey()
        {
            return "currentUser";
        }
    }
}
