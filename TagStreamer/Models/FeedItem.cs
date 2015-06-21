using InstaSharp.Models;

namespace TagStreamer.Models
{
	public class FeedItem
	{
		public FeedItem(Media instagramMedia)
		{
			InstagramItem = instagramMedia;
		}

		public FeedItemType ItemType { get; set; }

		public Media InstagramItem { get; private set; }

		// here is gonna be twitter item
	}
}