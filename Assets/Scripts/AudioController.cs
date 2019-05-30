using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for the mute button on the menu screen
public class AudioController : MonoBehaviour
{
    public GameObject unmuted;
    public GameObject muted;
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>(); // getting the global setting object

        if(!settings.GetAudioOn()) // making sure the current state is in sync with the settings
        {
            m_playing = false;
            unmuted.SetActive(false);
            muted.SetActive(true);
        }
    }
    // Called upon mute button press to change the audio mute state
    public void AudioMute() 
    {
        if (m_playing)
        {
            m_playing = false;

            unmuted.SetActive(false); // changing the button icon
            muted.SetActive(true);

            settings.SetAudioOn(false);
            AudioListener.volume = 0; // turning off the game sound
        }
        else
        {
            m_playing = true;

            unmuted.SetActive(true); // changes the button icon
            muted.SetActive(false);

            settings.SetAudioOn(true);
            AudioListener.volume = 1; // turning on the game sound
        }
    }

    private bool m_playing = true;
    private LevelSettings settings;
}
