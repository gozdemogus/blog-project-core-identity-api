using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Areas.Writer.Controllers
{
    [Area("Writer")]
    [AllowAnonymous]
    public class ProfilePanelController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public ProfilePanelController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppUser appUser)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

           
                user.About = appUser.About;
                user.Image = appUser.Image;
                user.Linkedin = appUser.Linkedin;
                user.Twitter = appUser.Twitter;
                user.Name = appUser.Name;
                user.Surname = appUser.Surname;
            await _userManager.UpdateAsync(user);
   

            var url = Url.RouteUrl("areas", new { controller = "Content", action = "Index", area = "Writer" });
            return Redirect(url);
        }
    }
}

