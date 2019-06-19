using JwSale.Packs.Pack;
using System;
using System.Collections.Generic;

namespace JwSale.Packs.Manager
{

    public interface IJwSalePackBuilder
    {

        IEnumerable<Type> Packs { get; }

        IEnumerable<Type> DependencyPacks { get; }

        /// <summary>
        /// 添加指定模块，执行此功能后将仅加载指定的模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        IJwSalePackBuilder AddPack<TPack>() where TPack : JwSalePack;

        IJwSalePackBuilder AddPackWithPackAttribute<TAttribute>() where TAttribute : Attribute;
    }
}
