using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    // The class responsible for controlling game time duration and level ending

    public GameObject ProgressIndicator;
    public GameObject ProgressIndicatorBackground;
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
        m_gameController = GetComponentInParent<GameController>();
        m_TimeLeft = settings.GetLevelDuration();
        m_indicatorTransform = ProgressIndicator.GetComponent<RectTransform>();
        m_maxScale = m_indicatorTransform.localScale.x;

        if (!settings.GetTimeLimit())
        {
            ProgressIndicator.SetActive(false);
            ProgressIndicatorBackground.SetActive(false);
        }
    }
    void Update()
    {
        if (m_gameController.GetGameRunning() && settings.GetTimeLimit())
        {
            m_TimeLeft -= Time.deltaTime;
            m_scaler = m_maxScale * ((settings.GetLevelDuration() - m_TimeLeft) / settings.GetLevelDuration());
            m_indicatorTransform.localScale = new Vector3(m_scaler, m_scaler, 1);
            if (m_TimeLeft < 0)
            {
                m_gameController.EndLevel();
            }
        }
    }

    private LevelSettings settings;
    private float m_TimeLeft, m_maxScale, m_scaler;
    private RectTransform m_indicatorTransform;
    private GameController m_gameController;
}
