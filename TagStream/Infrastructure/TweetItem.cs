using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Models.Entities;

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
			Medias = tweet.Media.Where(media => media.MediaType == "photo").Select(media => media.MediaURL).ToArray();
		}

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