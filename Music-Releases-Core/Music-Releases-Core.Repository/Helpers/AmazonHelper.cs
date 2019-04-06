using System;
using System.Collections.Generic;

namespace Music_Releases_Core.Repository.Helpers
{
    internal static class AmazonHelper
    {
        internal static IDictionary<string, string> GetBaseSearchParams(Enums.SearchType type)
        {
            IDictionary<string, string> searchParams = new Dictionary<string, string>();

            switch (type)
            {
                case Enums.SearchType.ASIN:
                    searchParams["Operation"] = "ItemLookup";
                    searchParams["ResponseGroup"] = "Medium";
                    searchParams["Service"] = "AWSCommerceService";
                    break;
                case Enums.SearchType.SearchTerm:
                    searchParams["Operation"] = "ItemSearch";
                    searchParams["ResponseGroup"] = "Medium";
                    searchParams["Service"] = "AWSCommerceService";
                    break;
                case Enums.SearchType.ArtistSearch:
                    searchParams["Condition"] = "New";
                    searchParams["Format"] = "Audio CD";
                    searchParams["Operation"] = "ItemSearch";
                    searchParams["ResponseGroup"] = "Medium";
                    searchParams["SearchIndex"] = "Music";
                    searchParams["Service"] = "AWSCommerceService";
                    searchParams["Sort"] = "-releasedate";
                    break;
            }

            return searchParams;
        }

        internal static ICatalogueExtendedInfo GetNewCatalogueObjectFromXmlNode(XmlNode node)
        {
            string artist = null;
            string title = null;
            string url = null;
            string asin = null;
            string releaseDate = null;
            string pic = null;
            string stringPrice = "0";
            XmlNode item = node;

            if (item["SmallImage"] != null)
            {
                pic = item["SmallImage"]["URL"].InnerText;
            }

            //Artist name will be either in artist or creator
            if (item["ItemAttributes"]["Artist"] != null)
            {
                artist = item["ItemAttributes"]["Artist"].InnerText;
            }
            else
            {
                artist = item["ItemAttributes"]["Creator"].InnerText;
            }

            title = item["ItemAttributes"]["Title"].InnerText;
            url = item["DetailPageURL"].InnerText;
            asin = item["ASIN"].InnerText;

            if (item["ItemAttributes"]["ReleaseDate"] != null)
                releaseDate = item["ItemAttributes"]["ReleaseDate"].InnerText;
            else if (item["ItemAttributes"]["PublicationDate"] != null)
                releaseDate = item["ItemAttributes"]["PublicationDate"].InnerText;
            else
                releaseDate = DateTime.Now.ToShortTimeString();

            if (item["OfferSummary"]["LowestNewPrice"] != null)
            {
                stringPrice = item["OfferSummary"]["LowestNewPrice"]["Amount"].InnerText;
            }
            else
            {
                stringPrice = "0";
            }

            Int32.TryParse(stringPrice, out int price);

            ICatalogueExtendedInfo newItem = new CatalogueExtendedInfo
            {
                Artist = artist,
                Title = title,
                PicUrl = pic,
                ASIN = asin,
                ReleaseDate = releaseDate,
                Price = price,
                Url = url
            };

            return newItem;
        }
    }
}
