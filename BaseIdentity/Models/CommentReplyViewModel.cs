using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.PresentationLayer.Models
{
	public class CommentReplyViewModel
	{
		public CommentReplyViewModel()
		{
		}

		public Comment comment { get; set; }
		public Reply reply { get; set; }
	}
}

