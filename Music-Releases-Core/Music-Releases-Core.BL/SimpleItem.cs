using System;

namespace Music_Releases_Core.BL
{
    public class SimpleItem
    {
        private string _artist;
        private string _title;
        private string _url;
        private decimal _price;
        private string _source;

        public string Artist { get => _artist; set => _artist = value; }
        public string Title { get => _title; set => _title = value; }
        public string Url { get => _url; set => _url = value; }
        public decimal Price { get => _price; set => _price = value; }
        public string Source { get => _source; set => _source = value; }
    }
}
