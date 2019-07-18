using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto
{

    public class RequestBase
    {
    }


    public class RequestPageBase
    {
        /// <summary>
        /// 页索引
        /// </summary>
        [Required]
        [MinLength(0)]
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        [Required]
        [MinLength(0)]
        public int PageSize { get; set; }

    }
}
