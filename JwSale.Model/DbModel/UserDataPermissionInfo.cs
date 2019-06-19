using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：UserDataPermissionInfo实体
    /// </summary>
    [Table("UserDataPermissionInfo")]
    public class UserDataPermissionInfo: Entity
    { 
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 允许访问的用户Id
        /// </summary>
        public Guid ToUserId { get; set; }
        /// <summary>
        /// 类型 0：可以访问 1：不能访问
        /// </summary>
        public short Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    
    }
}
