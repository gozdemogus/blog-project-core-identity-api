using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class PasswordController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public PasswordController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public void SendMail(string email, string link)
        {
            string mailKey = _configuration["MailKey"];

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Bloggy", "goezdem6@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            //  bodyBuilder.TextBody = emailcode;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Password Reset";

            var body = new TextPart("plain")
            {
                Text = link
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


        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
             var link = Url.Action("ResetPassword", "Password", new { token, email = user.Email }, Request.Scheme);
           // var link = $"https://localhost:5001/Password/ResetPassword/{token}/{email}";

            SendMail(user.Email, link);

            //if (emailResponse)
            //    return RedirectToAction("ForgotPasswordConfirmation");
            //else
            //{
            //    // log email failed 
            //}
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
     //   [Route("Password/ResetPassword/{token}/{email}")]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}

