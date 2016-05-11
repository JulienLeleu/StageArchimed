using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mashup.Provider.Service.LastFM.Model.SearchArtist
{
    [DataContract]
    public class OpensearchQuery
    {

        [DataMember(Name = "#text")]
        public string Text { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [DataMember(Name = "searchTerms")]
        public string SearchTerms { get; set; }

        [DataMember(Name = "startPage")]
        public string StartPage { get; set; }
    }

    [DataContract]
    public class Image
    {

        [DataMember(Name = "#text")]
        public string Text { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }
    }

    [DataContract]
    public class Artist
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "listeners")]
        public string Listeners { get; set; }

        [DataMember(Name = "mbid")]
        public string Mbid { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "streamable")]
        public string Streamable { get; set; }

        [DataMember(Name = "image")]
        public IList<Image> Image { get; set; }
    }

    [DataContract]
    public class Artistmatches
    {

        [DataMember(Name = "artist")]
        public IList<Artist> Artist { get; set; }
    }

    [DataContract]
    public class Attr
    {

        [DataMember(Name = "for")]
        public string For { get; set; }
    }

    [DataContract]
    public class Results
    {

        [DataMember(Name = "opensearch:Query")]
        public OpensearchQuery OpensearchQuery { get; set; }

        [DataMember(Name = "opensearch:totalResults")]
        public string OpensearchTotalResults { get; set; }

        [DataMember(Name = "opensearch:startIndex")]
        public string OpensearchStartIndex { get; set; }

        [DataMember(Name = "opensearch:itemsPerPage")]
        public string OpensearchItemsPerPage { get; set; }

        [DataMember(Name = "artistmatches")]
        public Artistmatches Artistmatches { get; set; }

        [DataMember(Name = "@attr")]
        public Attr Attr { get; set; }
    }

    [DataContract]
    public class LastFMArtistSearch
    {

        [DataMember(Name = "results")]
        public Results Results { get; set; }
    }

}
