using System;

namespace Music_Releases_Core.BL
{
    public class ExtendedItem : SimpleItem
    {
        private string _picUrl;
        private DateTime _releaseDate;
        private string _asin;

        public string PicUrl { get => _picUrl; set => _picUrl = value; }
        public DateTime ReleaseDate { get => _releaseDate; set => _releaseDate = value; }
        public string Asin { get => _asin; set => _asin = value; }
    }
}
