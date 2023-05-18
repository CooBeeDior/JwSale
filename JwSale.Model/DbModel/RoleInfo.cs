using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：RoleInfo实体
    /// </summary>
    [Table("RoleInfo")]
    public class RoleInfo: Entity
    { 
        /// <summary>
        /// 功能名称
        /// </summary>
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
