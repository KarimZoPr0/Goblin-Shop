using DG.Tweening;
using UnityEngine;

namespace GSS.Control {
	public class CameraController : MonoBehaviour {
		public void ShakeCamera(float strength = 0.1f, float duration = 0.1f) {
			transform.parent.DOShakePosition(duration, strength);
		}
	}
}