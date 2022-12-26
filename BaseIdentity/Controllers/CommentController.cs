using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
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
    }
}

