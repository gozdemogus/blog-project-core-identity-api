using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BaseIdentity.DataAccessLayer.Concrete;
using BaseIdentity.EntityLayer.Concrete;
using BaseIdentity.PresentationLayer.Controllers;
using BaseIdentity.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaseIdentity.PresentationLayer.ViewComponents.BlogMain
{
	public class PopularMusic:ViewComponent
	{
	
        private readonly BlogController _blogController;

        public PopularMusic(BlogController blogController)
        {
            _blogController = blogController;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SongViewModel> songs = await _blogController.PopularSongsAsync();
            return View(songs);
        }

    }
}

