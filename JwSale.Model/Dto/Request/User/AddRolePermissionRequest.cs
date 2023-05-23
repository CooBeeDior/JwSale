using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    public class AddRolePermissionRequest : RequestBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public string RoleId { get; set; }
        /// <summary>
        /// 功能Id
        /// </summary>
        [Required]
        public string FunctionId { get; set; }

    }

    public class BatchAddRolePermissionRequest : RequestBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public string RoleId { get; set; }

        [Required]
        public IList<string> FunctionIds { get; set; }
    }


}
