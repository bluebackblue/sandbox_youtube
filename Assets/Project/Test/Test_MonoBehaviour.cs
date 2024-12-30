


/// <summary>Project</summary>
namespace Project
{
	/// <summary>Test_MonoBehaviour</summary>
	[System.Serializable]
	public class Test_MonoBehaviour : UnityEngine.MonoBehaviour
	{

	}

	/// <summary>Test_CustomEditor</summary>
	[UnityEditor.CustomEditor(typeof(Test_MonoBehaviour))]
	public class Test_CustomEditor : UnityEditor.Editor 
	{
		/// <summary>CreateInspectorGUI</summary>
		public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
		{
			UnityEngine.UIElements.VisualElement inspector = new UnityEngine.UIElements.VisualElement();

			{
				UnityEngine.UIElements.Label label = new UnityEngine.UIElements.Label("ラベル\nラベル");

				inspector.Add(label);
			}

			{
				UnityEngine.UIElements.Button button = new UnityEngine.UIElements.Button(()=>{
					UnityEngine.Debug.Log("ボタン");
				});
				button.text = "あいうえお";
				inspector.Add(button);
			}


			return inspector;
		}
	}


	#if(false)


	/// <summary>Test_PropertyDrawer</summary>
	[UnityEditor.CustomPropertyDrawer(typeof(Test_MonoBehaviour))]
	public class Test_PropertyDrawer : UnityEditor.PropertyDrawer
	{
		/// <summary>GetPropertyHeight</summary>
		public override float GetPropertyHeight(UnityEditor.SerializedProperty property,UnityEngine.GUIContent label)
		{
			//return    UnityEditor.EditorGUI.GetPropertyHeight(property,label,true);

			return UnityEditor.EditorGUIUtility.singleLineHeight;
		}

		/// <summary>OnGUI</summary>
		public override void OnGUI(UnityEngine.Rect position,UnityEditor.SerializedProperty property,UnityEngine.GUIContent label)
		{
			UnityEngine.Debug.Log("OnGUI");

			UnityEngine.Rect rect = new UnityEngine.Rect(
				position.x,
				position.y,
				position.width,
				UnityEditor.EditorGUIUtility.singleLineHeight
			);

			UnityEditor.EditorGUI.LabelField(position,"abc");


			/*
			{
				UnityEngine.Rect rect = new UnityEngine.Rect(position.x,position.y,position.width,position.y);

				UnityEditor.EditorGUI.DropdownButton(position,new UnityEngine.GUIContent(""),UnityEngine.FocusType.Keyboard);
			}
			*/


			/*
			UnityEditor.EditorGUI.PropertyField(position,property,label,true);
			*/
		}
	}
	#endif
}

