


/// <summary>Project.Test</summary>
namespace Project.Test
{
	/// <summary>Foo_MonoBehaviour</summary>
	[System.Serializable]
	public class Foo_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		[System.Serializable]
		public struct Item
		{
			public string a;
			public string b;
			public string c;
		}

		[UnityEngine.SerializeField]
		public Item[] list;
	}
}

