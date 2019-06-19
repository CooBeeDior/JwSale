using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：ProductInfo实体
    /// </summary>
    [Table("ProductInfo")]
    public class ProductInfo: Entity
    {
 
        /// <summary>
        /// 简称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ProjectInfoId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectInfoName { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 类型 字典  软件 硬件 其他
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// 类型名称 字典
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 打折
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
     
    }
}
