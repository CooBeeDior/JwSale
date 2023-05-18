using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：UserRoleDataPermissionInfo实体
    /// </summary>
    [Table("UserRoleDataPermissionInfo")]
    public class UserRoleDataPermissionInfo: Entity
    { 
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
      
    }
}
