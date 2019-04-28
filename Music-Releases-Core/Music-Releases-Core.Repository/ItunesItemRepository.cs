using System;
using System.Net;
using System.Web;
using Music_Releases_Core.Repository.Helpers;
using Music_Releases_Core.Repository.Interfaces;
using Newtonsoft.Json;

namespace Music_Releases_Core.Repository
{
    public class ItunesItemRepository : IItunesItemRepository
    {
        string _affiliateId;
        string _requestUrl;

        public ItunesItemRepository(string affiliateId, string requestUrl)
        {
            _affiliateId = affiliateId;
            _requestUrl = requestUrl;
        }

        public ICatalogueInfo GetInfo(string searchTerm)
        {
            var keywords = HttpUtility.UrlEncode(searchTerm);
            string returnedData;
            ICatalogueInfo returnItem = new CatalogueInfo();

            var requestURL = String.Format(_requestUrl + "?term={0}&country=GB&entity=album", keywords);

            using (var client = new WebClient())
            {
                returnedData = client.DownloadString(requestURL);
            }

            var dataObj = JsonConvert.DeserializeObject<ItunesItemCollection>(returnedData);

            if (dataObj.ResultCount > 0)
                returnItem = ItunesHelper.GetNewCatalogueObjectFromItunesObject(dataObj.Results.First());
            else
                returnItem = null;

            return returnItem;
        }
    }
}
