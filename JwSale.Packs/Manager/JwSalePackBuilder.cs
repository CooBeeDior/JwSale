using JwSale.Packs.Pack;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JwSale.Packs.Manager
{

    public class JwSalePackBuilder : IJwSalePackBuilder
    {
        /// <summary>
        /// 初始化一个<see cref="OsharpBuilder"/>类型的新实例
        /// </summary>
        public JwSalePackBuilder()
        {
            Packs = new List<Type>();
            DependencyPacks = new List<Type>();
        }


        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        public IEnumerable<Type> Packs { get; private set; }

        /// <summary>
        /// 获取 依赖的模块集合
        /// </summary>
        public IEnumerable<Type> DependencyPacks { get; private set; }

        /// <summary>
        /// 添加指定模块，执行此功能后将仅加载指定的模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        public IJwSalePackBuilder AddPack<TPack>() where TPack : JwSalePack
        {
            List<Type> packs = Packs.ToList();
            List<Type> dependencyPacks = DependencyPacks.ToList();

            packs.AddIfNotExist<Type>(typeof(TPack));
            var packDependecyAttribute = typeof(TPack).GetCustomAttribute<PackDependecyAttribute>();
            dependencyPacks.AddIfNotNullAndNotExsit(packDependecyAttribute?.PackDependecyTypes?.Where(o => typeof(JwSalePack).IsAssignableFrom(o) && o.IsClass && !o.IsAbstract)?.ToList());

            Packs = packs;
            DependencyPacks = dependencyPacks;
            return this;
        }


        public IJwSalePackBuilder AddPackWithPackAttribute<TAttribute>() where TAttribute : Attribute
        {
            List<Type> packs = Packs.ToList();
            List<Type> dependencyPacks = DependencyPacks.ToList();
            foreach (var assembly in AssemblyFinder.AllAssembly)
            {
                var filterTypes = assembly.GetTypes().Where(o => Attribute.IsDefined(o, typeof(TAttribute)) && typeof(JwSalePack).IsAssignableFrom(o) && o.IsClass && !o.IsAbstract).ToList();

                foreach (var packType in filterTypes)
                {
                    packs.AddIfNotNullAndNotExsit<Type>(packType);
                    var packDependecyAttribute = packType.GetCustomAttribute<PackDependecyAttribute>();
                    dependencyPacks.AddIfNotNullAndNotExsit(packDependecyAttribute?.PackDependecyTypes?.Where(o=> typeof(JwSalePack).IsAssignableFrom(o) && o.IsClass && !o.IsAbstract)?.ToList());
                
                }

            }
            Packs = packs;
            DependencyPacks = dependencyPacks;
            return this;
        }









    }
}
