﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Mashup.Provider.Service.Deezer.Model
{
    [DataContract]
    public class DataGenre
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Genres
    {
        [DataMember(Name = "data")]
        public IList<DataGenre> Data { get; set; }
    }

    [DataContract]
    public class Contributor
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "share")]
        public string Share { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        [DataMember(Name = "picture_small")]
        public string PictureSmall { get; set; }

        [DataMember(Name = "picture_medium")]
        public string PictureMedium { get; set; }

        [DataMember(Name = "picture_big")]
        public string PictureBig { get; set; }

        [DataMember(Name = "radio")]
        public bool Radio { get; set; }

        [DataMember(Name = "tracklist")]
        public string Tracklist { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }
    }

    [DataContract]
    public class Artist
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

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
    public class Datum
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

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
        public int Duration { get; set; }

        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        [DataMember(Name = "preview")]
        public string Preview { get; set; }

        [DataMember(Name = "artist")]
        public Artist Artist { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Tracks
    {
        [DataMember(Name = "data")]
        public IList<Datum> Data { get; set; }
    }

    [DataContract]
    public class DeezerAlbum
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "upc")]
        public string Upc { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "share")]
        public string Share { get; set; }

        [DataMember(Name = "cover")]
        public string Cover { get; set; }

        [DataMember(Name = "cover_small")]
        public string CoverSmall { get; set; }

        [DataMember(Name = "cover_medium")]
        public string CoverMedium { get; set; }

        [DataMember(Name = "cover_big")]
        public string CoverBig { get; set; }

        [DataMember(Name = "genre_id")]
        public int GenreId { get; set; }

        [DataMember(Name = "genres")]
        public Genres Genres { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "nb_tracks")]
        public int NbTracks { get; set; }

        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        [DataMember(Name = "fans")]
        public int Fans { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [DataMember(Name = "release_date")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "record_type")]
        public string RecordType { get; set; }

        [DataMember(Name = "available")]
        public bool Available { get; set; }

        [DataMember(Name = "tracklist")]
        public string Tracklist { get; set; }

        [DataMember(Name = "explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        [DataMember(Name = "contributors")]
        public IList<Contributor> Contributors { get; set; }

        [DataMember(Name = "artist")]
        public Artist Artist { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "tracks")]
        public Tracks Tracks { get; set; }
    }
}