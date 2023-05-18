using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JwSale.Model.DbModel
{
    /// <summary>
    /// 项目类别
    /// </summary>
    [Table("ItemType")]
    public class ItemType : Entity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 医院Id
        /// </summary>
        public string HospitalId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
