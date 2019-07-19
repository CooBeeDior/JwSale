using JwSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Util
{
    public class UserHelper
    {
        public static UserInfo UserInfo
        {
            get
            {
                return new UserInfo()
                {
                    Id = Guid.Empty,
                    UserName = "admin",
                    AddUserRealName = "超级管理员",

                };
            }
        }
    }
}
