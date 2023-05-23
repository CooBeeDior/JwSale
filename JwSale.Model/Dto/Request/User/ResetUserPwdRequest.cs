using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    /// <summary>
    /// 重置密码
    /// </summary>
    public class ResetUserPwdRequest : RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string UserId { get; set; }
     
        /// <summary>
        /// 新密码
        /// </summary> 
        [Required]
        public string NewPassword { get; set; }
    }
}
