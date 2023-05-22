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
                    Id = new Guid("B025749E-2B58-4702-9EC2-97549169F552").ToString(),
                    UserName = "admin",
                    RealName = "默认用户",
                    Password = "123456",
                    Phone = "18138258825",
                    Remark = "默认初始化用户"
                };
            }
        }

        public string Id { get; set; }
        public string UserName { get; set; }

        public string RealName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }
        public string Remark { get; set; }
    }



}
