using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents.Home
{
	public class Header:ViewComponent
	{
        private readonly UserManager<AppUser> _userManager;

        public Header(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

       

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                ViewBag.logged = true;
            }

            var categories = new List<Category>();

            using (var context = new Context())
            {
                categories = context.Categories.Take(8).ToList();
            }

            return View(categories);
        }
    }
}

