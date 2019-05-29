using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public float tutorialDuration = 5;
    public GameObject panel;
    public GameObject text;
    public GameObject button;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        m_buttonPositionY = button.transform.localPosition.y;
        m_panelOpacity = panel.GetComponent<Image>().color.a;

        button.transform.localPosition += new Vector3(0, 200, 0);
        StartCoroutine("TutorialWindowCoroutine");

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
                button.transform.localPosition -= new Vector3(0, 5, 0);
                yield return new WaitForSeconds(0.005f);
            }
            else if (text.GetComponent<Text>().color.a < 0.9)
            {
                if (!m_played)
                {
                    m_audioSource.PlayOneShot(m_audioSource.clip);
                    m_played = true;
                }
                if (panel.GetComponent<Image>().color.a < m_panelOpacity)
                {
                    Color tempPanelColor = panel.GetComponent<Image>().color;
                    tempPanelColor.a += 0.05f;
                    panel.GetComponent<Image>().color = tempPanelColor;
                }
                
                Color tempTextColor = text.GetComponent<Text>().color;
                tempTextColor.a += 0.05f;
                text.GetComponent<Text>().color = tempTextColor;

                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                yield return new WaitForSeconds(tutorialDuration);

                break;
            }
        }
        while (true)
        {
            if (text.GetComponent<Text>().color.a >= 0)
            {
                Color tempPanelColor = panel.GetComponent<Image>().color;
                tempPanelColor.a -= 0.05f;
                panel.GetComponent<Image>().color = tempPanelColor;

                Color tempTextColor = text.GetComponent<Text>().color;
                tempTextColor.a -= 0.05f;
                text.GetComponent<Text>().color = tempTextColor;

                yield return new WaitForSeconds(0.05f);
            }
            else if(button.transform.localPosition.y < m_buttonPositionY + 200)
            {
                button.transform.localPosition += new Vector3(0, 5, 0);
                yield return new WaitForSeconds(0.005f);
            }
            else
            {
                yield break;
            }
        }
    }

    public void CloseTutorial()
    {
        m_tutorialOpen = false;
    }

    private bool m_tutorialOpen = true;
    private float m_buttonPositionY;
    private float m_panelOpacity;
    private AudioSource m_audioSource;
    private bool m_played = false;

}
