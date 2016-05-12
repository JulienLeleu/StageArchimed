//-----------------------------------------------------------------------
// <copyright file="Identifier.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Entity
{
    /// <summary>
    /// Types of Identifier for the request
    /// </summary>
    public enum Identifier
    {
        /// <summary>
        /// Represents the European Article Numbering
        /// </summary>
        Ean,

        /// <summary>
        /// Represents the International Standard Book Number
        /// </summary>
        Isbn,

        /// <summary>
        /// Represents the $$(Music Brainz)$$ ID
        /// </summary>
        Mbid,

        /// <summary>
        /// Represents the API specific identifier
        /// </summary>
        Id,

        /// <summary>
        /// 
        /// </summary>
        Author,

        /// <summary>
        /// 
        /// </summary>
        Title
    }
}