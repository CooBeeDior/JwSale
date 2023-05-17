using RabbitMQ.Client;

namespace RabbitmqCore
{
    public interface IRabbitmqPublisher : IRabbitmq
    {
        IConnectionFactory ConnectionFactory { get; }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message"></param>
        void Publish(string message);

        /// <summary>
        /// 初始化队列信息
        /// </summary>
        void Initialization();



    }
}
