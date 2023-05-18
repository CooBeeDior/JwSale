using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model.DbModel
{
    /// <summary>
    /// 医生
    /// </summary>
    [Table("Doctor")]
    public class Doctor : Entity
    {
        /// <summary>
        /// 所属医院
        /// </summary>
        public string BelongHospitalId { get; set; }

        /// <summary>
        /// 科室Id
        /// </summary>
        public string EpartmeneId { get; set; }

        /// <summary>
        /// 关联用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 职位 职级
        /// </summary>
        public string Professional { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
