using UnityEngine;

public class SceneInitializer : MonoBehaviour {
	public GameObject playerUIPrefab;
	public GameObject audioManagerPrefab;

	private void Awake() {
		var refObj = Instantiate(new GameObject(), transform.position, Quaternion.identity, transform);
		refObj.name = "Refence Manager";
		refObj.AddComponent<ReferenceUI>();

		var sceneTransitor = Instantiate(new GameObject(), transform.position, Quaternion.identity, transform);
		sceneTransitor.name = "Scenes Manager";
		sceneTransitor.AddComponent<SceneTransitor>();
	}

	private void Start() {
		var ui = Instantiate(playerUIPrefab, transform.position, Quaternion.identity).GetComponent<PlayerUI>();
		ReferenceUI.ui = ui;
		//
		// if (AudioManager.instance == null)
		// {
		//     Instantiate(audioManagerPrefab, transform.position, Quaternion.identity);
		// }
	}
}