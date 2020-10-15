using MassTransit;
using Newtonsoft.Json;
using Simplic.InMemoryDB;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Abstract class to define a base for all in Simplic used consumers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConsumerBase<T> : IConsumer<T> where T : class
    {
        private readonly IKeyValueStore keyValueStore;

        /// <summary>
        /// Initialize a new instance of ConsumerBase
        /// </summary>
        /// <param name="keyValueStore">An instance of IChannelPublisher</param>
        public ConsumerBase(IKeyValueStore keyValueStore)
        {
            this.keyValueStore = keyValueStore;
        }

        /// <summary>
        /// Consumes a context. Gets called whenever the consumer gets a message of the given type
        /// </summary>
        /// <param name="context">The consumer context of the given message type</param>
        /// <returns></returns>
        public virtual async Task Consume(ConsumeContext<T> context)
        {

            // Try to get the userid from the message header
            int userId;
            if (!context.Headers.Any(x => x.Key.ToLower() == "userid"))
                throw new MissingUserIdException("No UserId header set.");

            // We can use first since we already checked wherther we have a header with a userId
            if (!int.TryParse(context.Headers.First(x => x.Key.ToLower() == "userid").Value.ToString(), out userId))
                throw new UnableToParseUserIdException("UserId could not be parsed");

            try
            {
                // Try to call the execute function
                await Execute(context);
            }
            catch (Exception ex)
            {
                Log.LogManagerInstance.Instance.Error($"Error while executing consume in consumer: {this.GetType().Name}", ex);
            }

            try
            {
                // Dcrease the task in the global and the user queue
                keyValueStore.StringDecrement(MessageBrokerQueueKeys.GlobalQueueKey);
                keyValueStore.StringDecrement(MessageBrokerQueueKeys.GetUserQueueKey(userId));
            }
            catch (Exception ex)
            {
                Log.LogManagerInstance.Instance.Error("Error while updating in memory db key.", ex);
            }
        }

        /// <summary>
        /// The Execute function that needs to be implemented and should contain the consumer logic
        /// </summary>
        /// <param name="message">The message of the consumed context</param>
        /// <returns></returns>
        public abstract Task Execute(ConsumeContext<T> message);
    }
}