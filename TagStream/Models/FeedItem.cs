using System;
using InstaSharp.Models;
using TagStream.Infrastructure;
using Tweetinvi.Core.Interfaces.DTO;

namespace TagStream.Models
{
	public class FeedItem
	{
		public FeedItem(Media instagramMedia)
		{
			InstagramItem = instagramMedia;
			ItemType = FeedItemType.Instagram;
			ItemId = Guid.NewGuid();
		}

		public FeedItem(TweetItem tweet)
		{
			TwitterItem = tweet;
			ItemType = FeedItemType.Twitter;
			ItemId = Guid.NewGuid();
		}

		public Guid ItemId { get; private set; }

		public FeedItemType ItemType { get; private set; }

		public Media InstagramItem { get; private set; }

		public TweetItem TwitterItem { get; private set; }
	}
}