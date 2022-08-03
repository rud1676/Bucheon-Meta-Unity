
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class LoadStartScene : EditorWindow
{
	private const string SCENE_BASE_ROOT = "Assets/SCENES/";

	public static void SaveCurrentScene()
	{
		//현재 씬이 저장 안되어 있을 경우, 팝업으로 물어봄.
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().isDirty)
		{
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		}
	}

	[MenuItem("OpenScene/Intro")]
	public static void OpenContainerScene()
	{
		SaveCurrentScene();
		EditorSceneManager.OpenScene(SCENE_BASE_ROOT + "Intro.unity");
	}

	[MenuItem("OpenScene/Login")]
	public static void OpenDashboardScene()
	{
		SaveCurrentScene();
		EditorSceneManager.OpenScene(SCENE_BASE_ROOT + "LOGINSCENE.unity");
	}

	[MenuItem("OpenScene/Bucheon")]
	public static void OpenMakeUIScene()
	{
		SaveCurrentScene();
		EditorSceneManager.OpenScene(SCENE_BASE_ROOT + "Bucheon.unity");
	}


	//바로가기 하고싶은 씬 아래에 함수추가해서 사용.

}
