using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Areas.Models;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Areas.Writer.Controllers
{
    [Area("Writer")]
    [AllowAnonymous]
    public class AccountPanelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountPanelController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(user);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("SignUp", "Register");
            }
            else
            {

                var url = Url.RouteUrl("areas", new { controller = "Content", action = "Index", area = "Writer" });
                return Redirect(url);
            }
        }



        // GET: /<controller>/
        public async Task<IActionResult> ChangePassword()
        {
          //  var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> ChangePassword(EditPasswordViewModel editPasswordViewModel)
        {
            var url = "";
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (editPasswordViewModel.NewPassword == editPasswordViewModel.ConfirmNewPassword)
                {
                    var updateUser = await _userManager.ResetPasswordAsync(user, token, editPasswordViewModel.NewPassword);
                if (updateUser.Succeeded)
                {
                    url = Url.RouteUrl("areas", new { controller = "Content", action = "Index", area = "Writer" });
                    return Redirect(url);
                }
                     }
                else
                {
                    ModelState.AddModelError("", "An error occured.");
                }
             url = Url.RouteUrl("areas", new { controller = "AccountPanel", action = "ChangePassword", area = "Writer" });
            return Redirect(url);

        }

    }
}

