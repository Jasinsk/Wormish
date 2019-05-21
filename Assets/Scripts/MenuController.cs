using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void ChangeScene(string sceneName) {
        Application.LoadLevel(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
