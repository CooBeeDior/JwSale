using JwSale.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Util
{
    public class DefaultUserInfo
    {
        public static UserInfo UserInfo
        {
            get
            {
                return new UserInfo()
                {
                    Id = new Guid("B025749E-2B58-4702-9EC2-97549169F552"),
                    UserName = "admin",
                    RealName = "超级管理员",
                    Password = "123456"

                };
            }
        }

    }
}
