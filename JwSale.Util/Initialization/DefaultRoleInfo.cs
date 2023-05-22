using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util
{
    public class DefaultRoleInfo
    {

        public static DefaultRoleInfo RoleInfo
        {
            get
            {
                return new DefaultRoleInfo()
                {
                    Id = new Guid("B025749E-2B58-4702-9EC2-97549169F533").ToString(),
                    Name = "超级管理员",
                    Remark = "默认初始化用户角色"
                };
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string Remark { get; set; }


    }
}
