using JwSale.Packs.Attributes;
using JwSale.Packs.Enums;
using JwSale.Packs.Manager;
using JwSale.Packs.Options;
using JwSale.Packs.Pack;
using JwSale.Repository.Context;
using JwSale.Repository.Repositorys;
using JwSale.Repository.UnitOfWork;
using JwSale.Util.Assemblies;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitmqCore;
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
            {
                services.AddSingleton(jwSaleOptions.Rabbitmq);
                //创建连接工厂
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

                    var interfaces = type.GetInterfaces().Where(o => typeof(IRabbitmqPublisher).IsAssignableFrom(o)|| typeof(IRabbitmqConsumer).IsAssignableFrom(o)).ToList();
                    foreach (var rabbitmqInterface in interfaces)
                    {
                        services.AddSingleton(rabbitmqInterface, type);
         
                    }
                    //services.AddSingleton(typeof(IRabbitmq), type);
                }
            }
            return services;
        }

        protected override void UsePack(IApplicationBuilder app)
        {
            List<Type> types = new List<Type>();
            var rabbitmqPublishers = app.ApplicationServices.GetServices<IRabbitmqPublisher>();
            var rbbitmqConsumers = app.ApplicationServices.GetServices<IRabbitmqConsumer>();
            foreach (var rabbitmqPublisher in rabbitmqPublishers)
            {
                var eventAttribute = rabbitmqPublisher.GetType().GetCustomAttribute<EventAttribute>();
                if (eventAttribute.IsInitialization)
                {
                    rabbitmqPublisher.Initialization();
                }
            }
            foreach (var rabbitmqConsumer in rbbitmqConsumers)
            {
                var eventAttribute = rabbitmqConsumer.GetType().GetCustomAttribute<EventAttribute>();
                if (eventAttribute.IsInitialization)
                {
                    rabbitmqConsumer.Subscripe();
                }
            }

  

        }

    }





}
