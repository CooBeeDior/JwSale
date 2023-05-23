using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Enums
{
    public enum FunctionType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2,
        /// <summary>
        /// 修改
        /// </summary>
        Update = 4,
        /// <summary>
        /// 查询
        /// </summary>
        Select = 8,
        /// <summary>
        /// 导入
        /// </summary>
        Import = 16,
        /// <summary>
        /// 导出
        /// </summary>
        Export = 32

    }
}
