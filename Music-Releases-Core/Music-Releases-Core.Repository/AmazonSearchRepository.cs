using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Music_Releases_Core.Repository.Helpers;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.Repository
{
    public class AmazonSearchRepository : IAmazonSearchRepository
    {
        private string _accessID;
        private string _requestEndpoint;
        private string _associateTag;
        private string _secretKey;

        public AmazonSearchRepository(string accessId, string requestEndPoint, string associateTag, string secretKey)
        {
            _accessID = accessId;
            _requestEndpoint = requestEndPoint;
            _associateTag = associateTag;
            _secretKey = secretKey;
        }

        public IList<ICatalogueExtendedInfo> SearchAmazon(string searchTerms)
        {
            var searchArray = searchTerms.Split(',');
            IDictionary<string, string> searchParams = AmazonHelper.GetBaseSearchParams(Enums.SearchType.ArtistSearch);
            IList<ICatalogueExtendedInfo> returnArray = new List<ICatalogueExtendedInfo>();

            foreach (var searchItem in searchArray)
            {
                SignedRequestHelper urlHelper = new SignedRequestHelper(this._accessID, this._secretKey, this._requestEndpoint, this._associateTag);
                searchParams["Artist"] = searchItem.Trim();
                var restUrl = urlHelper.Sign(searchParams);

                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.Load(restUrl);
                }
                catch (WebException ex)
                {
                    //Log here
                    throw;
                }

                var items = xmlDoc["ItemSearchResponse"]["Items"];

                foreach (XmlNode item in items)
                {
                    if (item.Name == "Item")
                    {
                        var newItem = AmazonHelper.GetNewCatalogueObjectFromXmlNode(item);

                        if (newItem.Artist.ToLower() == searchItem.Trim())
                        {
                            returnArray.Add(newItem);
                        }
                    }
                }

                if (returnArray.Count == 0)
                {
                    ICatalogueExtendedInfo newItem = new CatalogueExtendedInfo
                    {
                        Artist = "None",
                        Title = "None",
                        PicUrl = "None available",
                        ASIN = "",
                        ReleaseDate = "",
                        Price = 0,
                        Url = "http://my-cue.co.uk"
                    };
                }
            }
            return returnArray;
        }
    }
}
