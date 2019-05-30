using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public GameObject unmuted;
    public GameObject muted;
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
        m_audioListener = GetComponent<AudioListener>();

        if(!settings.GetAudioOn())
        {
            m_playing = false;
            unmuted.SetActive(false);
            muted.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioMute()
    {
        if (m_playing)
        {
            m_playing = false;

            unmuted.SetActive(false);
            muted.SetActive(true);

            settings.SetAudioOn(false);
            AudioListener.volume = 0;
        }
        else
        {
            m_playing = true;

            unmuted.SetActive(true);
            muted.SetActive(false);

            settings.SetAudioOn(true);
            AudioListener.volume = 1;
        }
    }

    private bool m_playing = true;
    private LevelSettings settings;
    private AudioListener m_audioListener;
}
