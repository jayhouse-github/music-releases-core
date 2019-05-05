using System;
using System.Collections.Generic;
using Music_Releases_Core.BL;
using System.Net;
using Music_Releases_Core.Repository;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Music_Releases_Core.Console
{
    class Program
    {
        static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var AmazonAccessId = Configuration["AmazonAccessId"];
            var AmazonEndPoint = Configuration["AmazonEndPoint"];
            var AmazonAssociateTag = Configuration["AmazonAssociateTag"];
            var AmazonSecretKey = Configuration["AmazonSecretKey"];
            var ItunesAffiliateId = Configuration["ItunesAffiliateId"];
            var ItunesRequestUrl = Configuration["ItunesRequestUrl"];

            var amazonSearchRepo = new AmazonSearchRepository(AmazonAccessId, AmazonEndPoint, AmazonAssociateTag, AmazonSecretKey);
            var amazonItemRepo = new AmazonItemRepository(AmazonAccessId, AmazonEndPoint, AmazonAssociateTag, AmazonSecretKey);

            Console.WriteLine("1. Search from comma searated list.");
            Console.WriteLine("2. Search from ASIN.");
            Console.WriteLine("Please select.");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                Console.WriteLine("Type a list of bands separated by commas");
                string list = Console.ReadLine();
                var amazonSearch = new AmazonSearch(amazonSearchRepo);

                IList<ExtendedItem> returnObj = null;

                try
                {
                    returnObj = amazonSearch.SearchFromCommaSeparatedList(list);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Found " + returnObj.Count.ToString() + " items");

                foreach (var item in returnObj)
                {
                    Console.WriteLine(item.Artist + " - " + item.Title + " " + item.ReleaseDate.ToLongDateString());

                }

                Console.ReadLine();
            }
            else if (answer == "2")
            {
                var amazonItemRetrieve = new AmazonItemSearch(amazonItemRepo);
                Console.WriteLine("Please enter ASIN.");
                string asin = Console.ReadLine();
                var returnedObj = amazonItemRetrieve.GetByASIN(asin);

                Console.WriteLine(returnedObj.Artist + " - " + returnedObj.Title);
                Console.ReadLine();
            }

            string[] arg1 = { "" };

            Main(arg1);
        }
    }
}
