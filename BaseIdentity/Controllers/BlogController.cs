using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class BlogController : Controller
    {
        private readonly IConfiguration _configuration;

        public BlogController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Content(int id)
        {
            var blog = new Blog();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.Id == id).FirstOrDefault();
            }

            return View(blog);
        }

        public async Task<List<SongViewModel>> PopularSongsAsync()
        {
            List<SongViewModel> songs = new List<SongViewModel>();

            using (HttpClient client = new HttpClient())
            {
                string apiKey = _configuration["MyApiKey"];
                string url = $@"https://api.musixmatch.com/ws/1.1/chart.tracks.get?chart_name=top&page=1&page_size=5&country=TR&f_has_lyrics=1&apikey={apiKey}";
                HttpResponseMessage response = await client.GetAsync(url);
                string jsonRes = await response.Content.ReadAsStringAsync();

                var jsonx = Json(jsonRes);

                dynamic jsonResult = JsonConvert.DeserializeObject(jsonRes);
                foreach (var item in jsonResult.message.body.track_list)
                {
                    SongViewModel songViewModel = new SongViewModel();
                    songViewModel.artistName = item.track.artist_name;
                    songViewModel.trackName = item.track.track_name;

                    songs.Add(songViewModel);

                }
            }

            return songs;
        }


      
    }
}

