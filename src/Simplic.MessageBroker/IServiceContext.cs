namespace Simplic.MessageBroker
{
    /// <summary>
    /// Adds the possibility to give a consumer a context
    /// </summary>
    public interface IServiceContext
    {
        /// <summary>
        /// Gets the context (module) name
        /// </summary>
        string Context { get; }
    }
}
