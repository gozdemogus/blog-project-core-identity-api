using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.PresentationLayer.Models
{
	public class RoleViewModel
	{
		public RoleViewModel()
		{
		}

        [Required(ErrorMessage = "Lütfen rol adını giriniz.")]
        public string RoleName { get; set; }
    }
}

