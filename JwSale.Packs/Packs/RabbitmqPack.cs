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
using System.Reflection;

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
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

            if (jwSaleOptions.Rabbitmq != null && !jwSaleOptions.Rabbitmq.HostUrl.IsNullOrWhiteSpace())
            {  //创建连接工厂
                IConnectionFactory factory = new ConnectionFactory
                {
                    UserName = jwSaleOptions.Rabbitmq.UserName,//用户名
                    Password = jwSaleOptions.Rabbitmq.Password,//密码
                    HostName = jwSaleOptions.Rabbitmq.HostUrl//rabbitmq ip
                };
                services.AddSingleton<IConnectionFactory>(factory);

                //创建连接
                //var connection = factory.CreateConnection();
                //services.AddSingleton<IConnection>(connection);

                List<Type> types = new List<Type>();
                AssemblyFinder.AllAssembly.ToList().ForEach(o => types.AddRange(o.GetTypes().Where(t => typeof(IRabbitmq).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)));




                foreach (var type in types)
                {
                    if (typeof(IRabbitmqPublisher).IsAssignableFrom(type))
                    {
                        services.AddSingleton(typeof(IRabbitmqPublisher), type);

                    }
                    else if (typeof(IRabbitmqConsumer).IsAssignableFrom(type))
                    {
                        services.AddSingleton(typeof(IRabbitmqConsumer), type);
                    }
                    services.AddSingleton(type);
                }
            }



            return services;
        }

        protected override void UsePack(IApplicationBuilder app)
        {
            var rabbitmqIRabbitmqPublisher = app.ApplicationServices.GetServices<IRabbitmqPublisher>();
            foreach (var item in rabbitmqIRabbitmqPublisher)
            {
                var eventAttribute = item.GetType().GetCustomAttribute<EventAttribute>();
                if (eventAttribute.IsInitialization)
                {
                    item.Initialization();
                }

            }
            var rabbitmqConsumers = app.ApplicationServices.GetServices<IRabbitmqConsumer>();
            foreach (var item in rabbitmqConsumers)
            {
                var eventAttribute = item.GetType().GetCustomAttribute<EventAttribute>();
                if (eventAttribute.IsInitialization)
                {
                    item.Register();
                } 
            }

        }

    }





}
