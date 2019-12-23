using JwSale.Packs.Attributes;
using JwSale.Packs.Enums;
using JwSale.Packs.Manager;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Repository.Context;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Assemblies;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
namespace JwSale.Packs.Packs
{
    /// <summary>
    /// Rabbitmq模块
    /// </summary>
    [Pack("Rabbitmq模块")]
    [PackDependecy(typeof(JwSaleOptionsPack))]
    public class RabbitmqPack : JwSalePack
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        protected override IServiceCollection AddServices(IServiceCollection services)
        {
            var configuration = services.GetSingletonInstance<IConfiguration>();
            var jwSaleOptions = configuration.Get<JwSaleOptions>();
            if (jwSaleOptions.Rabbitmq != null)
            {  //创建连接工厂
                IConnectionFactory factory = new ConnectionFactory
                {
                    UserName = jwSaleOptions.Rabbitmq.UserName,//用户名
                    Password = jwSaleOptions.Rabbitmq.Password,//密码
                    HostName = jwSaleOptions.Rabbitmq.HostUrl//rabbitmq ip
                };
                services.AddSingleton<IConnectionFactory>(factory);

                //创建连接
                var connection = factory.CreateConnection();
                services.AddSingleton<IConnection>(connection);

                List<Type> types = new List<Type>();
                AssemblyFinder.AllAssembly.ToList().ForEach(o => types.AddRange(o.GetTypes().Where(t => typeof(IRabbitmq).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)));



                Dictionary<string, IModel> modelDic = new Dictionary<string, IModel>();
                foreach (var type in types)
                {
                    if (typeof(IRabbitmqPublisher).IsAssignableFrom(type))
                    {
                        IRabbitmqPublisher instance = Activator.CreateInstance(type) as IRabbitmqPublisher;
                        if (instance != null)
                        {
                            IModel model = connection.CreateModel();
                            string name = instance.Resove(model);
                            modelDic.Add(name, model);
                            services.AddSingleton<IRabbitmqPublisher>(instance);

                        }
                    }
                    else  if (typeof(IRabbitmqConsumer).IsAssignableFrom(type))
                    {
                        IRabbitmqConsumer instance = Activator.CreateInstance(type) as IRabbitmqConsumer;
                        if (instance != null)
                        {                             
                            services.AddSingleton<IRabbitmqConsumer>(instance);

                        }
                    }
                }

                services.AddSingleton<Func<string, IModel>>(p =>
                {
                    return n =>
                    {
                        return modelDic[n];
                    };
                });
            }



            return services;
        }

        protected override void UsePack(IApplicationBuilder app)
        {
            var rabbitmqConsumers = app.ApplicationServices.GetServices<IRabbitmqConsumer>();
            foreach (var item in rabbitmqConsumers)
            {
                item.Register();
            }

        }

    }





}
