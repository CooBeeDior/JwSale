using RabbitMQ.Client;

namespace JwSale.Packs.Manager
{
    public interface IRabbitmqPublisher: IRabbitmq
    {
        string Resove(IModel channel);

    }
}
