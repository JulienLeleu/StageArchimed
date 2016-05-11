//-----------------------------------------------------------------------
// <copyright file="IdentifierUnknownException.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Exception
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// This exception occurred when an identifier is unknown by the provider manager
    /// </summary>
    [Serializable]
    public class IdentifierUnknownException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnknownException"/> class
        /// </summary>
        public IdentifierUnknownException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnknownException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        public IdentifierUnknownException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnknownException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="inner">The inner</param>
        public IdentifierUnknownException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUnknownException"/> class
        /// </summary>
        /// <param name="info">The info</param>
        /// <param name="context">The context</param>
        protected IdentifierUnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
