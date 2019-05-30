using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public float storyDuration = 5;
    public GameObject panel;
    public GameObject text;
    public GameObject button;
    public string story_1;
    public string story_2;
    public string story_3;
    public string story_4;
    public string story_5;

    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();

        m_audioSource = GetComponent<AudioSource>();

        m_buttonPositionY = button.transform.localPosition.y;
        m_panelOpacity = panel.GetComponent<Image>().color.a;

        button.transform.localPosition += new Vector3(0, 200, 0);
        StartCoroutine("StoryWindowCoroutine");

        Color tempPanelColor = panel.GetComponent<Image>().color;
        tempPanelColor.a = 0;
        panel.GetComponent<Image>().color = tempPanelColor;

        Color tempTextColor = text.GetComponent<Text>().color;
        tempTextColor.a = 0;
        text.GetComponent<Text>().color = tempTextColor;

        AssignStoryText();
    }

    void Update()
    {
        
    }

    public void AssignStoryText()
    {
        switch(settings.currentLevel)
        {
            case 1:
                text.GetComponent<Text>().text = story_1;
                break;
            case 2:
                text.GetComponent<Text>().text = story_2;
                break;
            case 3:
                text.GetComponent<Text>().text = story_3;
                break;
            case 4:
                text.GetComponent<Text>().text = story_4;
                break;
            case 5:
                text.GetComponent<Text>().text = story_5;
                break;
        }
    }
    private IEnumerator StoryWindowCoroutine()
    {
        while (m_storyOpen)
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
                yield return new WaitForSeconds(storyDuration);

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

    public void CloseStory()
    {
        m_storyOpen = false;
    }

    private bool m_storyOpen = true;
    private float m_buttonPositionY;
    private float m_panelOpacity;
    private AudioSource m_audioSource;
    private bool m_played = false;
    private LevelSettings settings;

}
