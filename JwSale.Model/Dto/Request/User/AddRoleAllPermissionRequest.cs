﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    public class AddRoleAllPermissionRequest : RequestBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public string RoleId { get; set; }
 
    }
}
