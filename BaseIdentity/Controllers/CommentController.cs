using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public CommentController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            var blog = new List<Blog>();

            using (var context = new Context())
            {
                var comments = context.Comments.Include(x => x.AppUser).Include(x=>x.Replies).Where(x=>x.BlogId == id).ToList();

                return View(comments);
            }
        }

        public async void AddComment(SendCommentViewModel sendCommentViewModel)
        {
          //  var user = await _userManager.FindByNameAsync(User.Identity.Name);
          //  var userId = user.Id;
            using (var context = new Context())
            {
          
                Comment comment = new Comment();
                comment.AppUserId = sendCommentViewModel.UserInfo;
                comment.Content = sendCommentViewModel.CommentText;
                comment.Date = DateTime.Now;
                comment.BlogId = sendCommentViewModel.CurrentPageId;
                context.Comments.Add(comment);
                context.SaveChanges();
            }
            return;
        }

    }
}

