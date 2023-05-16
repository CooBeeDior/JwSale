using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.ObjectPools
{
    /// <summary>
    /// 对象存取方式
    /// </summary>
    public interface IAccessMode<T>
    {
        /// <summary>
        /// 租用对象
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        T Rent();

        /// <summary>
        /// 返回实例
        /// </summary>
        /// <param name="item"></param>
        void Return(T item);
    }
}
