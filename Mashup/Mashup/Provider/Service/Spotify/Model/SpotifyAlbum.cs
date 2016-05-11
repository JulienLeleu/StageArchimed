using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mashup.Provider.Service.Spotify.Model.Album
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
    public class Artist
    {

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Copyright
    {

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class ExternalIds
    {

        [DataMember(Name = "upc")]
        public string Upc { get; set; }
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

        [DataMember(Name = "artists")]
        public IList<Artist> Artists { get; set; }

        [DataMember(Name = "available_markets")]
        public IList<string> AvailableMarkets { get; set; }

        [DataMember(Name = "disc_number")]
        public int DiscNumber { get; set; }

        [DataMember(Name = "duration_ms")]
        public int DurationMs { get; set; }

        [DataMember(Name = "explicit")]
        public bool Explicit { get; set; }

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "preview_url")]
        public string PreviewUrl { get; set; }

        [DataMember(Name = "track_number")]
        public int TrackNumber { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract]
    public class Tracks
    {

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "items")]
        public IList<Item> Items { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "next")]
        public object Next { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "previous")]
        public object Previous { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class SpotifyAlbum
    {
        [DataMember(Name = "album_type")]
        public string AlbumType { get; set; }

        [DataMember(Name = "artists")]
        public IList<Artist> Artists { get; set; }

        [DataMember(Name = "available_markets")]
        public IList<string> AvailableMarkets { get; set; }

        [DataMember(Name = "copyrights")]
        public IList<Copyright> Copyrights { get; set; }

        [DataMember(Name = "external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [DataMember(Name = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [DataMember(Name = "genres")]
        public IList<object> Genres { get; set; }

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

        [DataMember(Name = "release_date")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [DataMember(Name = "tracks")]
        public Tracks Tracks { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }
}
