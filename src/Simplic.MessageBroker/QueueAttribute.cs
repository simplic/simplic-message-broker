using System;

namespace Simplic.MessageBroker
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QueueAttribute : Attribute
    {
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