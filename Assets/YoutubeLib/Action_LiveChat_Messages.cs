


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
			SetLiveChatMessage,
			AddLiveChatMessage,
			SetLiveChatMessagePageToken,
			UseLiveChatMessagePageToken,
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
				/// <summary>Snippet</summary>
				[System.Serializable]
				public struct Snippet
				{
					/// <summary>TextMessageDetails</summary>
					[System.Serializable]
					public struct TextMessageDetails
					{
						/// <summary>messageText</summary>
						public string messageText;
					}

					/// <summary>type</summary>
					public string type;

					/// <summary>liveChatId</summary>
					public string liveChatId;

					/// <summary>authorChannelId</summary>
					public string authorChannelId;

					/// <summary>publishedAt</summary>
					public string publishedAt;

					/// <summary>hasDisplayContent</summary>
					public bool hasDisplayContent;

					/// <summary>displayMessage</summary>
					public string displayMessage;

					/// <summary>textMessageDetails</summary>
					public TextMessageDetails textMessageDetails;
				}

				/// <summary>Item</summary>
				[System.Serializable]
				public struct AuthorDetails
				{
					/// <summary>channelId</summary>
					public string channelId;

					/// <summary>channelUrl</summary>
					public string channelUrl;

					/// <summary>displayName</summary>
					public string displayName;

					/// <summary>profileImageUrl</summary>
					public string profileImageUrl;

					/// <summary>isVerified</summary>
					public bool isVerified;

					/// <summary>isChatOwner</summary>
					public bool isChatOwner;

					/// <summary>isChatSponsor</summary>
					public bool isChatSponsor;

					/// <summary>isChatModerator</summary>
					public bool isChatModerator;
				}

				/// <summary>kind</summary>
				public string kind;

				/// <summary>etag</summary>
				public string etag;

				/// <summary>id</summary>
				public string id;

				/// <summary>snippet</summary>
				public Snippet snippet;

				/// <summary>authorDetails</summary>
				public AuthorDetails authorDetails;
			}

			/// <summary>PageInfo</summary>
			[System.Serializable]
			public struct PageInfo
			{
				/// <summary>totalResults</summary>
				public string totalResults;

				/// <summary>resultsPerPage</summary>
				public string resultsPerPage;
			}

			/// <summary>kind</summary>
			public string kind;

			/// <summary>etag</summary>
			public string etag;

			/// <summary>id</summary>
			public string id;

			/// <summary>pollingIntervalMillis</summary>
			public int pollingIntervalMillis;

			/// <summary>nextPageToken</summary>
			public string nextPageToken;

			/// <summary>pageInfo</summary>
			public PageInfo pageInfo;

			/// <summary>items</summary>
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

			if((type&Type.UseLiveChatMessagePageToken) > 0){
				unitywebrequest.url = string.Format(
					"{0}{1}?key={2}&liveChatId={3}&part={4}&pageToken={5}",
					"https://www.googleapis.com/youtube/v3/",
					"liveChat/messages",
					work.apikey,
					work.livechatid,
					"id,snippet,authorDetails",
					work.livechatpagetoken
				);
			}else{
				unitywebrequest.url = string.Format(
					"{0}{1}?key={2}&liveChatId={3}&part={4}",
					"https://www.googleapis.com/youtube/v3/",
					"liveChat/messages",
					work.apikey,
					work.livechatid,
					"id,snippet,authorDetails"
				);
			}
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
						bool error = false;

						string result_string = unitywebrequest.downloadHandler.text;
						Result result = UnityEngine.JsonUtility.FromJson<Result>(result_string);

						//ログ。
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

						//トークン設定。
						if((type&Type.SetLiveChatMessagePageToken) > 0){
							if(string.IsNullOrWhiteSpace(result.nextPageToken) == true){
								error = true;
								work.livechatpagetoken = "";
							}else{
								work.livechatpagetoken = result.nextPageToken;
							}
						}

						//クリア。
						if((type&Type.SetLiveChatMessage) > 0){
							work.livechatmessage.list.Clear();
						}

						//メッセージ追加。
						if((type&(Type.SetLiveChatMessage|Type.AddLiveChatMessage)) > 0){
							for(int ii=0;ii<result.items.Length;ii++){
								ref Result.Item item = ref result.items[ii];
								work.livechatmessage.list.Add(new LiveChatMessage.Item(){
									snippet_displaymessage = item.snippet.displayMessage,
									authordetails_displayname = item.authorDetails.displayName,
									authordetails_channelurl = item.authorDetails.channelUrl,
									authordetails_profileimageurl = item.authorDetails.profileImageUrl,
								});
							}
						}

						unitywebrequest.Dispose();

						if(error == true){
							return ActionResult.Error;
						}
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

