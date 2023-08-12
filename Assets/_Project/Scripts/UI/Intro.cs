using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class Intro : MonoBehaviour {
	[SerializeField] private float duration;
	[SerializeField] private float endValue;


	[SerializeField] private Ease             ease;
	[SerializeField] private PlayableDirector director;

	public void OnEnable() {
		StartCoroutine(StartIntro());
	}

	private IEnumerator StartIntro() {
		yield return new WaitForSeconds(.65f);
		transform
			.DOMoveY(endValue, duration)
			.SetEase(ease).OnComplete(() => director.Play());
	}
}