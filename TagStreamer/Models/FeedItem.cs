using System;
using InstaSharp.Models;

namespace TagStreamer.Models
{
	public class FeedItem
	{
		public FeedItem(Media instagramMedia)
		{
			InstagramItem = instagramMedia;
			ItemId = Guid.NewGuid();
		}

		public Guid ItemId { get; private set; }

		public FeedItemType ItemType { get; private set; }

		public Media InstagramItem { get; private set; }

		// here is gonna be twitter item
	}
}