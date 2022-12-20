using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class WriterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }



        // GET: /<controller>/
        public async Task<IActionResult> Index(int id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            
            var blog = new List<Blog>();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.AppUserId == id).ToList();
            }


            ViewBag.userInfo = user;
            return View(blog);

        }


        public async Task<IActionResult> Profile()
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


    }
}

