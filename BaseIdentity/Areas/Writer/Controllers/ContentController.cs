using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Areas.Admin.Controllers
{
    [Area("Writer")]
    [AllowAnonymous]
    public class ContentController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public ContentController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var blog = new List<Blog>();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.AppUserId == user.Id).ToList();
            }


            ViewBag.userInfo = user;
            return View(blog);
           
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var blog = new Blog();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.Id == id).FirstOrDefault();
                context.Remove(blog);
                context.SaveChanges();
            }

            var url = Url.RouteUrl("areas", new { controller = "Content", action = "Index", area = "Writer" });
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blog = new Blog();

               using (var context = new Context())
               {
                   blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.Id == id).FirstOrDefault();
               }

            return View(blog);
        }

        [HttpPost]
        public IActionResult Edit(Blog blogtoEdit)
        {

            var blog = new Blog();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.Id == blogtoEdit.Id).FirstOrDefault();


                blog.Image = blogtoEdit.Image;
                blog.Image2 = blogtoEdit.Image;
                blog.Image3 = blogtoEdit.Image3;
                blog.Header = blogtoEdit.Header;
                blog.MinsToRead = blogtoEdit.MinsToRead;
                blog.Entry = blogtoEdit.Entry;
                blog.Content = blogtoEdit.Content;
                blog.Content2 = blogtoEdit.Content2;
                blog.Content3 = blogtoEdit.Content3;
                blog.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.CategoryId = blogtoEdit.CategoryId;
                context.SaveChanges();
            }
            var url = Url.RouteUrl("areas", new { controller = "Content", action = "Index", area = "Writer" });
            return Redirect(url);
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Blog blogtoEdit)
        {

            var blog = new Blog();

            using (var context = new Context())
            {
               
                blog.Image = blogtoEdit.Image;
                blog.Image2 = blogtoEdit.Image;
                blog.Image3 = blogtoEdit.Image3;
                blog.Header = blogtoEdit.Header;
                blog.MinsToRead = blogtoEdit.MinsToRead;
                blog.Entry = blogtoEdit.Entry;
                blog.Content = blogtoEdit.Content;
                blog.Content2 = blogtoEdit.Content2;
                blog.Content3 = blogtoEdit.Content3;
                blog.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.CategoryId = blogtoEdit.CategoryId;
                blog.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
                context.SaveChanges();
            }
            var url = Url.RouteUrl("areas", new { controller = "Content", action = "Index", area = "Writer" });
            return Redirect(url);
        }
    }
}

