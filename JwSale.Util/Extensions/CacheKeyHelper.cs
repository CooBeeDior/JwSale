using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Util
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
        public static string GetWxLoginTokenKey(string openid)
        {
            return $"token:{openid}";
        }

        public static string GetUserTokenKey(string userName, string loginType)
        {
            return $"token:{userName}_{loginType}";
        }


        public const string CURRENTUSER = "CURRENT_USER";



        public const string WECHATUSER = "WECHAT_USER";


        public const string PHONENUMER = "phoneNumer";

        public const string WXOPENID = "wxOpenId";


        public const string HOSPITALID = "hospitalId";

    }
}
