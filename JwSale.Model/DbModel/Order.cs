using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model.DbModel
{
    [Table("Order")]
    public class Order : Entity
    {
        /// <summary>
        /// 医院Id
        /// </summary>
        public string HospitalId { get; set; }
        /// <summary>
        /// 开单医生Id
        /// </summary>
        public string DoctorId { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 状态 1:已开单 2：已付款 4：已取消
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
