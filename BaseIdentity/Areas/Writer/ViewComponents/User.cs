using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.Areas.Writer.ViewComponents
{
	public class User:ViewComponent
	{
        private readonly UserManager<AppUser> _userManager;

        public User(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }



        public async Task<IViewComponentResult> InvokeAsync()
        {


                var user = await _userManager.FindByNameAsync(User.Identity.Name);

           
            return View(user);
        }
    }
}

