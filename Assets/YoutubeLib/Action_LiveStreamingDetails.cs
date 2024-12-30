


/// <summary>YoutubeLib</summary>
namespace YoutubeLib
{
	/// <summary>Action_LiveStreamingDetails</summary>
	public class Action_LiveStreamingDetails : Action_Base
	{
		[System.Flags]
		public enum Type
		{
			Log = 1,
			SetLivechatId = 1 << 1,
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
			/// <summary>LiveStreamingDetails</summary>
			[System.Serializable]
			public struct LiveStreamingDetails
			{
				public string actualStartTime;
				public string scheduledStartTime;
				public string concurrentViewers;
				public string activeLiveChatId;
			};

			/// <summary>PageInfo</summary>
			[System.Serializable]
			public struct PageInfo
			{
				public string totalResults;
				public string resultsPerPage;
			}

			/// <summary>Item</summary>
			[System.Serializable]
			public struct Item
			{
				public string kind;
				public string etag;
				public string id;
				public LiveStreamingDetails liveStreamingDetails;
			}

			public string kind;
			public string etag;
			public PageInfo pageInfo;
			public Item[] items;
		}

		/// <summary>constructor</summary>
		public Action_LiveStreamingDetails(Work a_work,Type a_type)
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
				"{0}{1}?key={2}&id={3}&part={4}",
				"https://www.googleapis.com/youtube/v3/",
				"videos",
				work.apikey,
				work.videoid,
				"liveStreamingDetails"
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
						bool error = false;

						string result_string = unitywebrequest.downloadHandler.text;
						Result result = UnityEngine.JsonUtility.FromJson<Result>(result_string);

						//ログ。
						if((type&Type.Log) > 0){
							UnityEngine.Debug.Log(result_string);

							using(System.IO.TextWriter file = new System.IO.StreamWriter("../Log_Action_LiveStreamingDetails.txt")){
								file.WriteLine(result_string);
								file.Close();
								file.Dispose();
							}

							UnityEngine.Debug.Log("kind : " + result.kind);
							UnityEngine.Debug.Log("etag : " + result.etag);
							UnityEngine.Debug.Log("totalResults : " + result.pageInfo.totalResults);
							UnityEngine.Debug.Log("resultsPerPage : " + result.pageInfo.resultsPerPage);
							for(int ii=0;ii<result.items.Length;ii++){
								UnityEngine.Debug.Log("[" + ii.ToString() + "]kind : " + result.items[ii].kind);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]etag : " + result.items[ii].etag);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]id : " + result.items[ii].id);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]actualStartTime : " + result.items[ii].liveStreamingDetails.actualStartTime);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]scheduledStartTime : " + result.items[ii].liveStreamingDetails.scheduledStartTime);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]concurrentViewers : " + result.items[ii].liveStreamingDetails.concurrentViewers);
								UnityEngine.Debug.Log("[" + ii.ToString() + "]activeLiveChatId : " + result.items[ii].liveStreamingDetails.activeLiveChatId);
							}
						}

						//ライブチャットＩＤ設定。
						if((type&Type.SetLivechatId) > 0){
							if(result.items.Length > 0){
								work.livechatid = result.items[0].liveStreamingDetails.activeLiveChatId;
								if(string.IsNullOrWhiteSpace(work.livechatid) == true){
									error = true;
									work.livechatid = "";
								}
							}else{
								error = true;
								work.livechatid = "";
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

