using Microsoft.Extensions.DependencyInjection;
using Nordware.Ecommerce.Infrastructure.Messaging;
using System.Collections.Concurrent;

namespace Nordware.Ecommerce.Infrastructure.EventBus
{
    public class InMemoryEventBus : IEventPublisher
    {
        private readonly ConcurrentQueue<object> _events = new();
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<T>(T @event)
        {
            _events.Enqueue(@event);
        }

        public void ProcessEvents()
        {
            while (_events.TryDequeue(out var @event))
            {
                var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = _serviceProvider.GetServices(handlerType);

                foreach (var handler in handlers)
                {
                    var method = handlerType.GetMethod("Handle");
                    method?.Invoke(handler, new[] { @event });
                }
            }
        }
    }
}
