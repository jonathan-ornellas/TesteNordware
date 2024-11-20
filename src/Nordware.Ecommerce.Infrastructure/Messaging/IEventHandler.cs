namespace Nordware.Ecommerce.Infrastructure.Messaging
{
    public interface IEventHandler<in T>
    {
        void Handle(T @event);
    }
}
