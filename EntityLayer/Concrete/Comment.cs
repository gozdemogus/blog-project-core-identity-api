using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class Comment
	{
		public Comment()
		{
		}

		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; }


        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<Reply> Replies { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

