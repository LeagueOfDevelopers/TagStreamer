using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TagStream.Models;
using Tweetinvi;
using Tweetinvi.Core.Interfaces.WebLogic;

namespace TagStream.Infrastructure
{
	public class TwitterManager : ISocialNetworkFeedStream
	{
		public TwitterManager(string tag)
		{
			_tag = tag;
			_credentials = TwitterCredentials.CreateCredentials(
				ConfigurationManager.AppSettings["TwitterAccessKey"],
				ConfigurationManager.AppSettings["TwitterAccessSecret"],
				ConfigurationManager.AppSettings["TwitterConsumerKey"],
				ConfigurationManager.AppSettings["TwitterConsumerSecret"]);
		}

		public async Task<FeedItem> GetLastFeedItemAsync()
		{
			if (!_tweetsStore.Any())
			{
				await UpdateStore();
			}

			if (!_tweetsStore.Any())
			{
				return null;
			}

			var media = _tweetsStore.Dequeue();
			return new FeedItem(media);
		}

		public DateTimeOffset GetDataCreationItem(FeedItem feedItem)
		{
			if (feedItem == null)
			{
				throw new ArgumentNullException("feedItem");
			}

			if (feedItem.ItemType != FeedItemType.Twitter)
			{
				throw new ArgumentException("It is not twitter item");
			}

			return feedItem.TwitterItem.CreatedAt;
		}

		private async Task UpdateStore()
		{
			var parameters = Search.CreateTweetSearchParameter(_tag);
			parameters.Since = _lastUpdateTime;
			TwitterCredentials.SetCredentials(_credentials);
			var rawResponse = await SearchAsync.SearchTweets(parameters);
			_tweetsStore = new Queue<TweetItem>(
				rawResponse.OrderBy(tweet => tweet.CreatedAt)
				.Where(tweet => !tweet.IsRetweet)
				.Where(tweet => tweet.CreatedAt > _lastUpdateTime)
				.Select(tweet => new TweetItem(tweet)));
			_lastUpdateTime = DateTime.Now;
		}

		private DateTime _lastUpdateTime = DateTime.Now - TimeSpan.FromHours(1);
		private Queue<TweetItem> _tweetsStore = new Queue<TweetItem>();
		private readonly string _tag;
		private readonly IOAuthCredentials _credentials;
	}
}