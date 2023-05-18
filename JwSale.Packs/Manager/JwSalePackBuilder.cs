using JwSale.Packs.Attributes;
using JwSale.Packs.Enums;
using JwSale.Packs.Pack;
using JwSale.Util.Assemblies;
using JwSale.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace JwSale.Packs.Manager
{



    public class JwSalePackBuilder : IJwSalePackBuilder
    {
        private object objLock = new object();
        /// <summary>
        /// 初始化一个<see cref="OsharpBuilder"/>类型的新实例
        /// </summary>
        public JwSalePackBuilder()
        {
            Packs = new List<PackType>();
            DependencyPacks = new List<PackType>();
        }


        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        public IEnumerable<PackType> Packs { get; private set; }

        /// <summary>
        /// 获取 依赖的模块集合
        /// </summary>
        public IEnumerable<PackType> DependencyPacks { get; private set; }

        /// <summary>
        /// 添加指定模块，执行此功能后将仅加载指定的模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        public IJwSalePackBuilder AddPack<TPack>() where TPack : JwSalePack
        {
            lock (objLock)
            {
                List<PackType> packs = Packs.ToList();
                List<PackType> dependencyPacks = DependencyPacks.ToList();
                var packAttribute = typeof(TPack).GetCustomAttribute<PackAttribute>(true);
                Level level = packAttribute?.Level ?? Level.Medium;
                packs.AddIfNotExist(new PackType(typeof(TPack), level));
                var packDependecyAttributes = typeof(TPack).GetCustomAttributes<PackDependecyAttribute>();
                foreach (var packDependecyAttribute in packDependecyAttributes)
                {
                    var depencyList = packDependecyAttribute?.PackDependecyTypes?.Where(o => typeof(JwSalePack).IsAssignableFrom(o) && o.IsClass && !o.IsAbstract)?.ToList()?.Select(o =>
                    {
                        var depencyPackAttribute = typeof(TPack).GetCustomAttribute<PackAttribute>(true);
                        return new PackType(o, depencyPackAttribute.Level);
                    })?.ToList();
                    dependencyPacks.AddIfNotNullAndNotExsit(depencyList);
                }
                Packs = packs;
                DependencyPacks = dependencyPacks;
            }
            return this;

        }


        public IJwSalePackBuilder AddPackWithPackAttribute<TAttribute>() where TAttribute : PackAttribute
        {
            lock (objLock)
            {
                List<PackType> packs = Packs.ToList();
                List<PackType> dependencyPacks = DependencyPacks.ToList();
                foreach (var assembly in AssemblyFinder.AllAssembly)
                {
                    var filterTypes = assembly.GetTypes().Where(o => Attribute.IsDefined(o, typeof(TAttribute)) && typeof(JwSalePack).IsAssignableFrom(o) && o.IsClass && !o.IsAbstract).ToList();

                    foreach (var packType in filterTypes)
                    {
                        var packAttribute = packType.GetCustomAttribute<TAttribute>();
                        if (packAttribute.IsInitialization)
                        {
                            packs.AddIfNotNullAndNotExsit<PackType>(new PackType(packType, packAttribute.Level));
                            var packDependecyAttributes = packType.GetCustomAttributes<PackDependecyAttribute>();
                            foreach (var packDependecyAttribute in packDependecyAttributes)
                            {
                                var depencyList = packDependecyAttribute?.PackDependecyTypes?.Where(o => typeof(JwSalePack).IsAssignableFrom(o) && o.IsClass && !o.IsAbstract)?.ToList()?.Select(o =>
                                {
                                    var depencyPackAttribute = packType.GetCustomAttribute<TAttribute>();
                                    return new PackType(o, depencyPackAttribute.Level);
                                })?.ToList();
                                dependencyPacks.AddIfNotNullAndNotExsit(depencyList);
                            }

                        }
                    }

                }
                Packs = packs;
                DependencyPacks = dependencyPacks;
                return this;
            }
        }









    }
}
