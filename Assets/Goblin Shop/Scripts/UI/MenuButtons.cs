using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public int menuScene = 0;
    public int levelsScene = 1;
    public int aboutScene = 2;

    public void LoadMenu()
    {
        Reference.transitor.LoadScene(menuScene);
    }
    public void LoadLevels()
    {
        print("load");
        Reference.transitor.LoadScene(levelsScene);
    }
    public void LoadAbout()
    {
        Reference.transitor.LoadScene(aboutScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
