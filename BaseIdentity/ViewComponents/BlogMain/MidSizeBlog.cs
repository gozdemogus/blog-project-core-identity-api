using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogContent
{
	public class MidSizeBlog:ViewComponent
	{
		public MidSizeBlog()
		{
		}

        public IViewComponentResult Invoke()
        {
            var blog = new Blog();

            using (var context = new Context())
            {
                blog = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).OrderByDescending(x => x.Date).Take(1).FirstOrDefault();
            }

            return View(blog);
        }
    }
}

