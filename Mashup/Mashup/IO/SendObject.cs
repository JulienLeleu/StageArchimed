//-----------------------------------------------------------------------
// <copyright file="SendObject.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.IO
{
    using Provider.Entity;
    using System.Runtime.Serialization;

    /// <summary>
    /// The object sent to provider manager 
    /// </summary>
    [DataContract(Name = "SendObject")]
    [KnownType(typeof(SendObject))]
    public class SendObject
    {
        /// <summary>
        ///  The media type
        /// </summary>
        private Media mediaType;

        /// <summary>
        /// The identifier key
        /// </summary>
        private string identifierKey;

        /// <summary>
        /// The identifier value
        /// </summary>
        private string identifierValue;

        /// <summary>
        /// The favorite language
        /// </summary>
        private string favoriteLanguage;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendObject"/> class
        /// </summary>
        /// <param name="identifierKey">The identifier key</param>
        /// <param name="identifierValue">The identifier value</param>
        public SendObject(string identifierKey, string identifierValue)
        {
            this.IdentifierKey = identifierKey;
            this.IdentifierValue = identifierValue;
            this.FavoriteLanguage = "FR";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendObject"/> class
        /// </summary>
        /// <param name="identifierKey">The identifier key</param>
        /// <param name="identifierValue">The identifier value</param>
        /// <param name="favoriteLanguage">The favorite language</param>
        public SendObject(string identifierKey, string identifierValue, string favoriteLanguage) : this(identifierKey, identifierValue)
        {
            this.FavoriteLanguage = favoriteLanguage;
        }

        /// <summary>
        /// Gets or sets the identifier key
        /// </summary>
        [DataMember(Name = "IdentifierKey")]
        public string IdentifierKey
        {
            get
            {
                return this.identifierKey;
            }

            set
            {
                this.identifierKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier value
        /// </summary>
        [DataMember(Name = "IdentifierValue")]
        public string IdentifierValue
        {
            get
            {
                return this.identifierValue;
            }

            set
            {
                this.identifierValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the favorite language
        /// </summary>
        [DataMember(Name = "FavoriteLanguage")]
        public string FavoriteLanguage
        {
            get
            {
                return this.favoriteLanguage;
            }

            set
            {
                this.favoriteLanguage = value;
            }
        }
    }
}
