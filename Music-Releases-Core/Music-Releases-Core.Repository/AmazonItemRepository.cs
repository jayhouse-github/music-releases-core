using Music_Releases_Core.Repository.Helpers;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Net;
using Music_Releases_Core.Repository.Interfaces;

namespace Music_Releases_Core.Repository
{
    public class AmazonItemRepository : IAmazonItemRepository
    {
        private string _accessID;
        private string _requestEndpoint;
        private string _associateTag;
        private string _secretKey;

        public AmazonItemRepository(string accessId, string requestEndPoint, string associateTag, string secretKey)
        {
            _accessID = accessId;
            _requestEndpoint = requestEndPoint;
            _associateTag = associateTag;
            _secretKey = secretKey;
        }

        public ICatalogueExtendedInfo LookupASIN(string ASINNumber)
        {
            var itemNumber = HttpUtility.UrlEncode(ASINNumber);
            IDictionary<string, string> searchParams = AmazonHelper.GetBaseSearchParams(Enums.SearchType.ASIN);
            ICatalogueExtendedInfo returnItem = new CatalogueExtendedInfo();

            SignedRequestHelper urlHelper = new SignedRequestHelper(this._accessID, this._secretKey, this._requestEndpoint, this._associateTag);

            searchParams["ItemId"] = ASINNumber;

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

            var items = xmlDoc["ItemLookupResponse"]["Items"];

            foreach (XmlNode item in items)
            {
                if (item.Name == "Item")
                {
                    returnItem = AmazonHelper.GetNewCatalogueObjectFromXmlNode(item);
                    break;
                }
            }

            return returnItem;
        }

        public ICatalogueExtendedInfo LookupSearchString(string searchString, SearchIndex searchIndex)
        {
            string searchIndexString;
            var searchStringEncoded = HttpUtility.UrlEncode(searchString);
            IDictionary<string, string> searchParams = AmazonHelper.GetBaseSearchParams(Enums.SearchType.SearchTerm);
            ICatalogueExtendedInfo returnItem = new CatalogueExtendedInfo();

            switch (searchIndex)
            {
                case SearchIndex.MP3:
                    searchIndexString = "MP3Downloads";
                    break;
                case SearchIndex.CD:
                    searchIndexString = "Music";
                    break;
                default:
                    searchIndexString = "All";
                    break;
            }

            SignedRequestHelper urlHelper = new SignedRequestHelper(this._accessID, this._secretKey, this._requestEndpoint, this._associateTag);
            searchParams["Keywords"] = searchStringEncoded;
            searchParams["SearchIndex"] = searchIndexString;

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
                    returnItem = AmazonHelper.GetNewCatalogueObjectFromXmlNode(item);
                    break;
                }
            }
            return returnItem;
        }
    }
}
