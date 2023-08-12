using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {
	public new Image renderer;

	public Crosshair[] Crosshairs;

	[HideInInspector] public int activeNum;

	private RectTransform rect;
	// Update is called once per frame

	private void Start() {
		rect = GetComponent<RectTransform>();
	}

	private void Update() {
		Cursor.visible = false;

		var mousePos = Input.mousePosition;
		rect.position = Vector2.Lerp(rect.position, mousePos, Crosshairs[activeNum].moveSpeed);

		renderer.sprite = Crosshairs[activeNum].Image;
	}

	public void ChangeCrosshair(string crossHairName) {
		for (var i = 0; i < Crosshairs.Length; i++)
			if (Crosshairs[i].Name == crossHairName)
				activeNum = i;
	}
}