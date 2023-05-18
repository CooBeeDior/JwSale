using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：UserPermissionInfo实体
    /// </summary>
    [Table("UserPermissionInfo")]
    public class UserPermissionInfo: Entity
    {
        
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 功能Id
        /// </summary>
        public string FunctionId { get; set; }
        /// <summary>
        /// 类型：0增加 1：减少
        /// </summary>
        public short Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
       
    }
}
