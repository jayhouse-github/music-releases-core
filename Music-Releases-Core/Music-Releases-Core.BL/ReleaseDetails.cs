using System;
using System.Collections.Generic;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.BL
{
    public class ReleaseDetails
    {
        IAmazonItemRepository _amazonItemRepo;
        IItunesItemRepository _itunesItemRepo;

        public ReleaseDetails(IAmazonItemRepository inAmazonItemRepo, IItunesItemRepository inItunesItemRepo)
        {
            _amazonItemRepo = inAmazonItemRepo;
            _itunesItemRepo = inItunesItemRepo;
        }

        public ReleaseDetailItem GetReleaseDetailsFromASIN(string inAsin)
        {
            var releaseDetailItem = new ReleaseDetailItem();
            IList<SimpleItem> releaseItems = new List<SimpleItem>();

            var amazonSearch = new AmazonItemSearch(_amazonItemRepo);
            var amazonSearchResult = amazonSearch.GetByASIN(inAsin);

            if (amazonSearchResult != null)
            {
                releaseDetailItem.Asin = inAsin;
                releaseDetailItem.PicUrl = amazonSearchResult.PicUrl;
                releaseDetailItem.ReleaseDate = amazonSearchResult.ReleaseDate;
                releaseItems.Add(new SimpleItem
                {
                    Artist = amazonSearchResult.Artist,
                    Title = amazonSearchResult.Title,
                    Url = amazonSearchResult.Url,
                    Price = amazonSearchResult.Price,
                    Source = "AmazonCD"
                });

                var searchTerm = amazonSearchResult.Artist + " " + amazonSearchResult.Title;
                var mp3Result = amazonSearch.GetBySearchTerm(searchTerm, Repository.SearchIndex.MP3);

                if (mp3Result != null)
                {
                    releaseItems.Add(new SimpleItem
                    {
                        Artist = mp3Result.Artist,
                        Title = mp3Result.Title,
                        Url = mp3Result.Url,
                        Price = mp3Result.Price,
                        Source = "AmazonMP3"
                    });
                }

                //Lookup iTunes
                var iTunesSearch = new ItunesItemSearch(_itunesItemRepo);
                var iTunesResult = iTunesSearch.GetBySearchTerm(searchTerm);

                if (iTunesResult != null)
                {
                    releaseItems.Add(new SimpleItem
                    {
                        Artist = iTunesResult.Artist,
                        Title = iTunesResult.Title,
                        Url = iTunesResult.Url,
                        Price = iTunesResult.Price,
                        Source = "ITunes"
                    });
                }
            }
            releaseDetailItem.Items = releaseItems;

            return releaseDetailItem;
        }
    }
}
