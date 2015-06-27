using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagStream.Models;

namespace TagStream.Infrastructure
{
	public class RecentItemProvider : IRecentItemProvider
	{
		public RecentItemProvider(InstagramManager instagramManager, TwitterManager twitterManager)
		{
			_socialNetworkFeedStreams = new ISocialNetworkFeedStream[] {instagramManager, twitterManager};
		}

		public async Task<FeedItem> GetRecentItemAsync()
		{
			if (_feedItemStore.Any())
			{
				return _feedItemStore.Dequeue();
			}

			var news = await Task.WhenAll(_socialNetworkFeedStreams.Select(async stream => new
			{
				stream,
				lastItem = await stream.GetLastFeedItemAsync()
			}));
			var updates = news.Where(item => item.lastItem != null).Select(item => item);
			if (!updates.Any())
			{
				return null;
			}

			_feedItemStore = new Queue<FeedItem>(updates.OrderBy(item => item.stream.GetDataCreationItem(item.lastItem)).Select(item => item.lastItem));
			return _feedItemStore.Dequeue();
		}

		private Queue<FeedItem> _feedItemStore = new Queue<FeedItem>();
		private readonly ISocialNetworkFeedStream[] _socialNetworkFeedStreams;
	}
}