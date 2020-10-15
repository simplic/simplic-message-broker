using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.MessageBroker
{
    /// <summary>
    /// Exception to signalize that the given userid could not be parsed as int
    /// </summary>
    public class UnableToParseUserIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Simplic.MessageBroker.UnableToParseUserIdException class.
        /// </summary>
        public UnableToParseUserIdException() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Simplic.MessageBroker.UnableToParseUserIdException class
        /// with a specified error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UnableToParseUserIdException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Simplic.MessageBroker.UnableToParseUserIdException class
        /// with a specified error message and a reference to the inner exception that is the cause
        /// of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference 
        /// (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public UnableToParseUserIdException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
