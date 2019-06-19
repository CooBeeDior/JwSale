using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:12
    /// 描  述：GeneralizeInfo实体
    /// </summary>
    [Table("GeneralizeInfo")]
    public class GeneralizeInfo: Entity
    { 
        /// <summary>
        /// 投放目的地Id（字典）
        /// </summary>
        public Guid DestinationId { get; set; }
        /// <summary>
        /// 投放目的地名称（字典）
        /// </summary>
        public string DestinationName { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }
   
    }
}
