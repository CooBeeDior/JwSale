using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    public class BatchDeleteRolePermissionRequest : RequestBase
    {
        /// <summary>
        /// id列表
        /// </summary>
        [Required]
        public IList<string> Ids { get; set; }
    }
}
