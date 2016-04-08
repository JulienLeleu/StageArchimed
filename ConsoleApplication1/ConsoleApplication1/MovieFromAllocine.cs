using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace ConsoleApplication1
{
        [DataContract]
        public class MovieType
        {

            [DataMember(Name = "code")]
            public int Code { get; set; }

            [DataMember(Name = "$")]
            public string value { get; set; }
    }

    [DataContract]
    public class Nationality
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "$")]
        public string value { get; set; }
        }

        [DataContract]
    public class Genre
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "$")]
        public string value { get; set; }
        }

        [DataContract]
    public class Country
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "$")]
        public string value { get; set; }
        }

        [DataContract]
    public class ReleaseState
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "$")]
        public string value { get; set; }
        }

        [DataContract]
    public class Distributor
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class Release
    {

        [DataMember(Name = "releaseDate")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "country")]
        public Country Country { get; set; }

        [DataMember(Name = "releaseState")]
        public ReleaseState ReleaseState { get; set; }

        [DataMember(Name = "distributor")]
        public Distributor Distributor { get; set; }
    }

    [DataContract]
    public class CastingShort
    {

        [DataMember(Name = "directors")]
        public string Directors { get; set; }

        [DataMember(Name = "actors")]
        public string Actors { get; set; }
    }

    [DataContract]
    public class Person
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class Activity
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "$")]
        public string value { get; set; }
        }

        [DataContract]
    public class Picture
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    [DataContract]
    public class CastMember
    {

        [DataMember(Name = "person")]
        public Person Person { get; set; }

        [DataMember(Name = "activity")]
        public Activity Activity { get; set; }

        [DataMember(Name = "picture")]
        public Picture Picture { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }
    }

    [DataContract]
    public class Poster
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    [DataContract]
    public class Trailer
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    [DataContract]
    public class Link
    {

        [DataMember(Name = "rel")]
        public string Rel { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    [DataContract]
    public class Rating
    {

        [DataMember(Name = "note")]
        public double Note { get; set; }

        [DataMember(Name = "$")]
        public int value { get; set; }
        }

        [DataContract]
    public class Statistics
    {

        [DataMember(Name = "pressRating")]
        public double PressRating { get; set; }

        [DataMember(Name = "pressReviewCount")]
        public int PressReviewCount { get; set; }

        [DataMember(Name = "userRating")]
        public double UserRating { get; set; }

        [DataMember(Name = "userReviewCount")]
        public int UserReviewCount { get; set; }

        [DataMember(Name = "userRatingCount")]
        public int UserRatingCount { get; set; }

        [DataMember(Name = "editorialRatingCount")]
        public int EditorialRatingCount { get; set; }

        [DataMember(Name = "commentCount")]
        public int CommentCount { get; set; }

        [DataMember(Name = "photoCount")]
        public int PhotoCount { get; set; }

        [DataMember(Name = "videoCount")]
        public int VideoCount { get; set; }

        [DataMember(Name = "triviaCount")]
        public int TriviaCount { get; set; }

        [DataMember(Name = "newsCount")]
        public int NewsCount { get; set; }

        [DataMember(Name = "rankTopMovie")]
        public int RankTopMovie { get; set; }

        [DataMember(Name = "variationTopMovie")]
        public int VariationTopMovie { get; set; }

        [DataMember(Name = "awardCount")]
        public int AwardCount { get; set; }

        [DataMember(Name = "nominationCount")]
        public int NominationCount { get; set; }

        [DataMember(Name = "rating")]
        public IList<Rating> Rating { get; set; }

        [DataMember(Name = "fanCount")]
        public int FanCount { get; set; }

        [DataMember(Name = "wantToSeeCount")]
        public int WantToSeeCount { get; set; }

        [DataMember(Name = "releaseWeekPosition")]
        public int ReleaseWeekPosition { get; set; }

        [DataMember(Name = "theaterCountOnRelease")]
        public int TheaterCountOnRelease { get; set; }

        [DataMember(Name = "admissionCount")]
        public int AdmissionCount { get; set; }
    }

    [DataContract]
    public class Publication
    {

        [DataMember(Name = "dateStart")]
        public string DateStart { get; set; }
    }

    [DataContract]
    public class DisplayMode
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }
    }

    [DataContract]
    public class Category
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "$")]
        public string value { get; set; }
        }

        [DataContract]
    public class News
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "publication")]
        public Publication Publication { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "picture")]
        public Picture Picture { get; set; }

    [DataMember(Name = "displayMode")]
    public DisplayMode DisplayMode { get; set; }

    [DataMember(Name = "pageCount")]
    public int PageCount { get; set; }

    [DataMember(Name = "category")]
    public IList<Category> Category { get; set; }
        }

        [DataContract]
    public class Feature
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "publication")]
        public Publication Publication { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "picture")]
    public Picture Picture { get; set; }
        }

        [DataContract]
    public class Trivia
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "publication")]
        public Publication Publication { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "body")]
    public string Body { get; set; }
        }

        [DataContract]
    public class Writer
    {

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "avatar")]
        public string Avatar { get; set; }
    }

    [DataContract]
    public class HelpfulPositiveReview
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "opinion")]
        public int Opinion { get; set; }

        [DataMember(Name = "creationDate")]
        public string CreationDate { get; set; }

        [DataMember(Name = "writer")]
        public Writer Writer { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }
    }

    [DataContract]
    public class HelpfulNegativeReview
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "opinion")]
        public int Opinion { get; set; }

        [DataMember(Name = "creationDate")]
        public string CreationDate { get; set; }

        [DataMember(Name = "writer")]
        public Writer Writer { get; set; }

    [DataMember(Name = "body")]
    public string Body { get; set; }

    [DataMember(Name = "rating")]
    public double Rating { get; set; }
        }

        [DataContract]
    public class NewsSource
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    [DataContract]
    public class BestPressReview
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "creationDate")]
        public string CreationDate { get; set; }

        [DataMember(Name = "newsSource")]
        public NewsSource NewsSource { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "rating")]
        public double Rating { get; set; }
    }

    [DataContract]
    public class WorstPressReview
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "creationDate")]
        public string CreationDate { get; set; }

        [DataMember(Name = "newsSource")]
        public NewsSource NewsSource { get; set; }

    [DataMember(Name = "author")]
    public string Author { get; set; }

    [DataMember(Name = "body")]
    public string Body { get; set; }

    [DataMember(Name = "rating")]
    public double Rating { get; set; }
        }

        [DataContract]
    public class Movie
    {

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "movieType")]
        public MovieType MovieType { get; set; }

        [DataMember(Name = "originalTitle")]
        public string OriginalTitle { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "keywords")]
        public string Keywords { get; set; }

        [DataMember(Name = "productionYear")]
        public int ProductionYear { get; set; }

        [DataMember(Name = "nationality")]
        public IList<Nationality> Nationality { get; set; }

        [DataMember(Name = "genre")]
        public IList<Genre> Genre { get; set; }

        [DataMember(Name = "release")]
        public Release Release { get; set; }

        [DataMember(Name = "runtime")]
        public int Runtime { get; set; }

        [DataMember(Name = "synopsis")]
        public string Synopsis { get; set; }

        [DataMember(Name = "synopsisShort")]
        public string SynopsisShort { get; set; }

        [DataMember(Name = "castingShort")]
        public CastingShort CastingShort { get; set; }

        [DataMember(Name = "castMember")]
        public IList<CastMember> CastMember { get; set; }

        [DataMember(Name = "poster")]
        public Poster Poster { get; set; }

        [DataMember(Name = "trailer")]
        public Trailer Trailer { get; set; }

        [DataMember(Name = "trailerEmbed")]
        public string TrailerEmbed { get; set; }

        [DataMember(Name = "hasVOD")]
        public int HasVOD { get; set; }

        [DataMember(Name = "hasBluRay")]
        public int HasBluRay { get; set; }

        [DataMember(Name = "hasDVD")]
        public int HasDVD { get; set; }

        [DataMember(Name = "dvdReleaseDate")]
        public string DvdReleaseDate { get; set; }

        [DataMember(Name = "bluRayReleaseDate")]
        public string BluRayReleaseDate { get; set; }

        [DataMember(Name = "link")]
        public IList<Link> Link { get; set; }

        [DataMember(Name = "statistics")]
        public Statistics Statistics { get; set; }

        [DataMember(Name = "news")]
        public IList<News> News { get; set; }

        [DataMember(Name = "feature")]
        public IList<Feature> Feature { get; set; }

        [DataMember(Name = "trivia")]
        public IList<Trivia> Trivia { get; set; }

        [DataMember(Name = "helpfulPositiveReview")]
        public IList<HelpfulPositiveReview> HelpfulPositiveReview { get; set; }

        [DataMember(Name = "helpfulNegativeReview")]
        public IList<HelpfulNegativeReview> HelpfulNegativeReview { get; set; }

        [DataMember(Name = "bestPressReview")]
        public IList<BestPressReview> BestPressReview { get; set; }

        [DataMember(Name = "worstPressReview")]
        public IList<WorstPressReview> WorstPressReview { get; set; }
    }

    [DataContract]
    public class MovieFromAllocine
    {

        [DataMember(Name = "movie")]
        public Movie Movie { get; set; }
    }
}
