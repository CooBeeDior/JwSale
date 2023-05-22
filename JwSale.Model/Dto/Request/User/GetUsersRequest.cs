using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 获取用户
    /// </summary>
    public class GetUsersRequest : RequestPageBase
    {

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string IdCard { get; set; }

        public int? Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDayStart { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDayEnd { get; set; }

        public int? Status { get; set; }


      

    }


 
}
