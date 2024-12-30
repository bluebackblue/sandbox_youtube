


/// <summary>YoutubeLib</summary>
namespace YoutubeLib
{
	/// <summary>Action_CallBack</summary>
	public class Action_CallBack : Action_Base
	{
		[System.Flags]
		public enum Type
		{
			Log = 1,
		}

		/// <summary>type</summary>
		private Type type;

		/// <summary>work</summary>
		private Work work;

		/// <summary>action</summary>
		private System.Func<ActionResult> action;

		/// <summary>constructor</summary>
		public Action_CallBack(Work a_work,Type a_type,System.Func<ActionResult> a_action)
		{
			//work
			work = a_work;

			//a_type
			type = a_type;

			//action
			action = a_action;
		}

		/// <summary>Request</summary>
		public void Request()
		{
		}

		/// <summary>Update</summary>
		public ActionResult Update()
		{
			try{
				return action();
			}catch(System.Exception exception){
				UnityEngine.Debug.LogError(exception.ToString());
				return ActionResult.Error;
			}
		}
	}
}

