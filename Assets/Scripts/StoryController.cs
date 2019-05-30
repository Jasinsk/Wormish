using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    public float storyDuration = 5; // the duration of the message
    public GameObject panel;
    public GameObject text;
    public GameObject button;
    // the texts of the messages that are assigned to each level
    public string story_1;
    public string story_2;
    public string story_3;
    public string story_4;
    public string story_5;

    //The class responsible for the level starting messages
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>(); // get the settings object

        m_audioSource = GetComponent<AudioSource>();

        m_buttonPositionY = button.transform.localPosition.y;
        m_panelOpacity = panel.GetComponent<Image>().color.a;

        button.transform.localPosition += new Vector3(0, 200, 0);
        if (!settings.GetLevelReplay()) // stops the message from showing when level is being replayed
        {
            StartCoroutine("StoryWindowCoroutine");
        }

        // variables used to allow the fading in and out of the level messages
        Color tempPanelColor = panel.GetComponent<Image>().color;
        tempPanelColor.a = 0;
        panel.GetComponent<Image>().color = tempPanelColor;

        Color tempTextColor = text.GetComponent<Text>().color;
        
        if (!settings.GetDaySet())
        {
            m_panelOpacity = 0.9f;
            tempTextColor = Color.black;
        }

        tempTextColor.a = 0;
        text.GetComponent<Text>().color = tempTextColor;

        AssignStoryText(); // used to change the message text between levels
    }

    public void AssignStoryText()
    {
        switch(settings.currentLevel) // changes message text to that required on the current level
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
    // playes the animation of the lowering bubble, and fading message window
    private IEnumerator StoryWindowCoroutine()
    {
        while (m_storyOpen)
        {
            if (button.transform.localPosition.y > m_buttonPositionY) // lowers the button
            {
                button.transform.localPosition -= new Vector3(0, 5, 0);
                yield return new WaitForSeconds(0.005f);
            }
            else if (text.GetComponent<Text>().color.a < 0.9) // fades in the text box
            {
                if (!m_played)
                {
                    m_audioSource.PlayOneShot(m_audioSource.clip); // plays sound
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
                yield return new WaitForSeconds(storyDuration); // waits for the given amount of time

                break;
            }
        }
        while (true) // fades the message out and lifts the button
        {
            if (text.GetComponent<Text>().color.a >= 0) // fade out
            {
                Color tempPanelColor = panel.GetComponent<Image>().color;
                tempPanelColor.a -= 0.05f;
                panel.GetComponent<Image>().color = tempPanelColor;

                Color tempTextColor = text.GetComponent<Text>().color;
                tempTextColor.a -= 0.05f;
                text.GetComponent<Text>().color = tempTextColor;

                yield return new WaitForSeconds(0.05f);
            }
            else if(button.transform.localPosition.y < m_buttonPositionY + 200) // rising button
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
    // used when button is pressed to close the message earlier 
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
