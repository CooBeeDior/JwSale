using JwSale.Api.Const.JwSale.Api.Const;
using JwSale.Packs.Attributes;
using JwSale.Packs.Manager;
using JwSale.Util.Attributes;
using JwSale.Util.Dependencys;
using JwSale.Util.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitmqCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    /// <summary>
    /// 消费者
    /// </summary>
    [Event("AddUserSucceed", true)]
    public class AddUserSucceedConsumer : IRabbitmqConsumer
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AddUserSucceedConsumer> _logger;
        private readonly IConnectionFactory _connectionFactory;
        public AddUserSucceedConsumer(IMediator mediator, ILogger<AddUserSucceedConsumer> logger, IConnectionFactory connectionFactory)
        {
            _mediator = mediator;
            _logger = logger;
            _connectionFactory = connectionFactory;
        }
        /// <summary>
        /// 订阅消息
        /// </summary>
        public void Subscripe()
        {

            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            //事件基本消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            //接收到消息事件
            consumer.Received += async (ch, ea) =>
             {
                 try
                 {
                     var message = Encoding.UTF8.GetString(ea.Body.Span);
                     var addUserEvent = message.ToObj<AddUserSucceedEvent>();
                     await _mediator.Publish(addUserEvent);
                    //确认该消息已被消费
                    channel.BasicAck(ea.DeliveryTag, false);
                 }
                 catch (Exception ex)
                 {
                     _logger.LogError(ex, ex.Message);
                 }
             };
            channel.BasicConsume(QueueConst.TEST, false, consumer);
        }



    }
}
