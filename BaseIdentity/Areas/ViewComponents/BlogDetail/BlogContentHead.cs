using System;
using System.Linq;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogDetail
{
	public class BlogContentHead:ViewComponent
	{
		public BlogContentHead()
		{
		}


        public IViewComponentResult Invoke(int id)
        {
            var blog = new Blog();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x=>x.Id==id).FirstOrDefault();
            }

            return View(blog);
        }


    }
}

