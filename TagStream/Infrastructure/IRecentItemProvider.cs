using System.Threading.Tasks;
using TagStream.Models;

namespace TagStream.Infrastructure
{
	public interface IRecentItemProvider
	{
		Task<FeedItem> GetRecentItemAsync();
	}
}
