using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public GameObject playerUIPrefab;
    public GameObject audioManagerPrefab;
    private void Awake()
    {
        GameObject refObj = Instantiate(new GameObject(), transform.position, Quaternion.identity, transform);
        refObj.name = "Refence Manager";
        refObj.AddComponent<Reference>();

        GameObject sceneTransitor = Instantiate(new GameObject(), transform.position, Quaternion.identity, transform);
        sceneTransitor.name = "Scenes Manager";
        sceneTransitor.AddComponent<SceneTransitor>();

    }
    private void Start()
    {
        PlayerUI ui = Instantiate(playerUIPrefab, transform.position, Quaternion.identity).GetComponent<PlayerUI>();
        Reference.ui = ui;
        //
        // if (AudioManager.instance == null)
        // {
        //     Instantiate(audioManagerPrefab, transform.position, Quaternion.identity);
        // }
    }

}
