using System;
using BaseIdentity.EntityLayer.Concrete;

namespace BaseIdentity.PresentationLayer.Models
{
	public class SendCommentViewModel
	{
		public SendCommentViewModel()
		{
		}

		public string CommentText { get; set; }
		public int CurrentPageId { get; set; }
		public int UserInfo { get; set; }
	}
}

