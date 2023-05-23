using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwSale.Util.Extensions;

namespace JwSale.Util
{
    public class CacheKeyHelper
    {
        public static string GetUserTempTokenKey(string tempToken)
        {
            return $"temptoken:{tempToken}";
        }

        //public static string GetUserTokenKey(string username)
        //{
        //    return $"user:token:{username}";
        //}
        public static string GetWxLoginTokenKey(string openid)
        {
            return $"wxlogin:token:{openid}";
        }
        public static string GetWechatUserKey(string openid)
        {
            return $"wechatuser:{openid}";
        }
        public static string GetLoginUserKey(string userName, string loginDevice)
        {
            string loginDeviceStr = loginDevice.IsNullOrWhiteSpace() ? null : $"_{loginDevice}";
            return $"user:token:{userName}{loginDeviceStr}";
        }


        public const string CURRENTUSER = "CURRENT_USER";



        public const string WECHATUSER = "WECHAT_USER";


        public const string PHONENUMER = "phoneNumer";

        public const string WECHATOPENID = "openid";

        public const string LOGINDEVICE = "logindevice";


 

    }
}
