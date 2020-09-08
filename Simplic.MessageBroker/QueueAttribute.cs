using System;

namespace Simplic.MessageBroker
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QueueAttribute : Attribute
    {
        public QueueAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Type
        /// </summary>
        public QueueType Type { get; }
    }
}
