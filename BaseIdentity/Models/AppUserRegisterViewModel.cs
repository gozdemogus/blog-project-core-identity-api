using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.PresentationLayer.Models
{
	public class AppUserRegisterViewModel
	{
		public AppUserRegisterViewModel()
		{
		}
		[Required(ErrorMessage="Kullanıcı adı boş geçilemez!")]
		public string Username { get; set; }

        [Required(ErrorMessage = "Ad boş geçilemez!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadboş geçilemez!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Mail boş geçilemez!")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar boş geçilemez!")]
        public string ConfirmPassword { get; set; }
    }
}

