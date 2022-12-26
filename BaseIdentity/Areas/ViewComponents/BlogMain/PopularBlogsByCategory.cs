using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogMain
{
	public class PopularBlogsByCategory:ViewComponent
	{
		public PopularBlogsByCategory()
		{
		}


        public IViewComponentResult Invoke(int id)
        {
            var blogs = new List<Blog>();

            using (var context = new Context())
            {
                blogs = context.Blogs.Include(a => a.Category).Include(x => x.AppUser)
                    .Where(x=>x.CategoryId ==id)
                    .Where(x => x.Rate == 5)
                    .Take(4)
                    .ToList();
            }

            return View(blogs);
        }
    }
}

