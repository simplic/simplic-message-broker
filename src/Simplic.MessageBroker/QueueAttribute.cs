using System;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Class to represent a queue attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class QueueAttribute : Attribute, IServiceContext
    {
        /// <summary>
        /// Initializes a new instance of QueueAttribute
        /// </summary>
        /// <param name="name">Name of the queue Attribute</param>
        /// <param name="context">The context in which the consumer will be enabled</param>
        /// <param name="type">The consumer type</param>
        public QueueAttribute(string name, string context, QueueType type = QueueType.Server)
        {
            Name = name;
            Context = context;
            Type = type;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; }

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