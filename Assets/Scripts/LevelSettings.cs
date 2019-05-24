using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    void Start()
    {
        SetEndlessSet();   
    }
    void Update()
    {
        
    }

    public void SetEndlessSet()
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
    private bool m_dayCycle, m_starvation, m_Holes = false;
    private bool m_daySet = true;
    
}
