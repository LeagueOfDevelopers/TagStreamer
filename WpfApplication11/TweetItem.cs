using System;

namespace WpfApplication11
{
	public class TweetItem
	{
		public DateTime CreatedAt { get; set; }

		public int FavouriteCount { get; set; }

		public int RetweetCount { get; set; }

		public string Text { get; set; }

		public string AuthorNick { get; set; }

		public string AuthorName { get; set; }

		public string AuthorAvatarUrl { get; set; }

		public string[] Medias { get; set; }
	}
}