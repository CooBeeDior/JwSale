using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:12
    /// 描  述：OrderInfo实体
    /// </summary>
    [Table("OrderInfo")]
    public class OrderInfo: Entity
    {
         
        /// <summary>
        /// 客户Id
        /// </summary>
        public Guid CustomerInfoId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CsutomerInfoName { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 订单实际总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 订单类型 0：首单 1，复购
        /// </summary>
        public short OrderType { get; set; }
        /// <summary>
        /// 打折价格
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string ConsigneePhone { get; set; }
        /// <summary>
        /// ConsigneeCountry
        /// </summary>
        public string ConsigneeCountry { get; set; }
        /// <summary>
        /// ConsigneeProvince
        /// </summary>
        public string ConsigneeProvince { get; set; }
        /// <summary>
        /// ConsigneeCiry
        /// </summary>
        public string ConsigneeCiry { get; set; }
        /// <summary>
        /// ConsigneeArea
        /// </summary>
        public string ConsigneeArea { get; set; }
        /// <summary>
        /// ConsigneeAddress
        /// </summary>
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// 快递 字典
        /// </summary>
        public Guid ExpressTypeId { get; set; }
        /// <summary>
        /// 快递名称 字典
        /// </summary>
        public string ExpressTypeName { get; set; }
        /// <summary>
        /// 快递价格
        /// </summary>
        public decimal ExpressPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
      
    }
}
