﻿//-----------------------------------------------------------------------
// <copyright file="LastFMAlbum.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
// <auto-generated/>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.LastFM.Model.Album
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Image
    {

        [DataMember(Name = "#text")]
        public string Text { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }
    }

    [DataContract]
    public class Attr
    {

        [DataMember(Name = "rank")]
        public string Rank { get; set; }
    }

    [DataContract]
    public class Streamable
    {

        [DataMember(Name = "#text")]
        public string Text { get; set; }

        [DataMember(Name = "fulltrack")]
        public string Fulltrack { get; set; }
    }

    [DataContract]
    public class Artist
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "mbid")]
        public string Mbid { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class Track
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        [DataMember(Name = "@attr")]
        public Attr Attr { get; set; }

        [DataMember(Name = "streamable")]
        public Streamable Streamable { get; set; }

        [DataMember(Name = "artist")]
        public Artist Artist { get; set; }
    }

    [DataContract]
    public class Tracks
    {

        [DataMember(Name = "track")]
        public IList<Track> Track { get; set; }
    }

    [DataContract]
    public class Tag
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class Tags
    {

        [DataMember(Name = "tag")]
        public IList<Tag> Tag { get; set; }
    }

    [DataContract]
    public class Wiki
    {

        [DataMember(Name = "published")]
        public string Published { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
    }

    [DataContract]
    public class Album
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name = "mbid")]
        public string Mbid { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "image")]
        public IList<Image> Image { get; set; }

        [DataMember(Name = "listeners")]
        public string Listeners { get; set; }

        [DataMember(Name = "playcount")]
        public string Playcount { get; set; }

        [DataMember(Name = "tracks")]
        public Tracks Tracks { get; set; }

        [DataMember(Name = "tags")]
        public Tags Tags { get; set; }

        [DataMember(Name = "wiki")]
        public Wiki Wiki { get; set; }
    }

    [DataContract]
    public class LastFMAlbum
    {
        [DataMember(Name = "album")]
        public Album Album { get; set; }

    }
}