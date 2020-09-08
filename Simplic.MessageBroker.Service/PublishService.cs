using MassTransit;
using System;
using System.Threading;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Service to publish mass transit messages
    /// </summary>
    public class PublishService : IPublishService
    {
        private readonly IBusControl bus;

        /// <summary>
        /// Initializes a new instance of PublishService
        /// </summary>
        /// <param name="busControl"></param>
        public PublishService(IBusControl busControl)
        {
            bus = busControl;
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        public void Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            bus.Publish<T>(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        public void Publish(object message, CancellationToken cancellationToken = default)
        {
            bus.Publish(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="cancellationToken"></param>
        public void Publish(object message, Type messageType, CancellationToken cancellationToken = default)
        {
            bus.Publish(message, messageType, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        public void Publish<T>(object values, CancellationToken cancellationToken = default) where T : class
        {
            bus.Publish<T>(values, cancellationToken);
        }
    }
}