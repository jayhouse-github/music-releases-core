using System;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.BL
{
    public class AmazonItemSearch
    {
        IAmazonItemRepository _repo;

        public AmazonItemSearch(IAmazonItemRepository inRepo)
        {
            _repo = inRepo;
        }

        public ExtendedItem GetByASIN(string inAsin)
        {
            var returnedItem = _repo.LookupASIN(inAsin);

            if (returnedItem.ASIN == null)
                return null;

            return new ExtendedItem
            {
                Artist = returnedItem.Artist,
                Title = returnedItem.Title,
                Url = returnedItem.Url,
                Price = Convert.ToDecimal(returnedItem.Price) / 100,
                PicUrl = returnedItem.PicUrl,
                ReleaseDate = DateTime.Parse(returnedItem.ReleaseDate),
                Asin = returnedItem.ASIN
            };
        }

        public ExtendedItem GetBySearchTerm(string inSearchTerm, SearchIndex index)
        {
            var returnedItem = _repo.LookupSearchString(inSearchTerm, index);
            if (returnedItem.ASIN == null)
                return null;

            return new ExtendedItem
            {
                Artist = returnedItem.Artist,
                Title = returnedItem.Title,
                Url = returnedItem.Url,
                Price = Convert.ToDecimal(returnedItem.Price) / 100,
                PicUrl = returnedItem.PicUrl,
                ReleaseDate = DateTime.Parse(returnedItem.ReleaseDate),
                Asin = returnedItem.ASIN
            };
        }
    }
}
