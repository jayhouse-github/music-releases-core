using System.Net;
using Microsoft.AspNetCore.Mvc;
using Music_Releases_Core.BL;
using Music_Releases_Core.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicReleasesCore.API.Controllers
{
    public class ReleaseDetailController : Controller
    {
        IAmazonItemRepository _amazonItemRepo;
        IItunesItemRepository _itunesItemRepo;

        public ReleaseDetailController(IAmazonItemRepository amazonItemRepo, IItunesItemRepository itunesItemRepo)
        {
            _amazonItemRepo = amazonItemRepo;
            _itunesItemRepo = itunesItemRepo;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Route("api/releasedetail/{asin}")]
        public IActionResult Get(string asin)
        {
            var releaseSearch = new ReleaseSearch(_amazonItemRepo, _itunesItemRepo);
            MusicReleaseCollection musicReleaseDetailModel = null;

            try
            {
                musicReleaseDetailModel = releaseSearch.GetDetails(asin);
            }
            catch (WebException ex)
            {
                return StatusCode(500, ex.Message);
            }

            if (musicReleaseDetailModel != null)
                return Ok(musicReleaseDetailModel);
            else
                return StatusCode(500);
        }
    }
}
