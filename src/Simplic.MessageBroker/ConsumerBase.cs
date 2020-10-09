using MassTransit;
using Newtonsoft.Json;
using Simplic.MessageChannel;
using System.Threading.Tasks;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Abstract class to define a base for all in Simplic used consumers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConsumerBase<T> : IConsumer<T> where T : class, ICommandBase
    {
        private readonly IChannelPublisher channelPublisher;

        /// <summary>
        /// Initialize a new instance of ConsumerBase
        /// </summary>
        /// <param name="channelPublisher">An instance of IChannelPublisher</param>
        public ConsumerBase(IChannelPublisher channelPublisher)
        {
            this.channelPublisher = channelPublisher;
        }

        /// <summary>
        /// Consumes a context. Gets called whenever the consumer gets a message of the given type
        /// </summary>
        /// <param name="context">The consumer context of the given message type</param>
        /// <returns></returns>
        public virtual async Task Consume(ConsumeContext<T> context)
        {
            try
            {
                await Execute(context);
            }
            catch
            {
                Log.LogManagerInstance.Instance.Error($"Error while executing consume in consumer: {this.GetType().Name}");
            }

            channelPublisher.Publish(MessageBrokerChannel.CompleteMessageChannel, JsonConvert.SerializeObject(new { MessageId = context.Message.MessageId, UserId = context.Message.UserId }));
        }

        /// <summary>
        /// The Execute function that needs to be implemented and should contain the consumer logic
        /// </summary>
        /// <param name="message">The message of the consumed context</param>
        /// <returns></returns>
        public abstract Task Execute(ConsumeContext<T> message);
    }
}