using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
    }
    public void StartLevelOne(string sceneName) {
        settings.SetLevelOne();
        Application.LoadLevel(sceneName);
    }
    public void StartLevelTwo(string sceneName)
    {
        settings.SetLevelTwo();
        Application.LoadLevel(sceneName);
    }
    public void StartLevelThree(string sceneName)
    {
        settings.SetLevelThree();
        Application.LoadLevel(sceneName);
    }
    public void StartLevelFour(string sceneName)
    {
        settings.SetLevelFour();
        Application.LoadLevel(sceneName);
    }
    public void StartLevelEndless(string sceneName)
    {
        settings.SetEndless();
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private LevelSettings settings;
}
