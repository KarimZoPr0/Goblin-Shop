using UnityEngine;

public class MenuButtons : MonoBehaviour {
	public int menuScene;
	public int levelsScene = 1;
	public int aboutScene  = 2;

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
		Application.Quit();
	}
}