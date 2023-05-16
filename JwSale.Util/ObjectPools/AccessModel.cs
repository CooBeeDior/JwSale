using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.ObjectPools
{
    public enum AccessModel
    {
        /// <summary>
        /// 先进先出
        /// </summary>
        FIFO,
        /// <summary>
        /// 后进先出
        /// </summary>
        LIFO
    }
}
