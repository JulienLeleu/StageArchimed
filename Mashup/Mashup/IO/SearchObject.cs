//-----------------------------------------------------------------------
// <copyright file="SendObject.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.IO
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The object sent to provider manager 
    /// </summary>
    [DataContract(Name = "SearchObject")]
    [KnownType(typeof(SearchObject))]
    public class SearchObject
    {
        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The author
        /// </summary>
        private string author;

        /// <summary>
        /// The ean
        /// </summary>
        private string ean;

        /// <summary>
        /// The ean
        /// </summary>
        private string language;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendObject"/> class
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="author">The author</param>
        /// <param name="ean">The ean</param>
        /// <param name="language">The language</param>
        public SearchObject(string title, string author, string ean, string language)
        {
            this.Title = title;
            this.Author = author;
            this.Ean = ean;
            this.Language = language;
        }

        [DataMember]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        [DataMember]
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }

        [DataMember]
        public string Ean
        {
            get
            {
                return ean;
            }

            set
            {
                ean = value;
            }
        }

        [DataMember]
        public string Language
        {
            get
            {
                return language;
            }

            set
            {
                language = value;
            }
        }
    }
}
