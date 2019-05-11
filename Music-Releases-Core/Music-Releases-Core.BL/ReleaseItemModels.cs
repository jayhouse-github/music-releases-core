using System;

namespace Music_Releases_Core.BL
{
    public class MusicReleaseCollection
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string PicUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Asin { get; set; }
        public SimpleItem AmazonCD { get; set; }
        public SimpleItem AmazonMP3 { get; set; }
        public SimpleItem ITunes { get; set; }
    }
}
