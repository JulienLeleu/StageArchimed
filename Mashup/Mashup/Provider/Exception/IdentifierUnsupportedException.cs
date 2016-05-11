//-----------------------------------------------------------------------
// <copyright file="IdentifierUnsupportedException.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Exception
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// This exception occurred when an identifier is Unsupported by a provider
    /// </summary>
    [Serializable]
    public class IdentifierUnsupportedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnsupportedException"/> class
        /// </summary>
        public IdentifierUnsupportedException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnsupportedException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        public IdentifierUnsupportedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnsupportedException"/> class
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="inner">The inner</param>
        public IdentifierUnsupportedException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnsupportedException"/> class
        /// </summary>
        /// <param name="info">The info</param>
        /// <param name="context">The context</param>
        protected IdentifierUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
