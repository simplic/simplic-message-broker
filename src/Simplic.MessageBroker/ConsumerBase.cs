using MassTransit;
using Newtonsoft.Json;
using Simplic.Redis;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Abstract class to define a base for all in Simplic used consumers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConsumerBase<T> : IConsumer<T> where T : class, ICommandBase
    {
        private readonly IRedisService redisService;

        /// <summary>
        /// Initialize a new instance of ConsumerBase
        /// </summary>
        /// <param name="redisService">An instance of IRedisService</param>
        public ConsumerBase(IRedisService redisService)
        {
            this.redisService = redisService;
        }

        /// <summary>
        /// Consumes a context. Gets called whenever the consumer gets a message of the given type
        /// </summary>
        /// <param name="context">The consumer context of the given message type</param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<T> context)
        {
            await Execute(context.Message);

            redisService.Publish(MessageBrokerRedisChannel.CompleteMessageChannel, JsonConvert.SerializeObject(new { MessageId = context.Message.MessageId, UserId = context.Message.UserId }), CommandFlags.FireAndForget);
        }

        /// <summary>
        /// The Execute function that needs to be implemented and should contain the consumer logic
        /// </summary>
        /// <param name="message">The message of the consumed context</param>
        /// <returns></returns>
        public abstract Task Execute(T message);
    }
}