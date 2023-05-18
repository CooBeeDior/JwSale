using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JwSale.Model.DbModel
{
    [Table("OrderItem")]
    public class OrderItem : Entity
    {
        /// <summary>
        /// 医院Id
        /// </summary>
        public string HospitalId { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
