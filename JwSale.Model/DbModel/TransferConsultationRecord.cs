using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model.DbModel
{
    /// <summary>
    /// 转诊记录
    /// </summary>
    [Table("TransferConsultationRecord")]
    public class TransferConsultationRecord : Entity
    {
        /// <summary>
        /// 转诊患者Id
        /// </summary>
        public Guid PatientId { get; set; }
        /// <summary>
        /// 转诊Id
        /// </summary>
        public Guid TransferConsultationId { get; set; }
        /// <summary>
        ///来源 1:门诊 2：住院 3：急诊
        /// </summary>
        public int From { get; set; }
        /// <summary>
        /// 来源医院Id
        /// </summary>
        public Guid FromHospitalId { get; set; }
        /// <summary>
        /// 来源科室Id
        /// </summary>
        public Guid FromEpartmeneId { get; set; }
        /// <summary>
        /// 来源就诊医生Id
        /// </summary>
        public Guid FromDoctorId { get; set; }
        /// <summary>
        /// 来源就诊病情描述
        /// </summary>
        public string FromIllnessDesc { get; set; }
        /// <summary>
        /// 转诊目标医院Id
        /// </summary>
        public Guid TransferHospitalId { get; set; }
        /// <summary>
        /// 转诊目标科室Id
        /// </summary>
        public Guid TransferEpartmeneId { get; set; }
        /// <summary>
        /// 转诊目标医生
        /// </summary>
        public Guid TransferDoctorId { get; set; }

        /// <summary>
        /// 1:接诊 2：转诊 4：拒接诊
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
