using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util
{

    public class DefaultUserInfo
    {

        public static DefaultUserInfo UserInfo
        {
            get
            {
                return new DefaultUserInfo()
                {
                    Id = new Guid("B025749E-2B58-4702-9EC2-97549169F552"),
                    UserName = "admin",
                    RealName = "超级管理员",
                    Password = "123456"

                };
            }
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Password { get; set; }

    }
}
