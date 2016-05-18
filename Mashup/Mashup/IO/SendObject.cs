//-----------------------------------------------------------------------
// <copyright file="SendObject.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.IO
{
    using Provider.Entity;
    using System.Collections.Generic;
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
        private string mediaType;

        /// <summary>
        /// 
        /// </summary>
        private string methodType;

        /// <summary>
        /// The identifiers
        /// </summary>
        private Dictionary<string, string> identifiers;

        /// <summary>
        /// The favorite language
        /// </summary>
        private string language;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaType"></param>
        /// <param name="identifiers"></param>
        /// <param name="language"></param>
        public SendObject(string mediaType, string methodType, Dictionary<string, string> identifiers, string language)
        {
            this.MediaType = mediaType;
            this.MethodType = methodType;
            this.Identifiers = identifiers;
            this.Language = language;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "MediaType")]
        public string MediaType
        {
            get
            {
                return mediaType;
            }

            set
            {
                mediaType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "MethodType")]
        public string MethodType
        {
            get
            {
                return methodType;
            }

            set
            {
                methodType = value;
            }
        }

        /// <summary>
        /// The identifiers
        /// </summary>
        [DataMember(Name = "Identifiers")]
        public Dictionary<string, string> Identifiers
        {
            get
            {
                return identifiers;
            }

            set
            {
                identifiers = value;
            }
        }        

        /// <summary>
        /// Gets or sets the favorite language
        /// </summary>
        [DataMember(Name = "FavoriteLanguage")]
        public string Language
        {
            get
            {
                return this.language;
            }

            set
            {
                this.language = value;
            }
        }
    }
}
