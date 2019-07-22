using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 获取用户
    /// </summary>
    public class GetUsers : RequestPageBase
    {

        public string Name { get; set; }

        public int? Status { get; set; }


      

    }


 
}
