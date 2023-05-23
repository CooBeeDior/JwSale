using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    public class AddUserRoleRequest : RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public string RoleId { get; set; }
    }
}
