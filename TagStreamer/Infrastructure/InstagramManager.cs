using System.Collections.Concurrent;
using System.Configuration;
using System.Threading.Tasks;
using InstaSharp;
using InstaSharp.Endpoints;
using InstaSharp.Models.Responses;

namespace TagStreamer.Infrastructure
{
	public class InstagramManager
	{
		public InstagramManager()
		{
			Queue = new ConcurrentQueue<Media>();
		}

		public async Task SetupConnection(string tag)
		{
			Config = new InstagramConfig(
				ConfigurationManager.AppSettings["InstagramId"],
				ConfigurationManager.AppSettings["InstagramSecret"],
				ConfigurationManager.AppSettings["host"],
				ConfigurationManager.AppSettings["host"] + "/Instagram");
				
			var subscription = new Subscription(Config);
			_response = await subscription.CreateTag("tag");
			Tag = tag;
		}

		public ConcurrentQueue<Media> Queue { get; private set; }

		public InstagramConfig Config { get; private set; }

		public string Tag { get; set; }

		private SubscriptionResponse _response;
	}
}