using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{
    public static bool restart;
    private int targetScene;

    private void Start()
    {
        ReferenceUI.transitor = this;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(int sceneNum)
    {
        targetScene = sceneNum;
        StartCoroutine(Transition());
    }

    public void LoadDay()
    {
        StartCoroutine(Transition());
    }

    public void LoadNext(string music)
    {
        //Reference.audio.Play(music);
        StartCoroutine(Transition());
    }

    public void Fade()
    {
        StartCoroutine(fade());
    }

    public void ReloadScene()
    {
        targetScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Transition());
    }

    public IEnumerator Transition()
    {
        ReferenceUI.ui.LoadScreen("Right");
        yield return new WaitForSecondsRealtime(0.55f);
        SceneManager.LoadScene(targetScene);
        Time.timeScale = 1;
    }

    private IEnumerator fade()
    {
        ReferenceUI.ui.LoadScreen("OpenRight");
        yield return new WaitForSecondsRealtime(0.45f);
        Time.timeScale = 1;
    }
}