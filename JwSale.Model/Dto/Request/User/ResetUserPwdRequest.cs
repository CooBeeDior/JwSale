using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 重置密码
    /// </summary>
    public class ResetUserPwdRequest : RequestBase
    {         
        /// <summary>
        /// 密码
        /// </summary> 
        [Required]
        public string NewPassword { get; set; }
    }
}
