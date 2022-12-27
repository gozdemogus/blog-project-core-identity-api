using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.PresentationLayer.Models
{
	public class ForgotPasswordModel
	{
		public ForgotPasswordModel()
		{
		}

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

