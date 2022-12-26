using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogContent
{
	public class PopularBlogs:ViewComponent
	{
		public PopularBlogs()
		{
		}

        public IViewComponentResult Invoke()
        {
            var blogs = new List<Blog>();

            using (var context = new Context())
            {
                blogs = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Where(x => x.Rate == 5).Take(4).ToList();
            }

            return View(blogs);
        }
    }
}

