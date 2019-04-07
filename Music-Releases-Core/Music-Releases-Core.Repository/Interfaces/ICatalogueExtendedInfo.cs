using System;
namespace Music_Releases_Core.Repository.Interfaces
{
    public interface ICatalogueExtendedInfo : ICatalogueInfo
    {
        string PicUrl { get; set; }
        string ReleaseDate { get; set; }
        string ASIN { get; set; }
    }
}
