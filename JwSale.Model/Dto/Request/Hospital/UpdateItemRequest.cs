using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{
    public class UpdateItemRequest : RequestBase
    {
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 项目类别Id
        /// </summary>
        [Required]
        public string ItemTypeId { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [Required]
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
