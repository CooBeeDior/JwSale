using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JwSale.Model.DbModel
{
    /// <summary>
    /// 转诊
    /// </summary>
    [Table("TransferConsultation")]
    public class TransferConsultation : Entity
    {
        /// <summary>
        /// 转诊患者Id
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        ///来源 1:门诊 2：住院 3：急诊
        /// </summary>
        public int From { get; set; }
        /// <summary>
        /// 来源医院Id
        /// </summary>
        public string FromHospitalId { get; set; }
        /// <summary>
        /// 来源科室Id
        /// </summary>
        public string FromEpartmeneId { get; set; }
        /// <summary>
        /// 来源就诊医生Id
        /// </summary>
        public string FromDoctorId { get; set; }
        /// <summary>
        /// 来源就诊病情描述
        /// </summary>
        public string FromIllnessDesc { get; set; }
        /// <summary>
        /// 转诊目标医院Id
        /// </summary>
        public string TransferHospitalId { get; set; }
        /// <summary>
        /// 转诊目标科室Id
        /// </summary>
        public string TransferEpartmeneId { get; set; }
        /// <summary>
        /// 转诊目标医生
        /// </summary>
        public string TransferDoctorId { get; set; }
        /// <summary>
        /// 转诊状态1:待转诊 2：已确认接诊 4：已接诊 8：已住院 16：已完成 32：拒绝接诊
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
