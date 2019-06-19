using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:12
    /// 描  述：FollowInfo实体
    /// </summary>
    [Table("FollowInfo")]
    public class FollowInfo: Entity
    {
       
        /// <summary>
        /// 跟进产品Id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 跟进产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public Guid CustomerInfoId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CsutomerInfoName { get; set; }
        /// <summary>
        /// 跟进人Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 跟进人姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 跟进类型Id
        /// </summary>
        public Guid TypeId { get; set; }
        /// <summary>
        /// 跟进类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
    }
}
