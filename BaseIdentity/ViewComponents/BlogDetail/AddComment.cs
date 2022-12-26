using System;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogDetail
{
	public class AddComment:ViewComponent
	{
		public AddComment()
		{
		}

        public IViewComponentResult Invoke()
        {
           

            return View();
        }

    }
}

