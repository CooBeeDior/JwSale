using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 设置用户状态
    /// </summary>
    public class SetUserStatusRequest : RequestBase
    { 
        /// <summary>
       /// 用户Id
       /// </summary>
        [Required]
        public string UserId { get; set; }
 

        /// <summary>
        /// 状态：0：启用 1：停用
        /// </summary> 
        [Required]
        public short Status { get; set; }


    }
}
