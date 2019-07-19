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
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary> 
        public int PageSize { get; set; } = 10;

    }
}
