using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class regulating the tutorial canvas
public class TutorialController : MonoBehaviour
{
    // gets the tutorial and menu canvases
    public GameObject tutorialCanvas;
    public GameObject menuCanvas;
    
    void Start()
    {
        m_tutorialCanvas = tutorialCanvas.GetComponent<Canvas>();
        m_menuCanvas = menuCanvas.GetComponent<Canvas>();
    }
    //switches between the canvases upon opening
    public void OpenTutorial()
    {
        m_tutorialCanvas.enabled = true;
        m_menuCanvas.enabled = false;
    }
    // switches between canvases upon closing
    public void CloseTutorial()
    {
        m_tutorialCanvas.enabled = false;
        m_menuCanvas.enabled = true;
    }
    private Canvas m_tutorialCanvas, m_menuCanvas;
}
