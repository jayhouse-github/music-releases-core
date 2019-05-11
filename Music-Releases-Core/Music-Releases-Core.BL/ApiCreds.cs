using System;


namespace Music_Releases_Core.BL
{
    public class ApiCreds
    {
        string _amazonAccessId;
        string _amazonEndPoint;
        string _amazonAssociateTag;
        string _amazonSecretKey;
        string _itunesAffiliateId;
        string _itunesRequestUrl;

        public string AmazonAccessId { get => _amazonAccessId; set => _amazonAccessId = value; }
        public string AmazonEndPoint { get => _amazonEndPoint; set => _amazonEndPoint = value; }
        public string AmazonAssociateTag { get => _amazonAssociateTag; set => _amazonAssociateTag = value; }
        public string AmazonSecretKey { get => _amazonSecretKey; set => _amazonSecretKey = value; }
        public string ItunesAffiliateId { get => _itunesAffiliateId; set => _itunesAffiliateId = value; }
        public string ItunesRequestUrl { get => _itunesRequestUrl; set => _itunesRequestUrl = value; }
    }
}
