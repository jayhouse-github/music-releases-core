using System;
using System.Collections.Generic;
using System.Linq;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.BL
{
    public class AmazonSearch
    {
        IAmazonSearchRepository _repo;

        public AmazonSearch(IAmazonSearchRepository inRepo)
        {
            _repo = inRepo;
        }

        public IList<ExtendedItem> SearchFromCommaSeparatedList(string searchString)
        {
            IList<ExtendedItem> returnList = new List<ExtendedItem>();
            var returnedItems = _repo.SearchAmazon(searchString);

            foreach (var item in returnedItems)
            {
                ExtendedItem newItem = new ExtendedItem
                {
                    Artist = item.Artist,
                    Title = item.Title,
                    Url = item.Url,
                    Price = Convert.ToDecimal(item.Price) / 100,
                    PicUrl = item.PicUrl,
                    ReleaseDate = DateTime.Parse(item.ReleaseDate),
                    Asin = item.ASIN
                };

                returnList.Add(newItem);
            }

            returnList = returnList.OrderBy(a => a.ReleaseDate).ToList();

            return returnList;
        }
    }
}
