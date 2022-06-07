using DG.Tweening;
using UnityEngine;

public class UIButton : MonoBehaviour {
	public void MouseOn() {
		transform.DOScale(new Vector3(2.7f, 2.7f, 2.7f), 0.5f);
	}

	public void MouseOff() {
		transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.5f);
	}

	public void MouseOnBig() {
		transform.DOScale(new Vector3(-30f, -30f, 1), 0.5f);
	}

	public void MouseOffBig() {
		transform.DOScale(new Vector3(-30f, -30f, -30f), 0.5f);
	}
}