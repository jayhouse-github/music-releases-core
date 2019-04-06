using System;

namespace Music_Releases_Core.Repository.Helpers
{
    internal static class ItunesHelper
    {
        internal static ICatalogueInfo GetNewCatalogueObjectFromItunesObject(Result itunesItem)
        {
            ICatalogueInfo newItem = new CatalogueInfo
            {
                Artist = itunesItem.ArtistName,
                Title = itunesItem.CollectionName,
                Url = itunesItem.CollectionViewUrl
            };

            newItem.Price = Convert.ToInt32(itunesItem.CollectionPrice * 100);

            return newItem;
        }
    }
}
