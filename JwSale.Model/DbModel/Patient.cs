using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JwSale.Model.DbModel
{
    /// <summary>
    /// 患者
    /// </summary>
    [Table("Patient")]
    public class Patient : Entity
    {
        /// <summary>
        /// 所属医院Id
        /// </summary>
        public string BelongHospitalId { get; set; }
        /// <summary>
        /// 就诊医生Id
        /// </summary>
        public string BelongDoctorId { get; set; }
        /// <summary>
        /// 关联用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 病情描述
        /// </summary>
        public string IllnessDesc { get; set; }
        /// <summary>
        /// 来源,--1:门诊 2：住院 3：急诊
        /// </summary>
        public int From { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
