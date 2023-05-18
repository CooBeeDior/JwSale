using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：SysDictionary实体
    /// </summary>
    [Table("SysDictionary")]
    public class SysDictionary: Entity
    { 
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 父级别Id
        /// </summary>
        public string ParentId { get; set; }
   
    }
}
