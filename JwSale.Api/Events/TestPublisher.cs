using JwSale.Api.Const;
using JwSale.Api.Const.JwSale.Api.Const;
using JwSale.Packs.Attributes;
using JwSale.Packs.Manager;
using JwSale.Util.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    /// <summary>
    /// 发布者
    /// </summary> 
    [Event("Test", true)]
    public class TestPublisher : IRabbitmqPublisher
    {
        public IConnectionFactory ConnectionFactory { get; }
        public TestPublisher(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        /// <summary>
        /// 初始化队列信息
        /// </summary>
        public void Initialization()
        {
            //声明一个队列
            using (var connection = ConnectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueConst.TESTQUEUENAME,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                }
            }

        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message"></param>
        public void Publish(string message)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var testEvent = new TestEvent(message);
                    var body = Encoding.UTF8.GetBytes(testEvent.ToJson());

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueConst.TESTQUEUENAME,
                                         basicProperties: null,
                                         body: body);

                }
            }

        }
    }
}