using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto
{
    public class BriefInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
