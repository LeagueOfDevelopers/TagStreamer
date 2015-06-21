using System.Collections.Generic;

namespace TagStreamer.Models
{
	public class UserSession
	{
		public UserSession(string sessionId)
		{
			SessionId = sessionId;
			UserSessionQueue = new Queue<FeedItem>();
		}

		public Queue<FeedItem> UserSessionQueue { get; private set; }

		public string SessionId { get; private set; }
	}
}