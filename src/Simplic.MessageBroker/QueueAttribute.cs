using System;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Class to represent a queue attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class QueueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of QueueAttribute
        /// </summary>
        /// <param name="name">Name of the queue Attribute</param>
        /// <param name="context">The context in which the consumer will be enabled</param>
        /// <param name="type">The consumer type</param>
        /// <param name="filterContext">Defines whether to filter the actual context</param>
        public QueueAttribute(string name, string context, QueueType type = QueueType.Server, bool filterContext = false)
        {
            Name = name;
            Context = context;
            Type = type;
            FilterContext = filterContext;
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

        /// <summary>
        /// Gets whether to filter the actual context
        /// </summary>
        public bool FilterContext { get; }
    }
}