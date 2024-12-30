


/// <summary>Project.Test</summary>
namespace Project.Test
{
	/// <summary>
	/// 
	/// API作成 : https://console.cloud.google.com/apis/dashboard
	/// 
	/// 
	/// 
	/// </summary>
	[System.Serializable]
	[UnityEditor.CustomEditor(typeof(Test_MonoBehaviour))]
	public class Test_CustomEditor : UnityEditor.Editor 
	{
		/// <summary>work</summary>
		public static YoutubeLib.Work work;

		/// <summary>monobehaviour</summary>
		public Test_MonoBehaviour monobehaviour;

		public struct TextField
		{
			/// <summary>textfield</summary>
			public UnityEngine.UIElements.TextField textfield;

			/// <summary>text</summary>
			public string text;

			/// <summary>Initialize</summary>
			public void Initialize(UnityEngine.UIElements.TextField a_textfield)
			{
				textfield = a_textfield;
				textfield.value = text;
			}

			/// <summary>Set</summary>
			public void Set(string a_text)
			{
				text = a_text;
				textfield.value = a_text;
			}

			/// <summary>Get</summary>
			public string Get()
			{
				return text;
			}
		}

		private static TextField apikey;
		private static TextField videoid;
		private static TextField livechatid;
		private static TextField livechatpagetoken;

		/// <summary>Update</summary>
		public void Update()
		{
			YoutubeLib.Action.Do(work);
		}

		/// <summary>CreateInspectorGUI</summary>
		public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
		{
			if(work == null){
				work = YoutubeLib.CreateWork.Do();
			}

			{
				UnityEditor.EditorApplication.update -= Update;
				monobehaviour = target as Test_MonoBehaviour;
				if(monobehaviour != null){
					UnityEditor.EditorApplication.update += Update;
				}else{
					UnityEngine.Debug.LogError("error");
				}
			}

			//inspector
			UnityEngine.UIElements.VisualElement inspector = new UnityEngine.UIElements.VisualElement();

			//apikey
			{
				UnityEngine.UIElements.Foldout foldout_apikey = new UnityEngine.UIElements.Foldout();
				{
					foldout_apikey.value = false;
					foldout_apikey.text = "ApiKey";
					inspector.Add(foldout_apikey);

					UnityEngine.UIElements.TextField textfield = new UnityEngine.UIElements.TextField();
					{
						textfield.multiline = true;
						textfield.maxLength = 140;
						textfield.value = "";
						foldout_apikey.Add(textfield);
					}

					apikey.Initialize(textfield);

					if(string.IsNullOrWhiteSpace(apikey.Get()) == true){
						if(System.IO.File.Exists("../apikey.txt") == true){
							using(System.IO.TextReader file = new System.IO.StreamReader("../apikey.txt")){
								apikey.Set(file.ReadToEnd());
								UnityEngine.Debug.Log(apikey);
							}
						}
					}
				}


			}

			//videoid
			{
				UnityEngine.UIElements.Foldout foldout = new UnityEngine.UIElements.Foldout();
				{
					foldout.value = true;
					foldout.text = "VideoID";
					inspector.Add(foldout);

					UnityEngine.UIElements.TextField textfield = new UnityEngine.UIElements.TextField();
					{
						textfield.multiline = true;
						textfield.maxLength = 140;
						textfield.value = "";
						foldout.Add(textfield);
					}

					videoid.Initialize(textfield);

					if(string.IsNullOrWhiteSpace(videoid.Get()) == true){
						if(System.IO.File.Exists("../videoid.txt") == true){
							using(System.IO.TextReader file = new System.IO.StreamReader("../videoid.txt")){
								videoid.Set(file.ReadToEnd());
								UnityEngine.Debug.Log(videoid);
							}
						}
					}
				}
			}

			//livechatid
			{
				UnityEngine.UIElements.Foldout foldout = new UnityEngine.UIElements.Foldout();
				{
					foldout.value = true;
					foldout.text = "LiveChatID";
					inspector.Add(foldout);

					UnityEngine.UIElements.TextField textfield = new UnityEngine.UIElements.TextField();
					{
						textfield.multiline = true;
						textfield.maxLength = 140;
						textfield.value = "";
						foldout.Add(textfield);
					}

					livechatid.Initialize(textfield);
				}
			}

			//livechatpagetoken
			{
				UnityEngine.UIElements.Foldout foldout = new UnityEngine.UIElements.Foldout();
				{
					foldout.value = true;
					foldout.text = "LiveChatPageToken";
					inspector.Add(foldout);

					UnityEngine.UIElements.TextField textfield = new UnityEngine.UIElements.TextField();
					{
						textfield.multiline = true;
						textfield.maxLength = 140;
						textfield.value = "";
						foldout.Add(textfield);
					}

					livechatpagetoken.Initialize(textfield);
				}
			}

			UnityEngine.UIElements.Foldout foldout_list = new UnityEngine.UIElements.Foldout();
			{
				foldout_list.value = true;
				foldout_list.text = "List";
				inspector.Add(foldout_list);
			}

			//実行。
			{
				UnityEngine.UIElements.Button button = new UnityEngine.UIElements.Button(()=>{
					YoutubeLib.SetApiKey.Do(work,apikey.Get());
					YoutubeLib.SetVideoID.Do(work,videoid.Get());

					if(string.IsNullOrWhiteSpace(livechatid.Get()) == true){
						//ライブチャットＩＤ取得。
						{
							YoutubeLib.Action_Base action = new YoutubeLib.Action_LiveStreamingDetails(work,YoutubeLib.Action_LiveStreamingDetails.Type.SetLivechatId | YoutubeLib.Action_LiveStreamingDetails.Type.Log);
							work.actionlist.AddLast(action);
						}

						{
							YoutubeLib.Action_Base action = new YoutubeLib.Action_CallBack(work,YoutubeLib.Action_CallBack.Type.Log,()=>{
								livechatid.Set(work.livechatid);
								return YoutubeLib.ActionResult.Success;
							});
							work.actionlist.AddLast(action);
						}
					}else{
						//テキストフィールドのライブチャットＩＤを使用。
						work.livechatid = livechatid.Get();		
					}

					{
						{
							if(string.IsNullOrWhiteSpace(livechatpagetoken.Get()) == true){
								//ライブチャットメッセージ取得。
								YoutubeLib.Action_Base action = new YoutubeLib.Action_LiveChat_Messages(
									work,
									YoutubeLib.Action_LiveChat_Messages.Type.Log |
									YoutubeLib.Action_LiveChat_Messages.Type.SetLiveChatMessage |
									YoutubeLib.Action_LiveChat_Messages.Type.SetLiveChatMessagePageToken
								);
								work.actionlist.AddLast(action);
							}else{
								//テキストフィールドのライブチャットページトークンを使用。
								work.livechatpagetoken = livechatpagetoken.Get();

								YoutubeLib.Action_Base action = new YoutubeLib.Action_LiveChat_Messages(
									work,
									YoutubeLib.Action_LiveChat_Messages.Type.Log |
									YoutubeLib.Action_LiveChat_Messages.Type.SetLiveChatMessage |
									YoutubeLib.Action_LiveChat_Messages.Type.SetLiveChatMessagePageToken |
									YoutubeLib.Action_LiveChat_Messages.Type.UseLiveChatMessagePageToken
								);
								work.actionlist.AddLast(action);
							}
						}

						{
							YoutubeLib.Action_Base action = new YoutubeLib.Action_CallBack(work,YoutubeLib.Action_CallBack.Type.Log,()=>{

								livechatpagetoken.Set(work.livechatpagetoken);

								foldout_list.Clear();

								UnityEngine.UIElements.ScrollView scrollview = new UnityEngine.UIElements.ScrollView(UnityEngine.UIElements.ScrollViewMode.Vertical);
								foldout_list.Add(scrollview);
								scrollview.style.maxHeight = 1000.0f;
								
								for(int ii=0;ii<work.livechatmessage.list.Count;ii++){
									YoutubeLib.LiveChatMessage.Item item = work.livechatmessage.list[ii];

									{
										UnityEngine.UIElements.GroupBox box = new UnityEngine.UIElements.GroupBox("");
										box.style.backgroundColor = new UnityEngine.UIElements.StyleColor(new UnityEngine.Color(0.1f,0.1f,0.1f,1.0f));
										scrollview.contentContainer.Add(box);

										UnityEngine.UIElements.Label label_authordetails_displayname = new UnityEngine.UIElements.Label(item.authordetails_displayname);
										box.Add(label_authordetails_displayname);

										UnityEngine.UIElements.Label label_snippet_displaymessage = new UnityEngine.UIElements.Label(item.snippet_displaymessage);
										box.Add(label_snippet_displaymessage);

										UnityEngine.UIElements.Label label_authordetails_channelurl = new UnityEngine.UIElements.Label(item.authordetails_channelurl);
										box.Add(label_authordetails_channelurl);

										UnityEngine.UIElements.Label label_authordetails_profileimageurl = new UnityEngine.UIElements.Label(item.authordetails_profileimageurl);
										box.Add(label_authordetails_profileimageurl);
									}
								}

								return YoutubeLib.ActionResult.Success;
							});

							work.actionlist.AddLast(action);
						}
					}
				});

				button.text = "実行";
				inspector.Add(button);
			}

			return inspector;
		}
	}
}

