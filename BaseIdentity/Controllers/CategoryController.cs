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
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        public IActionResult List(int id)
        {
            var blog = new List<Blog>();

            using (var context = new Context())
            {
                var blogs = context.Blogs.Include(x => x.Category).Include(x=>x.AppUser).Where(x => x.CategoryId == id).ToList();
               
                return View(blogs);
            }
        }
    }
}

