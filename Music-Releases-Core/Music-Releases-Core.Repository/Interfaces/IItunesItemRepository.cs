using System;

namespace Music_Releases_Core.Repository.Interfaces
{
    public interface IItunesItemRepository
    {
        ICatalogueInfo GetInfo(string searchTerm);
    }
}
