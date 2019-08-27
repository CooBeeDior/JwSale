using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model
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

        /// <summary>
        /// 排序字段
        /// </summary>
        public IList<OrderByBase> OrderBys { get; set; }

    }


    public class OrderByBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否升序
        /// </summary>
        public bool IsAsc { get; set; }

    }
}
