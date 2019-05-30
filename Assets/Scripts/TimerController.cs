using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// the class responsible for the level duration and progress circle in the game
public class TimerController : MonoBehaviour
{
    // The class responsible for controlling game time duration and level ending

    public GameObject ProgressIndicator;
    public GameObject ProgressIndicatorBackground;
    void Start()
    {
        // load all required values from the game settings
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
        m_gameController = GetComponentInParent<GameController>();
        m_TimeLeft = settings.GetLevelDuration();
        m_indicatorTransform = ProgressIndicator.GetComponent<RectTransform>();
        m_maxScale = m_indicatorTransform.localScale.x;

        if (!settings.GetTimeLimit()) // if the level has the time limit disabled remove the indicator
        {
            ProgressIndicator.SetActive(false);
            ProgressIndicatorBackground.SetActive(false);
        }
    }
    void Update()
    {
        if (m_gameController.GetGameRunning() && settings.GetTimeLimit()) // make indicator fuller
        {
            m_TimeLeft -= Time.deltaTime; // with each step lower the time left variable value
            m_scaler = m_maxScale * ((settings.GetLevelDuration() - m_TimeLeft) / settings.GetLevelDuration()); // adjust the circle fill according to the time left
            m_indicatorTransform.localScale = new Vector3(m_scaler, m_scaler, 1);
            if (m_TimeLeft < 0) // end level when time runs out
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
