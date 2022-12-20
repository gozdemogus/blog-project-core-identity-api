using System;
using Microsoft.AspNetCore.Identity;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class AppUser:IdentityUser<int>
	{
		public AppUser()
		{
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		public string? Image { get; set; }
        public string? About { get; set; }
        public string? Linkedin { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}

