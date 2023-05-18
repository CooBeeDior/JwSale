using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.UserRole
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermssionResponse
    {
        /// <summary>
        /// 主键标志
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
