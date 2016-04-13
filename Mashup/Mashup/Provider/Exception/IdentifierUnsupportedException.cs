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
    public class IdentifierUnsupportedException : System.Exception
    {
        public IdentifierUnsupportedException() : base() { }
        public IdentifierUnsupportedException(string message) : base(message) { }
        public IdentifierUnsupportedException(string message, System.Exception inner) : base(message, inner) { }
        protected IdentifierUnsupportedException(SerializationInfo info, StreamingContext context) : base(info,context)
        {
        }
    }
}
