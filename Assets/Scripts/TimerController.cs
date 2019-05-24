using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
        m_gameController = GetComponentInParent<GameController>();
        m_TimeLeft = settings.GetLevelDuration();
    }
    void Update()
    {
        if (m_gameController.GetGameRunning())
        {
            m_TimeLeft -= Time.deltaTime;
            if (m_TimeLeft < 0)
            {
                m_gameController.EndLevel();
            }
        }
    }

    private LevelSettings settings;
    private float m_TimeLeft;
    private GameController m_gameController;
}
