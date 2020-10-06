using MassTransit;
using Newtonsoft.Json;
using Simplic.Redis;
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
        private readonly IRedisService redisService;
        private readonly ISessionService sessionService;

        /// <summary>
        /// Initializes a new instance of PublishService
        /// </summary>
        /// <param name="busControl"></param>
        public MessageBus(IBusControl busControl, IRedisService redisService, ISessionService sessionService)
        {
            bus = busControl;
            this.redisService = redisService;
            this.sessionService = sessionService;
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Publish<T>(T message, CancellationToken cancellationToken = default) where T : class, ICommandBase
        {
            message = PublishInRedisChannel<T>(message);
            bus.Publish<T>(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Publish(ICommandBase message, CancellationToken cancellationToken = default)
        {
            message = PublishInRedisChannel(message);
            bus.Publish(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="messageType">the message type</param>
        /// <param name="cancellationToken"></param>
        public void Publish(ICommandBase message, Type messageType, CancellationToken cancellationToken = default)
        {
            message = PublishInRedisChannel(message);
            bus.Publish(message, messageType, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <typeparam name="T">the message type</typeparam>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        public void Publish<T>(object values, CancellationToken cancellationToken = default) where T : class, ICommandBase
        {
            var message = values as T;
            message = PublishInRedisChannel<T>(message);
            bus.Publish<T>(message, cancellationToken);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Send<T>(T message, CancellationToken cancellationToken = default) where T : class, ICommandBase
        {
            message = PublishInRedisChannel(message);
            bus.Send<T>(message, cancellationToken);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="cancellationToken"></param>
        public void Send(ICommandBase message, CancellationToken cancellationToken = default)
        {
            message = PublishInRedisChannel(message);
            bus.Send(message, cancellationToken);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="messageType">The message type</param>
        /// <param name="cancellationToken"></param>
        public void Send(ICommandBase message, Type messageType, CancellationToken cancellationToken = default)
        {
            message = PublishInRedisChannel(message);
            bus.Send(message, messageType);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="values">The property values to initialize on the interface</param>
        /// <param name="cancellationToken"></param>
        public void Send<T>(object values, CancellationToken cancellationToken = default) where T : class, ICommandBase
        {
            var message = values as T;
            message = PublishInRedisChannel(message);
            bus.Send<T>(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message to a redis channel
        /// </summary>
        /// <param name="commandBase"></param>
        private T PublishInRedisChannel<T>(T commandBase) where T : ICommandBase
        {
            commandBase.UserId = sessionService.CurrentSession.UserId;
            commandBase.MessageId = Guid.NewGuid();

            //redisService.Publish(MessageBrokerRedisChannel.EnqueueMessageChannel, JsonConvert.SerializeObject(new { MessageId = commandBase.MessageId, UserId = commandBase.UserId }), StackExchange.Redis.CommandFlags.FireAndForget);

            return commandBase;
        }
    }
}