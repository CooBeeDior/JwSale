using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:12
    /// 描  述：FunctionInfo实体
    /// </summary>
    [Table("FunctionInfo")]
    public class FunctionInfo : Entity
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 功能编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
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
