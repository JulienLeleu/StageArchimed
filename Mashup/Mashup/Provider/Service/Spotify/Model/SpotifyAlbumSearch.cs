namespace Mashup.Provider.Service.Spotify.Model.AlbumSearch
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class ExternalUrls
    {

        [DataMember(Name = "spotify")]
        public string Spotify { get; set; }
    }

    [DataContract]
    public class Image
    {

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }
    }

    [DataContract]
    public class Item
    {

        [DataMember(Name = "album_type")]
        public string AlbumType { get; set; }

        [DataMember(Name = "available_markets")]
        public IList<string> AvailableMarkets { get; set; }

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "images")]
        public IList<Image> Images { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Albums
    {

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "items")]
        public IList<Item> Items { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "next")]
        public string Next { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "previous")]
        public object Previous { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class SpotifyAlbumSearch
    {

        [DataMember(Name = "albums")]
        public Albums Albums { get; set; }
    }
}
