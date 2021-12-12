using DG.Tweening;
using UnityEngine;

public class UIButton : MonoBehaviour {
	public void MouseOn() {
		transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.5f);
		print("mouse on");
	}

	public void MouseOff() {
		transform.DOScale(new Vector3(1f, 1f, 1), 0.5f);
		print("mouse off");
	}

	public void MouseOnBig() {
		transform.DOScale(new Vector3(-30f, -30f, 1), 0.5f);
	}

	public void MouseOffBig() {
		transform.DOScale(new Vector3(-30f, -30f, -30f), 0.5f);
	}
}