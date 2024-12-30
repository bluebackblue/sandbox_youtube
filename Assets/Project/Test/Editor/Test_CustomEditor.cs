


/// <summary>Project</summary>
namespace Project
{
	/// <summary>Test_CustomEditor</summary>
	[UnityEditor.CustomEditor(typeof(Test_MonoBehaviour))]
	public class Test_CustomEditor : UnityEditor.Editor 
	{
		/// <summary>work</summary>
		public YoutubeLib.Work work;

		/// <summary>monobehaviour</summary>
		public Test_MonoBehaviour monobehaviour;

		/// <summary>apikey</summary>
		private string apikey;

		/// <summary>videoid</summary>
		private string videoid;

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

			if(string.IsNullOrWhiteSpace(apikey) == true){
				using(System.IO.TextReader file = new System.IO.StreamReader("../apikey.txt")){
					apikey = file.ReadToEnd();
					UnityEngine.Debug.Log(apikey);
				}
			}

			if(string.IsNullOrWhiteSpace(videoid) == true){
				using(System.IO.TextReader file = new System.IO.StreamReader("../videoid.txt")){
					videoid = file.ReadToEnd();
					UnityEngine.Debug.Log(videoid);
				}
			}

			UnityEngine.UIElements.VisualElement inspector = new UnityEngine.UIElements.VisualElement();

			//apikey
			{
				UnityEngine.UIElements.Foldout foldout_apikey = new UnityEngine.UIElements.Foldout();
				{
					foldout_apikey.value = false;
					foldout_apikey.text = "ApiKey";
					inspector.Add(foldout_apikey);

					UnityEngine.UIElements.TextField textfield_clientid = new UnityEngine.UIElements.TextField();
					{
						textfield_clientid.multiline = true;
						textfield_clientid.maxLength = 140;
						textfield_clientid.value = apikey;

						textfield_clientid.RegisterCallback<UnityEngine.UIElements.ChangeEvent<string>>((changeevent) => {
							apikey = changeevent.newValue;
						});

						foldout_apikey.Add(textfield_clientid);
					}
				}
			}

			//videoid
			{
				UnityEngine.UIElements.Foldout foldout_videoid = new UnityEngine.UIElements.Foldout();
				{
					foldout_videoid.value = true;
					foldout_videoid.text = "VideoID";
					inspector.Add(foldout_videoid);

					UnityEngine.UIElements.TextField textfield_clientid = new UnityEngine.UIElements.TextField();
					{
						textfield_clientid.multiline = true;
						textfield_clientid.maxLength = 140;
						textfield_clientid.value = videoid;

						textfield_clientid.RegisterCallback<UnityEngine.UIElements.ChangeEvent<string>>((changeevent) => {
							videoid = changeevent.newValue;
						});

						foldout_videoid.Add(textfield_clientid);
					}
				}
			}

			//実行。
			{
				UnityEngine.UIElements.Button button = new UnityEngine.UIElements.Button(()=>{
					YoutubeLib.SetApiKey.Do(work,apikey);
					YoutubeLib.SetVideoID.Do(work,videoid);

					{
						YoutubeLib.Action_Base action = new YoutubeLib.Action_LiveStreamingDetails(work,YoutubeLib.Action_LiveStreamingDetails.Type.SetLivechatId | YoutubeLib.Action_LiveStreamingDetails.Type.Log);
						work.actionlist.AddLast(action);
					}

					{
						YoutubeLib.Action_Base action = new YoutubeLib.Action_LiveChat_Messages(work,YoutubeLib.Action_LiveChat_Messages.Type.Log);
						work.actionlist.AddLast(action);
					}
				});

				button.text = "実行";
				inspector.Add(button);
			}

			return inspector;
		}
	}
}

