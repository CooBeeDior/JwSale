using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwSale.Model
{
    /// <summary>
    /// 创建人：LTL
    /// 日  期：2019.06.11 10:13
    /// 描  述：ProjectInfo实体
    /// </summary>
    [Table("ProjectInfo")]
    public class ProjectInfo: Entity
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
        /// 负责人
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 负责姓名
        /// </summary>
        public string UserRealName { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
   
    }
}
