using System;
using InstaSharp.Models;

namespace TagStreamDesktop
{
	public class FeedItem
	{
		public Guid ItemId { get; set; }

		public FeedItemType ItemType { get; set; }

		public Media InstagramItem { get; set; }

		public TweetItem TwitterItem { get; set; }
	}
}