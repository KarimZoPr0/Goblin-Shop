using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject settingsObj;
    public Animation loadScreen;
    public Slider abilityBar;
    public MouseController crosshair;
    // Start is called before the first frame update
    void Start()
    {
        Reference.ui.LoadScreen("OpenRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            settingsObj.SetActive(!settingsObj.activeSelf);

            if (settingsObj.activeSelf)
            {
                Reference.ui.crosshair.ChangeCrosshair("Select");
                Time.timeScale = 0;
            }
            else
                Time.timeScale = 1;
        }
    }
    public void LoadScreen(string anim)
    {
        loadScreen.Play("LoadScreen_" + anim);
    }
    public void LoadDay(string anim)
    {
        loadScreen.Play("LoadScreen_Right_" + anim);
    }
    public void CloseSettings()
    {
        settingsObj.SetActive(false);
        Time.timeScale = 1;
    }
    public void ReloadScene()
    {
        Time.timeScale = 1;
        Reference.transitor.ReloadScene();
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
