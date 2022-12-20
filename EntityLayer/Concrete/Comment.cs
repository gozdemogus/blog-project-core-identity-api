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


        public virtual Blog Blog { get; set; }

    }
}

