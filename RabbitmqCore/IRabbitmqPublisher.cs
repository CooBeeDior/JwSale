using RabbitMQ.Client;

namespace RabbitmqCore
{
    public interface IRabbitmqPublisher<T> : IRabbitmqPublisher where T : Event
    {

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message"></param>
        void Publish(T @event);

  



    }

    public interface IRabbitmqPublisher : IRabbitmq
    {
        IConnectionFactory ConnectionFactory { get; }
        /// <summary>
        /// 初始化队列信息
        /// </summary>
        void Initialization();
    }
}
