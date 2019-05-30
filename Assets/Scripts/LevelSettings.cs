using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for setting up every level and changing the settings between them
public class LevelSettings : MonoBehaviour
{
    public int currentLevel = 1;
    void Start()
    {
        SetEndless();   
    }
    // Making sure only one instance of LevelSettings is present on the scene
    void Awake()
    {
        if (!wasCreated)
        {
            DontDestroyOnLoad(gameObject); // making this object not destroyable upon scene changes
            wasCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Setting up all the particular level settings
    public void SetLevelOne()
    {
        m_dayCycle = false; // Does this level have a day/night cycle?
        m_starvation = false; // Does the starvation mechanic work on this level? 
        m_Holes = true; // Do holes show up?
        m_daySet = true; // is it day or night at the start of the level?
        m_timeLimit = true; // is their a time duration of the level?
        m_levelDuration = 50; // setting the level duration
    }
    public void SetLevelTwo()
    {
        m_dayCycle = false;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = true;
        m_levelDuration = 70;
    }
    public void SetLevelThree()
    {
        m_dayCycle = false;
        m_starvation = true;
        m_Holes = true;
        m_daySet = false;
        m_timeLimit = true;
        m_levelDuration = 70;
    }
    public void SetLevelFour()
    {
        m_dayCycle = true;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = true;
        m_levelDuration = 100;
    }
    public void SetEndless()
    {
        m_dayCycle = true;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = false;
    }

    public void SetLevelReplay(bool reset) // 
    {
        m_levelReplay = reset;
    }
    public void SetAudioOn(bool value)
    {
        m_AudioOn = value;
    }

    public bool GetDayCycleSet()
    {
        return m_dayCycle;
    }
    public bool GetDaySet()
    {
        return m_daySet;
    }
    public bool GetStarvationSet()
    {
        return m_starvation;
    }
    public bool GetHolesSet()
    {
        return m_Holes;
    }
    public float GetLevelDuration()
    {
        return m_levelDuration;
    }
    public bool GetTimeLimit()
    {
        return m_timeLimit;
    }
    public bool GetLevelReplay()
    {
        return m_levelReplay;
    }
    public bool GetAudioOn()
    {
        return m_AudioOn;
    }
    private bool m_dayCycle, m_starvation, m_Holes = false;
    private bool m_daySet = true;
    private int m_levelDuration;
    private static bool wasCreated = false;
    private bool m_timeLimit = true;
    private bool m_levelReplay = false;
    private bool m_AudioOn = true;
}
