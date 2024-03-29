﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    public class AddRoleRequest
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
