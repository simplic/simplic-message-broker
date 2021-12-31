using System;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Attribute for marking a consumer as queueless-consumer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NoQueueAttribute : Attribute, IServiceContext
    {
        /// <summary>
        /// Initializes a new instance of NoQueueAttribute
        /// </summary>
        /// <param name="context">The context in which the consumer will be enabled</param>
        /// <param name="type">The consumer type</param>
        public NoQueueAttribute(string context, QueueType type = QueueType.Server)
        {
            Context = context;
            Type = type;
        }

        /// <summary>
        /// Gets the Type
        /// </summary>
        public QueueType Type { get; }

        /// <summary>
        /// Gets the context
        /// </summary>
        public string Context { get; }
    }
}