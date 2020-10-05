using System;
using System.Threading;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Interface for a servie that publishes mass transit messaghes
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T">The Message Type</typeparam>
        /// <param name="message">The Message</param>
        /// <param name="cancellationToken"></param>
        void Publish<T>(T message, CancellationToken cancellationToken = default) where T : class , ICommandBase;

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message object</param>
        /// <param name="cancellationToken"></param>
        void Publish(ICommandBase message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message object</param>
        /// <param name="messageType">The message type</param>
        /// <param name="cancellationToken"></param>
        void Publish(ICommandBase message, Type messageType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T">The messgae type</typeparam>
        /// <param name="values">The property values to initialize on the interface</param>
        /// <param name="cancellationToken"></param>
        void Publish<T>(object values, CancellationToken cancellationToken = default) where T : class, ICommandBase;

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The Message Type</typeparam>
        /// <param name="message">The Message</param>
        /// <param name="cancellationToken"></param>
        void Send<T>(T message, CancellationToken cancellationToken = default) where T : class, ICommandBase;

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">The message object</param>
        /// <param name="cancellationToken"></param>
        void Send(ICommandBase message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">The message object</param>
        /// <param name="messageType">the message type</param>
        /// <param name="cancellationToken"></param>
        void Send(ICommandBase message, Type messageType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="values">The property values to initialize on the interface</param>
        /// <param name="cancellationToken"></param>
        void Send<T>(object values, CancellationToken cancellationToken = default) where T : class, ICommandBase;
    }
}