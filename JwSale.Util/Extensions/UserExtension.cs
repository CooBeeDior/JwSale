using JwSale.Util.Extensions;
using System;

namespace JwSale.Util
{
    public static class UserExtension
    { 

        public static string GenerateToken(this UserToken userToken, string key)
        {
            var userTokenJson = userToken.ToJson();

            var token = userTokenJson.ToDes(key);
            return token;

        }



        public static UserToken AnalysisToken(this string token, string key)
        {
            var josn = token.FromDes(key);
            var userToken = josn?.ToObj<UserToken>();
            return userToken;
        } 

    }

    public class UserToken
    {
        public string UserId { get; set; }

        public string UserName { get; set; }


        public string Ip { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }


        public DateTime AddTime { get; set; }


    }
}
