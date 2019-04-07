using System;

namespace Music_Releases_Core.Repository.Interfaces
{
    public interface ICatalogueInfo
    {
        string Artist { get; set; }
        string Title { get; set; }
        string Url { get; set; }
        int Price { get; set; }
    }
}
