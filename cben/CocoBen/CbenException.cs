using System;
using System.Runtime.Serialization;

namespace Cben
{
    /// <summary>
    /// Base exception type for those are thrown by Cben system for Cben specific exceptions.
    /// </summary>
    [Serializable]
    public class CbenException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="CbenException"/> object.
        /// </summary>
        public CbenException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="CbenException"/> object.
        /// </summary>
        public CbenException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CbenException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CbenException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CbenException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CbenException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
