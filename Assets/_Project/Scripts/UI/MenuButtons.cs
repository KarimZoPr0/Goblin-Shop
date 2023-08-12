using UnityEngine;

public class MenuButtons : MonoBehaviour
{
	public int menuScene = 0;
	public int levelsScene = 2;
	public int aboutScene  = 1;

	public void LoadMenu() {
		ReferenceUI.transitor.LoadScene(menuScene);
	}

	public void LoadLevels() {
		print("load");
		ReferenceUI.transitor.LoadScene(levelsScene);
	}

	public void LoadAbout() {
		ReferenceUI.transitor.LoadScene(aboutScene);
	}

	public void Quit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}