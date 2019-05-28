using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject panel;
    public GameObject text;
    public GameObject button;

    void Start()
    {
        m_buttonPositionY = button.transform.localPosition.y;
        m_panelOpacity = panel.GetComponent<Image>().color.a;


        StartCoroutine("TutorialWindowCoroutine");
        button.transform.localPosition += new Vector3(0, 100, 0);

        Color tempPanelColor = panel.GetComponent<Image>().color;
        tempPanelColor.a = 0;
        panel.GetComponent<Image>().color = tempPanelColor;

        Color tempTextColor = text.GetComponent<Text>().color;
        tempTextColor.a = 0;
        text.GetComponent<Text>().color = tempTextColor;
    }

    void Update()
    {
        
    }

    private IEnumerator TutorialWindowCoroutine()
    {
        while (m_tutorialOpen)
        {
            if (button.transform.localPosition.y > m_buttonPositionY)
            {
                button.transform.localPosition -= new Vector3(0, 3, 0);
                yield return new WaitForSeconds(0.005f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
            
        }
        yield break;
    }

    public void CloseTutorial()
    {
        m_tutorialOpen = false;
    }

    private bool m_tutorialOpen = true;
    private float m_buttonPositionY;
    private float m_panelOpacity;
}
