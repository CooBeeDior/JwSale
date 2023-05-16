using JwSale.Api.Const;
using JwSale.Packs.Manager;
using JwSale.Util.Dependencys;
using JwSale.Util.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    public class RefreshWxInfoConsumer : IRabbitmqConsumer
    { 
        public void Register()
        {
            var service = ServiceLocator.Instance;
            var mediator = service.GetService<IMediator>();
            var loggerFactory = service.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<RefreshWxInfoConsumer>();
            var modelDic = service.GetService<Func<string, IModel>>();
            IModel channel = modelDic(QueueConst.RefreshWxInfoName);
            //事件基本消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            //接收到消息事件
            consumer.Received += (ch, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    var refreshWxInfoEvent = message.ToObj<RefreshWxInfoEvent>();
                    mediator.Publish(refreshWxInfoEvent);
                    //确认该消息已被消费
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            };
            channel.BasicConsume(QueueConst.RefreshWxInfoName, false, consumer);
        }
    }
}
