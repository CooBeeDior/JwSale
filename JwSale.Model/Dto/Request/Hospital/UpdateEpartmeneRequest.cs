using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.Hospital
{
   public class UpdateEpartmeneRequest : RequestBase
    {
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 状态 默认赋值：1
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

   
    }
}
