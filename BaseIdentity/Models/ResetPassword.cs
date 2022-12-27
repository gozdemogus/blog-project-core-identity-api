using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.PresentationLayer.Models
{
	public class ResetPassword
	{
		public ResetPassword()
		{
		}

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}

