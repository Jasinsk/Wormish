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
            DontDestroyOnLoad(gameObject);
            wasCreated = true;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        
    }
    // Setting up all the particular level settings
    public void SetLevelOne()
    {
        m_dayCycle = false;
        m_starvation = false;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = true;
        m_levelDuration = 100;
    }
    public void SetLevelTwo()
    {
        m_dayCycle = false;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = true;
        m_levelDuration = 150;
    }
    public void SetLevelThree()
    {
        m_dayCycle = false;
        m_starvation = true;
        m_Holes = true;
        m_daySet = false;
        m_timeLimit = true;
        m_levelDuration = 150;
    }
    public void SetLevelFour()
    {
        m_dayCycle = true;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = true;
        m_levelDuration = 200;
    }
    public void SetEndless()
    {
        m_dayCycle = true;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_timeLimit = false;
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
    private bool m_dayCycle, m_starvation, m_Holes = false;
    private bool m_daySet = true;
    private int m_levelDuration;
    private static bool wasCreated = false;
    private bool m_timeLimit = true;
}
