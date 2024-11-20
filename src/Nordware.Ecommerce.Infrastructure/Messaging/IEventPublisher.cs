namespace Nordware.Ecommerce.Infrastructure.Messaging
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event);
    }
}
