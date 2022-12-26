using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogDetail
{
	public class Comment:ViewComponent
	{

        public IViewComponentResult Invoke(int id)
        {
            var comments = new List<Comment>();

            using (var context = new Context())
            {
                var blogs = context.Comments.Include(x => x.AppUser).Include(x => x.Replies).Where(x => x.BlogId == id).ToList();

                return View(blogs);
            }
        }
    }
}

