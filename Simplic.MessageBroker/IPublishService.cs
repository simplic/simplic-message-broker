using System;
using System.Threading;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Interface for a servie that publishes mass transit messaghes
    /// </summary>
    public interface IPublishService
    {
        /// <summary>
        /// Publishes a message 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        void Publish<T>(T message, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Publishes a message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        void Publish(object message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a message 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <param name="cancellationToken"></param>
        void Publish(object message, Type messageType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a message 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        void Publish<T>(object values, CancellationToken cancellationToken = default) where T : class;
    }
}