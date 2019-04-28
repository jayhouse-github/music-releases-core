using System;

namespace Music_Releases_Core.Repository
{
    internal class ItunesItemCollection
    {
        public int ResultCount { get; set; }
        public Result[] Results { get; set; }
    }

    internal class Result
    {
        public string WrapperType { get; set; }
        public string CollectionType { get; set; }
        public int ArtistId { get; set; }
        public int CollectionId { get; set; }
        public int AmgArtistId { get; set; }
        public string ArtistName { get; set; }
        public string CollectionName { get; set; }
        public string CollectionCensoredName { get; set; }
        public string ArtistViewUrl { get; set; }
        public string CollectionViewUrl { get; set; }
        public string ArtworkUrl60 { get; set; }
        public string ArtworkUrl100 { get; set; }
        public float CollectionPrice { get; set; }
        public string CollectionExplicitness { get; set; }
        public int TrackCount { get; set; }
        public string Copyright { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PrimaryGenreName { get; set; }
    }
}
