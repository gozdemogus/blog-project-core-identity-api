using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class Blog
	{
		public Blog()
		{
		}
		[Key]
		public int Id { get; set; }
		public string Header { get; set; }
		public string? Content { get; set; }
        public string? Content2 { get; set; }
        public string? Content3 { get; set; }

        public DateTime? Date { get; set; }
		public string? Image { get; set; }
		public string? Image2 { get; set; }
		public string? Image3 { get; set; }
		public string? MinsToRead { get; set; }
		public int? Rate { get; set; }
		public string? Entry { get; set; }


		public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}

