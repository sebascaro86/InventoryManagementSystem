using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Domain.Core.Commands;
using InventoryManagementSystem.Domain.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace InventoryManagementSystem.Infrastructure.Bus
{
    /// <summary>
    /// Represents a RabbitMQ event bus implementation.
    /// </summary>
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitMQSettings _rabbitMQSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMQBus"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance for sending commands.</param>
        /// <param name="serviceScopeFactory">The service scope factory for creating service scopes.</param>
        /// <param name="rabbitMQSettings">The RabbitMQ settings.</param>
        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory,IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        /// <inheritdoc/>
        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var eventName = @event.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);
            }
        }

        /// <inheritdoc/>
        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        /// <inheritdoc/>
        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"The handler exception {handlerType.Name} It was previously registered by '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        /// <summary>
        /// Starts consuming messages from the specified event queue in RabbitMQ.
        /// </summary>
        /// <typeparam name="T">The type of event to consume.</typeparam>
        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(eventName, true, consumer);
        }

        /// <summary>
        /// Event handler for receiving a message from RabbitMQ.
        /// </summary>
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.Span);
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }

        /// <summary>
        /// Processes the received event message by invoking the appropriate event handlers.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="message">The event message.</param>
        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];

                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;
                        
                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        var handleMethod = concreteType.GetMethod("Handle");
                        await (Task)handleMethod.Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
    }
}
