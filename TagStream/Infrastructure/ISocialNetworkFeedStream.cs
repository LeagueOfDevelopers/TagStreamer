using System;
using System.Threading.Tasks;
using TagStream.Models;

namespace TagStream.Infrastructure
{
	public interface ISocialNetworkFeedStream
	{
		Task<FeedItem> GetLastFeedItemAsync();

		DateTimeOffset GetDataCreationItem(FeedItem feedItem);
	}
}
