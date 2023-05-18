using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 设置所有权限
    /// </summary>
    public class SetAllPermissonRequest: RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string UserId { get; set; }
    }
}
