using System;
using System.ComponentModel.DataAnnotations;

namespace BaseIdentity.EntityLayer.Concrete
{
	public class Reply
	{
		public Reply()
		{
		}
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime date { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

