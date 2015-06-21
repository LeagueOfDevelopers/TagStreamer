using System.Threading.Tasks;
using TagStreamer.Models;

namespace TagStreamer.Infrastructure
{
	public interface IRecentItemProvider
	{
		Task<FeedItem> GetRecentItemAsync();
	}
}
