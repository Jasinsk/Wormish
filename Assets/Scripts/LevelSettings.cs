using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public int currentLevel = 1;
    void Start()
    {
        SetEndless();   
    }
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
    public void SetLevelOne()
    {
        m_dayCycle = false;
        m_starvation = false;
        m_Holes = true;
        m_daySet = true;
        m_levelDuration = 10;
    }
    public void SetLevelTwo()
    {
        m_dayCycle = false;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_levelDuration = 10;
    }
    public void SetLevelThree()
    {
        m_dayCycle = false;
        m_starvation = true;
        m_Holes = true;
        m_daySet = false;
        m_levelDuration = 10;
    }
    public void SetLevelFour()
    {
        m_dayCycle = true;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
        m_levelDuration = 10;
    }
    public void SetEndless()
    {
        m_dayCycle = true;
        m_starvation = true;
        m_Holes = true;
        m_daySet = true;
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
    private bool m_dayCycle, m_starvation, m_Holes = false;
    private bool m_daySet = true;
    private int m_levelDuration;
    private static bool wasCreated = false;    
}
