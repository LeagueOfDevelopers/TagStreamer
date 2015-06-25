using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TagStream.Infrastructure;

namespace TagStream.Models
{
	//todo: too much functionality, refactor
	public class AdminService
	{
		public AdminService(IRecentItemProvider recentItemProvider, UserFeedItemService userFeedItemService)
		{
			_recentItemProvider = recentItemProvider;
			_userFeedItemService = userFeedItemService;
		}

		public string RegisterNewAdmin(string login, string password)
		{
			if (!VerifyCredentials(login, password))
			{
				return null;
			}

			var id = GenerateNewSessionId();
			_adminConnections.Add(id);
			return id;
		}

		public async Task<FeedItem> GetLastItemAsync(string token)
		{
			if (!CheckSessionSetUp(token))
			{
				return null;
			}

			var feedItem = await _recentItemProvider.GetRecentItemAsync();
			_itemsBuffer.TryAdd(feedItem.ItemId, feedItem);
			return feedItem;
		}

		public void ProcessItem(string token, Guid itemId, bool accepted)
		{
			if (!CheckSessionSetUp(token))
			{
				return;
			}

			FeedItem processedItem;
			var checkOutCompleted = _itemsBuffer.TryRemove(itemId, out processedItem);
			if (accepted && checkOutCompleted)
			{
				_userFeedItemService.SendNewItem(processedItem);
			}
		}

		public void DisconnectAdmin(string token)
		{
			_adminConnections.TryTake(out token);
		}

		private static bool VerifyCredentials(string login, string password)
		{
			string passwordHash;
			using (var md5 = MD5.Create())
			{
				var passwordBytes = Encoding.UTF8.GetBytes(password);
				passwordHash = Convert.ToBase64String(md5.ComputeHash(passwordBytes));
			}
			return login == ConfigurationManager.AppSettings["login"] &&
			       passwordHash == ConfigurationManager.AppSettings["password"];
		}

		private bool CheckSessionSetUp(string token)
		{
			return _adminConnections.Contains(token);
		}

		//todo: extract in it's own helper
		private static string GenerateNewSessionId()
		{
			var guid = Guid.NewGuid();
			return BitConverter.ToString(guid.ToByteArray());
		}

		private readonly ConcurrentBag<string> _adminConnections = new ConcurrentBag<string>();  
		private readonly ConcurrentDictionary<Guid, FeedItem> _itemsBuffer = new ConcurrentDictionary<Guid, FeedItem>();
		private readonly IRecentItemProvider _recentItemProvider;
		private readonly UserFeedItemService _userFeedItemService;
	}
}