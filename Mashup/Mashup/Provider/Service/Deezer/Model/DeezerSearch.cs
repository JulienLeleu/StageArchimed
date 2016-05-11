namespace Mashup.Provider.Service.Deezer.Model.Search
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Artist
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        [DataMember(Name = "picture_small")]
        public string PictureSmall { get; set; }

        [DataMember(Name = "picture_medium")]
        public string PictureMedium { get; set; }

        [DataMember(Name = "picture_big")]
        public string PictureBig { get; set; }

        [DataMember(Name = "tracklist")]
        public string Tracklist { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Album
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "cover")]
        public string Cover { get; set; }

        [DataMember(Name = "cover_small")]
        public string CoverSmall { get; set; }

        [DataMember(Name = "cover_medium")]
        public string CoverMedium { get; set; }

        [DataMember(Name = "cover_big")]
        public string CoverBig { get; set; }

        [DataMember(Name = "tracklist")]
        public string Tracklist { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Datum
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "readable")]
        public bool Readable { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "title_short")]
        public string TitleShort { get; set; }

        [DataMember(Name = "title_version")]
        public string TitleVersion { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        [DataMember(Name = "preview")]
        public string Preview { get; set; }

        [DataMember(Name = "artist")]
        public Artist Artist { get; set; }

        [DataMember(Name = "album")]
        public Album Album { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class DeezerSearch
    {

        [DataMember(Name = "data")]
        public IList<Datum> Data { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "next")]
        public string Next { get; set; }
    }

}
