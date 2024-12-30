


/// <summary>YoutubeLib</summary>
namespace YoutubeLib
{
	/// <summary>LiveChatMessage</summary>
	public class LiveChatMessage
	{
		/// <summary>Item</summary>
		public struct Item
		{
			/// <summary>snippet_displaymessage</summary>
			public string snippet_displaymessage;

			/// <summary>authordetails_displayname</summary>
			public string authordetails_displayname;

			/// <summary>authordetails_channelurl</summary>
			public string authordetails_channelurl;

			/// <summary>authordetails_profileimageurl</summary>
			public string authordetails_profileimageurl;
		}

		/// <summary>list</summary>
		public System.Collections.Generic.List<Item> list;

		/// <summary>constructor</summary>
		public LiveChatMessage()
		{
			list = new System.Collections.Generic.List<Item>();
		}
	}
}

