using TagStreamer.Models;

namespace TagStreamer.Infrastructure
{
	public interface IRecentItemProvider
	{
		FeedItem GetRecentItem();
	}
}
