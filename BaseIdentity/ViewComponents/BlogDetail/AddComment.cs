using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogDetail
{
	public class AddComment:ViewComponent
	{

        private readonly UserManager<AppUser> _userManager;

        public AddComment(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {


            if (User.Identity.Name != null)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {
                    ViewBag.logged = user.Id;
                }

            }
            SendCommentViewModel sendCommentViewModel = new SendCommentViewModel();
            return View(sendCommentViewModel);
        }
    }
}

