using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JwSale.Model.DbModel
{
    /// <summary>
    /// 科室
    /// </summary>
    [Table("Epartmene")]
    public class Epartmene : Entity
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
        /// 医院Id
        /// </summary>
        public string HospitalId { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
