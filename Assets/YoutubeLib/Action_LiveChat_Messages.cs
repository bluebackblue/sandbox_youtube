


/// <summary>YoutubeLib</summary>
namespace YoutubeLib
{
	/// <summary>Action_LiveChat_Messages</summary>
	public class Action_LiveChat_Messages : Action_Base
	{
		[System.Flags]
		public enum Type
		{
			Log = 1,
		}

		/// <summary>type</summary>
		private Type type;

		/// <summary>unitywebrequest</summary>
		private UnityEngine.Networking.UnityWebRequest unitywebrequest;

		/// <summary>work</summary>
		private Work work;

		/// <summary>Result</summary>
		public struct Result
		{
			/// <summary>Item</summary>
			[System.Serializable]
			public struct Item
			{
				[System.Serializable]
				public struct Snippet
				{
					[System.Serializable]
					public struct TextMessageDetails
					{
						public string messageText;
					}

					public string type;
					public string liveChatId;
					public string authorChannelId;
					public string publishedAt;
					public bool hasDisplayContent;
					public string displayMessage;
					public TextMessageDetails textMessageDetails;
				}

				/// <summary>Item</summary>
				[System.Serializable]
				public struct AuthorDetails
				{
					public string channelId;
					public string channelUrl;
					public string displayName;
					public string profileImageUrl;
					public bool isVerified;
					public bool isChatOwner;
					public bool isChatSponsor;
					public bool isChatModerator;
				}

				public string kind;
				public string etag;
				public string id;
				public Snippet snippet;
				public AuthorDetails authorDetails;
			}


			/// <summary>PageInfo</summary>
			[System.Serializable]
			public struct PageInfo
			{
				public string totalResults;
				public string resultsPerPage;
			}

			public string kind;
			public string etag;
			public string id;
			public int pollingIntervalMillis;
			public string nextPageToken;
			public PageInfo pageInfo;
			public Item[] items;
		}

		/// <summary>constructor</summary>
		public Action_LiveChat_Messages(Work a_work,Type a_type)
		{
			//work
			work = a_work;

			//a_type
			type = a_type;
		}

		/// <summary>Request</summary>
		public void Request()
		{
			//unitywebrequest
			unitywebrequest = new UnityEngine.Networking.UnityWebRequest();
			unitywebrequest.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
			unitywebrequest.url = string.Format(
				"{0}{1}?key={2}&liveChatId={3}&part={4}",
				"https://www.googleapis.com/youtube/v3/",
				"liveChat/messages",
				work.apikey,
				work.livechatid,
				"id,snippet,authorDetails"
			);
			unitywebrequest.SendWebRequest();

			//ログ。
			if((type&Type.Log) > 0){
				UnityEngine.Debug.Log(unitywebrequest.url);
			}
		}

		/// <summary>Update</summary>
		public ActionResult Update()
		{
			try{
				switch(unitywebrequest.result){
				case UnityEngine.Networking.UnityWebRequest.Result.Success:
					{
						string result_string = unitywebrequest.downloadHandler.text;
						
						Result result = UnityEngine.JsonUtility.FromJson<Result>(result_string);

						if((type&Type.Log) > 0){
							UnityEngine.Debug.Log(result_string);

							using(System.IO.TextWriter file = new System.IO.StreamWriter("../Log_Action_LiveChat_Messages.txt")){
								file.WriteLine(result_string);
								file.Close();
								file.Dispose();
							}
							
							UnityEngine.Debug.Log("kind : " + result.kind);
							UnityEngine.Debug.Log("etag : " + result.etag);
							UnityEngine.Debug.Log("pollingIntervalMillis : " + result.pollingIntervalMillis);
							UnityEngine.Debug.Log("pageInfo.totalResults : " + result.pageInfo.totalResults);
							UnityEngine.Debug.Log("pageInfo.resultsPerPage : " + result.pageInfo.resultsPerPage);
							UnityEngine.Debug.Log("nextPageToken : " + result.nextPageToken);
							for(int ii=0;ii<result.items.Length;ii++){
								UnityEngine.Debug.Log("[" + ii.ToString() + "]kind : " + result.items[ii].kind);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]etag : " + result.items[ii].etag);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]id : " + result.items[ii].id);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.type : " + result.items[ii].snippet.type);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.liveChatId : " + result.items[ii].snippet.liveChatId);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.authorChannelId : " + result.items[ii].snippet.authorChannelId);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.publishedAt : " + result.items[ii].snippet.publishedAt);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.hasDisplayContent : " + result.items[ii].snippet.hasDisplayContent);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.displayMessage : " + result.items[ii].snippet.displayMessage);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]snippet.textMessageDetails.messageText : " + result.items[ii].snippet.textMessageDetails.messageText);

								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.channelId : " + result.items[ii].authorDetails.channelId);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.channelUrl : " + result.items[ii].authorDetails.channelUrl);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.displayName : " + result.items[ii].authorDetails.displayName);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.profileImageUrl : " + result.items[ii].authorDetails.profileImageUrl);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.isVerified : " + result.items[ii].authorDetails.isVerified);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.isChatOwner : " + result.items[ii].authorDetails.isChatOwner);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.isChatSponsor : " + result.items[ii].authorDetails.isChatSponsor);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]authorDetails.isChatModerator : " + result.items[ii].authorDetails.isChatModerator);
							}
						}

						/*
						if(result.items.Length > 0){
							work.activelivechatid = result.items[0].liveStreamingDetails.activeLiveChatId;
						}
						*/

						unitywebrequest.Dispose();
					}return ActionResult.Success;
				case UnityEngine.Networking.UnityWebRequest.Result.InProgress:
					{
					}return ActionResult.Busy;
				default:
					{
						UnityEngine.Debug.LogError(unitywebrequest.error);
						unitywebrequest.Dispose();
					}return ActionResult.Error;
				}
			}catch(System.Exception exception){

				UnityEngine.Debug.LogError(exception.ToString());
				unitywebrequest.Dispose();
				return ActionResult.Error;
			}
		}
	}
}

