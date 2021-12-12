using UnityEngine;

public class PlayerUI : MonoBehaviour {
	public delegate void Test();

	public GameObject settingsObj;
	public Animation  loadScreen;

	public MouseController crosshair;
	// Start is called before the first frame update

	private void Start() {
		LoadScreen("OpenRight");
	}

	// Update is called once per frame
	private void Update() {
		if (Input.GetButtonDown("Cancel")) {
			settingsObj.SetActive(!settingsObj.activeSelf);

			if (settingsObj.activeSelf) {
				crosshair.ChangeCrosshair("Select");
				Time.timeScale = 0;
			}
			else {
				Time.timeScale = 1;
			}
		}
	}

	private void OnDestroy() {
		Time.timeScale = 1;
	}

	public void LoadScreen(string anim) {
		loadScreen.Play("LoadScreen_" + anim);
	}

	public void CloseSettings() {
		settingsObj.SetActive(false);
		Time.timeScale = 1;
	}

	public void ReloadScene() {
		Time.timeScale = 1;
		//Reference.transitor.ReloadScene();
	}
}