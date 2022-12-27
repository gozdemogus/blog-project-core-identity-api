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
using MimeKit;
using Newtonsoft.Json;
using System.Net.Mail;

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


        public void SendMail(SendMailViewModel sendMailViewModel)
        {
            string mailKey = _configuration["MailKey"];

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Bloggy", "goezdem6@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", sendMailViewModel.Receiver);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
        //    bodyBuilder.TextBody = emailcode;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Mail Subscription";

            var body = new TextPart("plain")
            {
                Text = "Your membership subscription has been received. Thank you."
            };

            mimeMessage.Body = body;

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate("goezdem6@gmail.com", mailKey); //kod
                smtp.Send(mimeMessage);
                smtp.Disconnect(true);
            }

           
        }


    }
}

