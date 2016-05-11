//-----------------------------------------------------------------------
// <copyright file="CultureUnknownException.cs" company="Archimed">
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
    public class CultureUnknownException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CultureUnknownException"/> class
        /// </summary>
        public CultureUnknownException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureUnknownException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        public CultureUnknownException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureUnknownException"/> class
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="inner">The inner</param>
        public CultureUnknownException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureUnknownException"/> class
        /// </summary>
        /// <param name="info">The info</param>
        /// <param name="context">The context</param>
        protected CultureUnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
