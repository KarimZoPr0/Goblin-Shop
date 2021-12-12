using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour {
	public static bool restart;
	private       int  targetScene;

	/*
	string music;
	int NightShoppingID = 5;
	int NightCombatID = 5;
	int DayShoppingID = 4;
	int DayCombatID = 4;
	*/

	private void Start() {
		ReferenceUI.transitor = this;
	}

	public bool LoadScene(int sceneNum) {
		if (sceneNum == 0 && restart) return true;
		restart     = true;
		targetScene = sceneNum;
		StartCoroutine(Transition());

		if (sceneNum == 0) return false;

		return true;
	}

	public void LoadDay() {
		StartCoroutine(Transition());
	}

	public void LoadNext(string music) {
		//Reference.audio.Play(music);
		StartCoroutine(Transition());
	}

	public void Fade() {
		StartCoroutine(fade());
	}

	public void ReloadScene() {
		targetScene = SceneManager.GetActiveScene().buildIndex;
		StartCoroutine(Transition());
	}

	private IEnumerator Transition() {
		ReferenceUI.ui.LoadScreen("Right");
		yield return new WaitForSecondsRealtime(0.45f);
		SceneManager.LoadScene(targetScene);
		Time.timeScale = 1;
	}

	private IEnumerator fade() {
		ReferenceUI.ui.LoadScreen("OpenRight");
		yield return new WaitForSecondsRealtime(0.45f);
		Time.timeScale = 1;
	}
}