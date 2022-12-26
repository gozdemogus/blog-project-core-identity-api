using System;
using BaseIdentity.DataAccessLayer.Concrete;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogDetail
{
	public class Replies:ViewComponent
	{
		public Replies()
		{
		}

        public IViewComponentResult Invoke(int id)
        {
            using (var context = new Context())
            {
              List<Reply>  replies = context.Replies.Include(x=>x.AppUser).Where(x=>x.CommentId == id).OrderBy(x=>x.date).ToList();

                return View(replies);
            }
        }
    }
}

