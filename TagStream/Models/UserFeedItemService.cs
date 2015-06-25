using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TagStream.Models
{
	public class UserFeedItemService
	{
		public string CreateNewSession()
		{
			var session = new UserSession(GenerateNewSessionId());
			_userSessionsStore.AddOrUpdate(session.SessionId, session, (s, userSession) => userSession);
			return session.SessionId;
		}

		public void SendNewItem(FeedItem item)
		{
			foreach (var userSession in _userSessionsStore.Values)
			{
				userSession.UserSessionQueue.Enqueue(item);
			}
		}

		public FeedItem GetNewItem(string sessionId)
		{
			FeedItem newItem;
			try
			{
				_userSessionsStore[sessionId].UserSessionQueue.TryDequeue(out newItem);
			}
			catch (KeyNotFoundException)
			{
				return null;
			}

			return newItem;
		}

		public void CloseSession(string sessionId)
		{
			UserSession deletedSession;
			_userSessionsStore.TryRemove(sessionId, out deletedSession);
		}

		//todo: extract in it's own helper
		private static string GenerateNewSessionId()
		{
			var guid = Guid.NewGuid();
			return BitConverter.ToString(guid.ToByteArray());
		}

		private readonly ConcurrentDictionary<string, UserSession> _userSessionsStore = new ConcurrentDictionary<string, UserSession>();
 
	}
}