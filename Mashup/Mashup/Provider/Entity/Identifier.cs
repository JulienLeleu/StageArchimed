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
        /// Represents the $$(Music Brainz)$$ ID
        /// </summary>
        Mbid,

        /// <summary>
        /// Represents the API specific identifier for a title
        /// </summary>
        Id_Title,

        /// <summary>
        /// Represents the API specific identifier for an author
        /// </summary>
        Id_Author,

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