using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:12
    /// 描  述：OrderItemInfo实体
    /// </summary>
    [Table("OrderItemInfo")]
    public class OrderItemInfo: Entity
    {
     
        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid OrderInfoId { get; set; }
        /// <summary>
        /// ProductId
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// ProductName
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 打折价格
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 实际价格=价格-打折价格
        /// </summary>
        public decimal RealPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
    }
}
