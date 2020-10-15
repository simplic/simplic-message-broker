using MassTransit;
using Newtonsoft.Json;
using Simplic.InMemoryDB;
using Simplic.Session;
using System;
using System.Threading;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Service to publish mass transit messages
    /// </summary>
    public class MessageBus : IMessageBus
    {
        private readonly IBusControl bus;
        private readonly IKeyValueStore keyValueStore;
        private readonly ISessionService sessionService;

        /// <summary>
        /// Initializes a new instance of PublishService
        /// </summary>
        /// <param name="busControl"></param>
        public MessageBus(IBusControl busControl, IKeyValueStore keyValueStore, ISessionService sessionService)
        {
            bus = busControl;
            this.keyValueStore = keyValueStore;
            this.sessionService = sessionService;
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            IncrementMessageChannel();
            bus.Publish<T>(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Publish(object message, CancellationToken cancellationToken = default)
        {
            IncrementMessageChannel();
            bus.Publish(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="messageType">the message type</param>
        /// <param name="cancellationToken"></param>
        public void Publish(object message, Type messageType, CancellationToken cancellationToken = default)
        {
            IncrementMessageChannel();
            bus.Publish(message, messageType, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T">the message type</typeparam>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        public void Publish<T>(object values, CancellationToken cancellationToken = default) where T : class
        {
                IncrementMessageChannel();
                bus.Publish<T>(values, cancellationToken);   
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Send<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            IncrementMessageChannel();
            bus.Send<T>(message, cancellationToken);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="cancellationToken"></param>
        public void Send(object message, CancellationToken cancellationToken = default)
        {
            IncrementMessageChannel();
            bus.Send(message, cancellationToken);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="messageType">The message type</param>
        /// <param name="cancellationToken"></param>
        public void Send(object message, Type messageType, CancellationToken cancellationToken = default)
        {
            IncrementMessageChannel();
            bus.Send(message, messageType);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="values">The property values to initialize on the interface</param>
        /// <param name="cancellationToken"></param>
        public void Send<T>(object values, CancellationToken cancellationToken = default) where T : class
        {
            IncrementMessageChannel();
            bus.Send<T>(values, cancellationToken);
        }

        /// <summary>
        /// Publishes a message to a message channel
        /// </summary>
        /// <param name="commandBase"></param>
        private void IncrementMessageChannel()
        {
            try
            {
                var userId = sessionService.CurrentSession.UserId;
                keyValueStore.StringIncrement(MessageBrokerChannel.GlobalMessageChannel);
                keyValueStore.StringIncrement(MessageBrokerChannel.GetUserMessageChannel(userId));
            }
            catch (Exception ex)
            {
                Log.LogManagerInstance.Instance.Error("Error while incrementing in message channel db", ex);
            };
        }
    }
}