using AutoMapper;
using MassTransit;
using Newtonsoft.Json;
using Simplic.MessageChannel;
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
        private readonly IChannelPublisher channelPublisher;
        private readonly ISessionService sessionService;

        /// <summary>
        /// Initializes a new instance of PublishService
        /// </summary>
        /// <param name="busControl"></param>
        public MessageBus(IBusControl busControl, IChannelPublisher channelPublisher, ISessionService sessionService)
        {
            bus = busControl;
            this.channelPublisher = channelPublisher;
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
            message = PublishInMessageChannel<T>(message);
            bus.Publish<T>(message, cancellationToken);
        }

        /// <summary>
        /// Publishes a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Publish(ICommandBase message, CancellationToken cancellationToken = default)
        {
            message = PublishInMessageChannel(message);
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
            message = PublishInMessageChannel(message);
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
            try
            {
                var type = values.GetType();

                var id = (Guid)type.GetProperty("MessageId").GetValue(values);
                if (id == null || id == Guid.Empty)
                    throw new Exception("No MessageId set");

                PublishInMessageChannel(id);
                bus.Publish<T>(values, cancellationToken);
            }
            catch
            {
                bus.Publish<T>(values, cancellationToken);
            }
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="cancellationToken"></param>
        public void Send<T>(T message, CancellationToken cancellationToken = default) where T : class, ICommandBase
        {
            message = PublishInMessageChannel(message);
            bus.Send<T>(message, cancellationToken);
        }

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="cancellationToken"></param>
        public void Send(ICommandBase message, CancellationToken cancellationToken = default)
        {
            message = PublishInMessageChannel(message);
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
            message = PublishInMessageChannel(message);
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
            var type = values.GetType();

            var id = (Guid)type.GetProperty("MessageId").GetValue(values);
            if (id == null || id == Guid.Empty)
                throw new Exception("No MessageId set");

            PublishInMessageChannel(id);
            bus.Send<T>(values, cancellationToken);
        }

        /// <summary>
        /// Publishes a message to a message channel
        /// </summary>
        /// <param name="commandBase"></param>
        private T PublishInMessageChannel<T>(T commandBase) where T : ICommandBase
        {
            try
            {
                commandBase.UserId = sessionService.CurrentSession.UserId;
                commandBase.MessageId = Guid.NewGuid();

                channelPublisher.Publish(MessageBrokerChannel.EnqueueMessageChannel, JsonConvert.SerializeObject(new { MessageId = commandBase.MessageId, UserId = commandBase.UserId }));
            }
            catch (Exception ex)
            {
                Log.LogManagerInstance.Instance.Error("Error while publishing to message channel db", ex);
            };

            return commandBase;
        }

        /// <summary>
        /// Publishes a message to a channel
        /// </summary>
        /// <param name="messageId"></param>
        private void PublishInMessageChannel(Guid messageId)
        {
            try
            {
                channelPublisher.Publish(MessageBrokerChannel.EnqueueMessageChannel, JsonConvert.SerializeObject(new { MessageId = messageId, UserId = sessionService.CurrentSession.UserId }));
            }
            catch (Exception ex)
            {
                Log.LogManagerInstance.Instance.Error("Error while publishing to message channel db", ex);
            }
        }
    }
}