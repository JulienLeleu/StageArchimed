namespace Mashup.Provider.Service.Spotify.Model.Artist
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
    public class Followers
    {

        [DataMember(Name = "href")]
        public object Href { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
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
    public class SpotifyArtist
    {

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "followers")]
        public Followers Followers { get; set; }

        [DataMember(Name = "genres")]
        public IList<string> Genres { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "images")]
        public IList<Image> Images { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "popularity")]
        public int Popularity { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

}
