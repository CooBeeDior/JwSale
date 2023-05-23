using JwSale.Util.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    /// <summary>
    /// 获取用户
    /// </summary>
    public class QueryUsersRequest : RequestPageBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexType? Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDayStart { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDayEnd { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus? Status { get; set; }




    }



}
