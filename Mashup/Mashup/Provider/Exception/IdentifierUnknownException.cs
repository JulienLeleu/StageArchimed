//-----------------------------------------------------------------------
// <copyright file="IFactory.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Runtime.Serialization;

namespace Mashup.Provider.Exception
{
    [Serializable]
    public class IdentifierUnknownException : System.Exception
    {
        public IdentifierUnknownException() : base() { }
        public IdentifierUnknownException(string message) : base(message) { }
        public IdentifierUnknownException(string message, System.Exception inner) : base(message, inner) { }
        protected IdentifierUnknownException(SerializationInfo info, StreamingContext context) : base(info,context)
        {
        }
    }
}
