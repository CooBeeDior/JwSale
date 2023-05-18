using JwSale.Util.Extensions;
using System;

namespace JwSale.Util
{
    public class UserHelper
    { 

        public static string GenerateToken(UserToken userToken, string key)
        {
            var userTokenJson = userToken.ToJson();

            var token = userTokenJson.ToDes(key);
            return token;

        }



        public static UserToken AnalysisToken(string token, string key)
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


        public int Expireds { get; set; }


        public DateTime AddTime { get; set; }


    }
}
