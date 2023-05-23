using JwSale.Api.Const.JwSale.Api.Const;
using JwSale.Util.Attributes;
using JwSale.Util.Extensions;
using RabbitMQ.Client;
using RabbitmqCore;
using System.Text;
namespace JwSale.Api.Events
{
    /// <summary>
    /// 发布者
    /// </summary> 
    [Event("Test", true)]
    public class BindWechatUserSucceedPublisher : IRabbitmqPublisher<BindWechatUserSucceedEvent>
    {
        public IConnectionFactory ConnectionFactory { get; }
        public BindWechatUserSucceedPublisher(IConnectionFactory connectionFactory)
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
                    channel.QueueDeclare(queue: QueueConst.BINDWECHATUSERSUCCEED,
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
        public void Publish(BindWechatUserSucceedEvent @event)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var body = Encoding.UTF8.GetBytes(@event.ToJson());

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueConst.BINDWECHATUSERSUCCEED,
                                         basicProperties: null,
                                         body: body);

                }
            }

        }
    }
}
