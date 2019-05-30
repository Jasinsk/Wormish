using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject menuCanvas;
    
    void Start()
    {
        m_tutorialCanvas = tutorialCanvas.GetComponent<Canvas>();
        m_menuCanvas = menuCanvas.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTutorial()
    {
        m_tutorialCanvas.enabled = true;
        m_menuCanvas.enabled = false;
    }

    public void CloseTutorial()
    {
        m_tutorialCanvas.enabled = false;
        m_menuCanvas.enabled = true;
    }
    private Canvas m_tutorialCanvas, m_menuCanvas;
}
