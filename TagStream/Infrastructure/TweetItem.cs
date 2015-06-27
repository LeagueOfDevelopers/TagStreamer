using System;
using Tweetinvi.Core.Interfaces;

namespace TagStream.Infrastructure
{
	public class TweetItem
	{
		public TweetItem()
		{
		}
		
		public TweetItem(ITweet tweet)
		{
			CreatedAt = tweet.CreatedAt;
			AuthorNick = tweet.CreatedBy.ScreenName;
			AuthorName = tweet.CreatedBy.Name;
			AuthorAvatarUrl = tweet.CreatedBy.ProfileImageFullSizeUrl;
			FavouriteCount = tweet.FavouriteCount;
			RetweetCount = tweet.RetweetCount;
			Text = tweet.Text;
		}

		public DateTime CreatedAt { get; set; }

		public int FavouriteCount { get; set; }

		public int RetweetCount { get; set; }

		public string Text { get; set; }

		public string AuthorNick { get; set; }

		public string AuthorName { get; set; }

		public string AuthorAvatarUrl { get; set; }
	}
}