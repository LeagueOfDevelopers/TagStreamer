using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using InstaSharp;
using InstaSharp.Endpoints;
using TagStream.Models;
using Media = InstaSharp.Models.Media;

namespace TagStream.Infrastructure
{
	public class InstagramManager : ISocialNetworkFeedStream
	{
		public InstagramManager(string tag)
		{
			_config = new InstagramConfig(
				ConfigurationManager.AppSettings["InstagramId"],
				ConfigurationManager.AppSettings["InstagramSecret"],
				ConfigurationManager.AppSettings["host"]);
			_tag = tag;
			_lastUpdateTime = DateTime.MinValue;
		}

		public async Task<FeedItem> GetLastFeedItemAsync()
		{
			if (!_mediaStore.Any())
			{
				await UpdateStoredItems();
			}
			if (!_mediaStore.Any())
			{
				return null;
			}

			var media = _mediaStore.Dequeue();
			return new FeedItem(media);
		}

		public DateTimeOffset GetDataCreationItem(FeedItem feedItem)
		{
			if (feedItem == null)
			{
				throw new ArgumentNullException("feedItem");
			}

			if (feedItem.InstagramItem == null)
			{
				throw new ArgumentException("Instagram item can not be null");
			}

			return feedItem.InstagramItem.CreatedTime;
		}

		private async Task UpdateStoredItems()
		{
			var tag = new Tags(_config);
			var response = await tag.Recent(_tag);
			var sortedMedias = response.Data.OrderBy(media => media.CreatedTime).Where(media => media.CreatedTime > _lastUpdateTime);
			_mediaStore = new Queue<Media>(sortedMedias);
			_lastUpdateTime = DateTime.Now;
		}

		private Queue<Media> _mediaStore = new Queue<Media>();
		private DateTime _lastUpdateTime;
		private readonly InstagramConfig _config;
		private readonly string _tag;
	}
}