using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JwSale.Model.DbModel
{
    [Table("Item")]
    public class Item : Entity
    {
        /// <summary>
        /// 医院Id
        /// </summary>
        public string HospitalId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目类别Id
        /// </summary>
        public string ItemTypeId { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 1:上架 2：下架
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
