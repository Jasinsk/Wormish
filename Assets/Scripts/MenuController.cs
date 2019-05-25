using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public string gameScene;
    public string menuScene;
    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
    }
    public void StartLevel(int level) {
        switch (level)
        {
            case 1:
                settings.SetLevelOne();
                settings.currentLevel = 1;
                break;
            case 2:
                settings.SetLevelTwo();
                settings.currentLevel = 2;
                break;
            case 3:
                settings.SetLevelThree();
                settings.currentLevel = 3;
                break;
            case 4:
                settings.SetLevelFour();
                settings.currentLevel = 4;
                break;
            case 5:
                settings.SetEndless();
                settings.currentLevel = 5;
                break;

        }
        Application.LoadLevel(gameScene);
    }
    public void NextLevel()
    {
        StartLevel(settings.currentLevel + 1);
    }

    public void ReturnToMenu()
    {
        Application.LoadLevel(menuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private LevelSettings settings;
}
