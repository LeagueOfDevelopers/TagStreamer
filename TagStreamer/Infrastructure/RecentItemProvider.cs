using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TagStreamer.Models;

namespace TagStreamer.Infrastructure
{
	public class RecentItemProvider : IRecentItemProvider
	{
		public Task<FeedItem> GetRecentItemAsync()
		{
			throw new NotImplementedException();
		}
	}
}