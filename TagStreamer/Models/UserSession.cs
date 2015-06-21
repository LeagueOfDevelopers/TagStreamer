using System;
using System.Collections.Concurrent;

namespace TagStreamer.Models
{
	public class UserSession
	{
		public UserSession(string sessionId)
		{
			SessionId = sessionId;
			UserSessionQueue = new ConcurrentQueue<FeedItem>();
			CreationTime = DateTime.Now;
		}

		public ConcurrentQueue<FeedItem> UserSessionQueue { get; private set; }

		public string SessionId { get; private set; }

		public DateTimeOffset CreationTime { get; private set; }
	}
}