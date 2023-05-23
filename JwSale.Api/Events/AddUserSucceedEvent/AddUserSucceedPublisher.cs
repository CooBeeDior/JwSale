using JwSale.Api.Const;
using JwSale.Api.Const.JwSale.Api.Const;
using JwSale.Packs.Attributes;
using JwSale.Packs.Manager;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using RabbitMQ.Client;
using RabbitmqCore;
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
    [Event("AddUserSucceed", true)]
    public class AddUserSucceedPublisher :  IRabbitmqPublisher<AddUserSucceedEvent>
    {
        public IConnectionFactory ConnectionFactory { get; }
        public AddUserSucceedPublisher(IConnectionFactory connectionFactory)
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
                    channel.QueueDeclare(queue: QueueConst.ADDUSERSUCCEED,
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
        /// <param name="event"></param>
        public void Publish(AddUserSucceedEvent @event)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    var body = Encoding.UTF8.GetBytes(@event.ToJson());
                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueConst.ADDUSERSUCCEED,
                                         basicProperties: null,
                                         body: body);

                }
            }

        }


    }
}