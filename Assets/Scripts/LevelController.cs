using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // The class responsible for switching between different levels of the game

    public string gameScene;
    public string menuScene;

    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
    }
    // Starts the level which number is passed through as an argument
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
    // These functions are used to make the buttons of the GUI work
    // Switches onto the next level
    public void NextLevel()
    {
        settings.SetLevelReplay(false);
        StartLevel(settings.currentLevel + 1);
    }
    // Plays same level once more
    public void ReplayLevel()
    {
        settings.SetLevelReplay(true);
        StartLevel(settings.currentLevel);
    }
    // Changes scene to the main menu
    public void ReturnToMenu()
    {
        settings.SetLevelReplay(false);
        Application.LoadLevel(menuScene);
    }
    // Exits game
    public void QuitGame()
    {
        Application.Quit();
    }

    private LevelSettings settings;
}
