using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaSharp.Models;
using System.Net;
using TagStreamer.Models;
using System.IO;
using Newtonsoft.Json;

namespace WpfApplication11
{
    class Connection
    {
        static string site = "сайт";
        public static FeedItem LoadItem()
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(String.Format("http://{0}", site));
                var resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                FeedItem fi = JsonConvert.DeserializeObject<FeedItem>(sr.ReadToEnd());
                return fi;
            }
            catch { return null; }
        }
        public static FeedItem LoadItemAsAdmin(string key)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(String.Format("http://{1}?key={0}",key, site));
                req.Method = "GET";
                var resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                FeedItem fi = JsonConvert.DeserializeObject<FeedItem>(sr.ReadToEnd());
                return fi;
            }
            catch { return null; }
        }
        public static string Authorization(string log, string pass)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(string.Format("http://{2}?login={0}&password={1}", log, pass, site));
                req.Method = "GET";
                var resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                return sr.ReadToEnd();
            }
            catch { return null; }
        }
        public static bool SendGoodId(string key, string id)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(string.Format("http://{2}?key={0}&id={1}", key, id, site));
                var resp = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
                return true;
            }
            catch { return false; }
        }
    }
}
