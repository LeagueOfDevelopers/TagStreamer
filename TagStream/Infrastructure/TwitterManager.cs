using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TagStream.Models;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.WebLogic;

namespace TagStream.Infrastructure
{
	public class TwitterManager : ISocialNetworkFeedStream
	{
		public TwitterManager(string tag)
		{
			_tag = tag;
			_credentials = TwitterCredentials.CreateCredentials(
				ConfigurationManager.AppSettings["TwitterConsumerKey"],
				ConfigurationManager.AppSettings["TwitterConsumerSecret"],
				ConfigurationManager.AppSettings["TwitterAccessKey"],
				ConfigurationManager.AppSettings["TwitterAccessSecret"]);
			_minTagId = 0;
		}

		public async Task<FeedItem> GetLastFeedItemAsync()
		{
			if (!_tweetsStore.Any())
			{
				await Task.Run(() => UpdateStore());
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

		private void UpdateStore()
		{
			var parameters = Search.CreateTweetSearchParameter(_tag);
			if (_minTagId == 0)
			{
				parameters.Since = DateTime.Now - TimeSpan.FromMinutes(200);
			}
			else
			{
				parameters.SinceId = _minTagId;
			}

			var response = TwitterCredentials.ExecuteOperationWithCredentials(_credentials,
				() => Search.SearchTweets(parameters));
			_tweetsStore = new Queue<ITweet>(response.OrderBy(tweet => tweet.CreatedAt));
			if (_tweetsStore.Any())
			{
				_minTagId = _tweetsStore.Peek().Id;
			}
		}

		private Queue<ITweet> _tweetsStore = new Queue<ITweet>();
		private readonly string _tag;
		private readonly IOAuthCredentials _credentials;
		private long _minTagId;
	}
}