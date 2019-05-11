using System;
using Music_Releases_Core.Repository.Enums;

namespace Music_Releases_Core.Repository.Interfaces
{
    public interface IAmazonItemRepository
    {
        ICatalogueExtendedInfo LookupASIN(string ASINNumber);
        ICatalogueExtendedInfo LookupSearchString(string searchString, SearchIndex searchIndex);
    }
}
