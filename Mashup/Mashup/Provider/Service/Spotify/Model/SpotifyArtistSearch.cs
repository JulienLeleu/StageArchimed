namespace Mashup.Provider.Service.Spotify.Model.ArtistSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

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
    public class Item
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

    [DataContract]
    public class Artists
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
    public class SpotifyArtistSearch
    {

        [DataMember(Name = "artists")]
        public Artists Artists { get; set; }
    }
}
