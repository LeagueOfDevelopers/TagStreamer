﻿using System;
using InstaSharp.Models;

namespace WpfApplication11
{
	public class FeedItem
	{
		public Guid ItemId { get; set; }

		public FeedItemType ItemType { get; set; }

		public Media InstagramItem { get; set; }

		// here is gonna be twitter item
	}
}