using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TagStreamDesktop
{
	internal class Connection
	{
		private static string site = "tagstream.azurewebsites.net/api";

		public static string ConnectUser()
		{
			try
			{
				var req = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/User/Connect", site));
				req.Method = "GET";
				var resp = (HttpWebResponse)req.GetResponse();
				var sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
				return sr.ReadToEnd().Trim('\"');
			}
			catch
			{
				return null;
			}
		}

		public static FeedItem LoadItemAsUser(string connectionKey)
		{
			try
			{
				var req = (HttpWebRequest)WebRequest.Create(String.Format("http://{1}/User/NewPhoto?connectionToken={0}", connectionKey, site));
				req.Method = "GET";
				var resp = (HttpWebResponse) req.GetResponse();
				var sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
				var fi = JsonConvert.DeserializeObject<FeedItem>(sr.ReadToEnd());
				return fi;
			}
			catch
			{
				return null;
			}
		}

		public static FeedItem LoadItemAsAdmin(string key)
		{
			try
			{
				var req = (HttpWebRequest) WebRequest.Create(String.Format("http://{1}/Admin/NewPhoto?connectionToken={0}", key, site));
				req.Method = "GET";
				var resp = (HttpWebResponse) req.GetResponse();
				var sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
				var fi = JsonConvert.DeserializeObject<FeedItem>(sr.ReadToEnd());
				return fi;
			}
			catch
			{
				return null;
			}
		}

		public static string Authorization(string log, string pass)
		{
			try
			{
				var req = (HttpWebRequest) WebRequest.Create(string.Format("http://{2}/Admin/Connect?login={0}&password={1}", log, pass, site));
				req.Method = "GET";
				var resp = (HttpWebResponse) req.GetResponse();
				var sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
				return sr.ReadToEnd().Trim('\"');
			}
			catch
			{
				return null;
			}
		}

		public static bool ProcessedPhoto(string key, string id, bool accepted)
		{
			try
			{
				var req = (HttpWebRequest) WebRequest.Create(string.Format("http://{3}/Admin/PhotoProcessed?connectionToken={0}&itemGuid={1}&accepted={2}", key, id, accepted, site));
				var resp = (HttpWebResponse) req.GetResponse();
				var sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}