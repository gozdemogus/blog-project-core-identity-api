using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class Category
	{
		public Category()
		{
		}
		[Key]
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}

