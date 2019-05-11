using System;
using System.Linq;
using System.Net;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.BL
{
    public class ReleaseSearch
    {
        IAmazonItemRepository _amazonRepo;
        IItunesItemRepository _itunesRepo;

        public ReleaseSearch(IAmazonItemRepository amazonRepo, IItunesItemRepository itunesRepo)
        {
            _amazonRepo = amazonRepo;
            _itunesRepo = itunesRepo;
        }

        public MusicReleaseCollection GetDetails(string asin)
        {
            ReleaseDetailItem releaseDetail = null;
            MusicReleaseCollection musicReleaseDetailModel = null;

            if (asin != null)
            {
                try
                {
                    releaseDetail = new ReleaseDetails(_amazonRepo, _itunesRepo).GetReleaseDetailsFromASIN(asin);
                }
                catch (WebException ex)
                {
                    //Log
                    throw;
                }
            }
            else
            {
                return musicReleaseDetailModel;
            }

            var amazonCDItem = releaseDetail.Items.Where(i => i.Source.ToLower() == "amazoncd").FirstOrDefault();
            var amazonMP3Item = releaseDetail.Items.Where(i => i.Source.ToLower() == "amazonmp3").FirstOrDefault();
            var itunesItem = releaseDetail.Items.Where(i => i.Source.ToLower() == "itunes").FirstOrDefault();

            if (amazonCDItem == null && amazonMP3Item == null && itunesItem == null)
            {
                return musicReleaseDetailModel;
            }

            musicReleaseDetailModel = new MusicReleaseCollection
            {
                PicUrl = releaseDetail.PicUrl,
                ReleaseDate = releaseDetail.ReleaseDate,
                Asin = releaseDetail.Asin,
                AmazonCD = amazonCDItem,
                AmazonMP3 = amazonMP3Item,
                ITunes = itunesItem
            };

            if (amazonCDItem != null)
            {
                musicReleaseDetailModel.Artist = amazonCDItem.Artist;
                musicReleaseDetailModel.Title = amazonCDItem.Title;
            }
            else if (amazonMP3Item != null)
            {
                amazonMP3Item.Artist = amazonCDItem.Artist;
                amazonMP3Item.Title = amazonCDItem.Title;
            }
            else if (itunesItem != null)
            {
                itunesItem.Artist = amazonCDItem.Artist;
                itunesItem.Title = amazonCDItem.Title;
            }

            return musicReleaseDetailModel;
        }
    }
}
