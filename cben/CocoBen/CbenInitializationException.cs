using System;
using System.Runtime.Serialization;

namespace Cben
{
    /// <summary>
    /// This exception is thrown if a problem on Cben initialization progress.
    /// </summary>
    [Serializable]
    public class CbenInitializationException : CbenException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CbenInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public CbenInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CbenInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CbenInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
