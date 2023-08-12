using System;
using UnityEngine;

public class ReferenceUI : MonoBehaviour {
	public static PlayerUI       ui;
	public static SceneTransitor transitor;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}