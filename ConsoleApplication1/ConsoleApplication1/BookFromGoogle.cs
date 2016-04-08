using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace ConsoleApplication1
{
    [DataContract]
    public class IndustryIdentifier
    {

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "identifier")]
        public string Identifier { get; set; }

        override
        public string ToString()
        {
            return Type + " : " + Identifier; 
        }
    }

    [DataContract]
    public class ReadingModes
    {

        [DataMember(Name = "text")]
        public bool Text { get; set; }

        [DataMember(Name = "image")]
        public bool Image { get; set; }
    }

    [DataContract]
    public class Dimensions
    {

        [DataMember(Name = "height")]
        public string Height { get; set; }

        [DataMember(Name = "width")]
        public string Width { get; set; }

        [DataMember(Name = "thickness")]
        public string Thickness { get; set; }
    }

    [DataContract]
    public class ImageLinks
    {

        [DataMember(Name = "smallThumbnail")]
        public string SmallThumbnail { get; set; }

        [DataMember(Name = "thumbnail")]
        public string Thumbnail { get; set; }
    }

    [DataContract]
    public class VolumeInfo
    {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "authors")]
        public IList<string> Authors { get; set; }

        [DataMember(Name = "publisher")]
        public string Publisher { get; set; }

        [DataMember(Name = "publishedDate")]
        public string PublishedDate { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "industryIdentifiers")]
        public IList<IndustryIdentifier> IndustryIdentifiers { get; set; }

        [DataMember(Name = "readingModes")]
        public ReadingModes ReadingModes { get; set; }

        [DataMember(Name = "pageCount")]
        public int PageCount { get; set; }

        [DataMember(Name = "printedPageCount")]
        public int PrintedPageCount { get; set; }

        [DataMember(Name = "dimensions")]
        public Dimensions Dimensions { get; set; }

        [DataMember(Name = "printType")]
        public string PrintType { get; set; }

        [DataMember(Name = "categories")]
        public IList<string> Categories { get; set; }

        [DataMember(Name = "averageRating")]
        public double AverageRating { get; set; }

        [DataMember(Name = "ratingsCount")]
        public int RatingsCount { get; set; }

        [DataMember(Name = "maturityRating")]
        public string MaturityRating { get; set; }

        [DataMember(Name = "allowAnonLogging")]
        public bool AllowAnonLogging { get; set; }

        [DataMember(Name = "contentVersion")]
        public string ContentVersion { get; set; }

        [DataMember(Name = "imageLinks")]
        public ImageLinks ImageLinks { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "previewLink")]
        public string PreviewLink { get; set; }

        [DataMember(Name = "infoLink")]
        public string InfoLink { get; set; }

        [DataMember(Name = "canonicalVolumeLink")]
        public string CanonicalVolumeLink { get; set; }
    }

    [DataContract]
    public class SaleInfo
    {

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "saleability")]
        public string Saleability { get; set; }

        [DataMember(Name = "isEbook")]
        public bool IsEbook { get; set; }
    }

    [DataContract]
    public class Epub
    {

        [DataMember(Name = "isAvailable")]
        public bool IsAvailable { get; set; }
    }

    [DataContract]
    public class Pdf
    {

        [DataMember(Name = "isAvailable")]
        public bool IsAvailable { get; set; }
    }

    [DataContract]
    public class AccessInfo
    {

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "viewability")]
        public string Viewability { get; set; }

        [DataMember(Name = "embeddable")]
        public bool Embeddable { get; set; }

        [DataMember(Name = "publicDomain")]
        public bool PublicDomain { get; set; }

        [DataMember(Name = "textToSpeechPermission")]
        public string TextToSpeechPermission { get; set; }

        [DataMember(Name = "epub")]
        public Epub Epub { get; set; }

        [DataMember(Name = "pdf")]
        public Pdf Pdf { get; set; }

        [DataMember(Name = "webReaderLink")]
        public string WebReaderLink { get; set; }

        [DataMember(Name = "accessViewStatus")]
        public string AccessViewStatus { get; set; }

        [DataMember(Name = "quoteSharingAllowed")]
        public bool QuoteSharingAllowed { get; set; }
    }

    [DataContract]
    public class BookFromGoogle
    {

        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "etag")]
        public string Etag { get; set; }

        [DataMember(Name = "selfLink")]
        public string SelfLink { get; set; }

        [DataMember(Name = "volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; }

        [DataMember(Name = "saleInfo")]
        public SaleInfo SaleInfo { get; set; }

        [DataMember(Name = "accessInfo")]
        public AccessInfo AccessInfo { get; set; }

        override
        public String ToString()
        {
            string str = "[Livre] ";
            str += VolumeInfo.Title + " de ";
            foreach (string s in VolumeInfo.Authors)
            {
                str += s + " ";
            }
            str += "\n";
            str += "Description : " + VolumeInfo.Description + "\n";
            foreach (IndustryIdentifier i in VolumeInfo.IndustryIdentifiers)
            {
                str += i + "\n";
            }
            return str;
        }
    }

}
