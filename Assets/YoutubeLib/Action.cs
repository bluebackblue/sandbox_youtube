


/// <summary>YoutubeLib</summary>
namespace YoutubeLib
{
	/// <summary>Action</summary>
	public static class Action
	{
		/// <summary>Do</summary>
		public static void Do(Work work)
		{
			if(work.current == null){
				if(work.actionlist.First != null){
					work.current = work.actionlist.First.Value;
					work.actionlist.RemoveFirst();
					work.current.Request();
				}
			}

			if(work.current != null){
				switch(work.current.Update()){
				case ActionResult.Busy:
					{
					}break;
				case ActionResult.Success:
					{
						work.current = null;
					}break;
				case ActionResult.Error:
					{
						UnityEngine.Debug.LogErrorFormat("error : {0}",work.current.GetType());

						work.current = null;
						work.actionlist.Clear();
					}break;
				}
			}
		}
	}
}

