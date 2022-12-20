using System;
using BaseIdentity.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogContent
{
	public class Main4Blogs:ViewComponent
	{
		public Main4Blogs()
		{
		}

        public IViewComponentResult Invoke()
        {
            var blogs = new List<Blog>();

            using (var context = new Context())
            {
                blogs = context.Blogs.Include(a => a.Category).Include(x => x.AppUser).Take(4).ToList();
            }

            return View(blogs);
        }
    }
}

