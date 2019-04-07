using System;
using System.Collections.Generic;

namespace Music_Releases_Core.Repository.Interfaces
{
    public interface IAmazonSearchRepository
    {
        IList<ICatalogueExtendedInfo> SearchAmazon(string searchTerms);
    }
}
