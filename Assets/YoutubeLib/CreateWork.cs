


/// <summary>YoutubeLib</summary>
namespace YoutubeLib
{
	/// <summary>CreateWork</summary>
	public static class CreateWork
	{
		/// <summary>Do</summary>
		public static Work Do()
		{
			return new Work(){
				actionlist = new System.Collections.Generic.LinkedList<Action_Base>(),
				apikey = "",
				videoid = "",
				current = null,
				livechatid = "",
				livechatmessage = new LiveChatMessage(),
				livechatpagetoken = "",
			};
		}
	}
}

