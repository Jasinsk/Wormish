using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for creation of the in-game day-night cycle 
public class LightController : MonoBehaviour
{
    public float rotationAngle = 1;

    void Start()
    {
        m_rotator = new Vector3(0, -rotationAngle, 0); // object used to rotate our lights
        m_light = GetComponent<Light>();
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>();
        if (!settings.GetDayCycleSet()) // check whether this day has a day/nihgt cycle
        {
            m_premptiveSunUp = settings.GetDaySet();
            m_sunUp = settings.GetDaySet();
        }
        if (!settings.GetDaySet()) //check if this level starts at day or night
        {
            transform.Rotate(m_switcher, Space.Self);
        }
    }

    void Update()
    {
        if (settings.GetDayCycleSet())
        {
            transform.Rotate(m_rotator, Space.Self); //rotate the light
            if (transform.rotation.eulerAngles.y > 90 && transform.rotation.eulerAngles.y < 270) // check whether it is night
            {
                m_light.enabled = false;
                m_sunUp = false;
            }
            else if (transform.rotation.eulerAngles.y > 360) // return the object to the original rotation after 360 degrees of rotation
            {
                transform.Rotate(m_backrotator, Space.Self);
            }
            else
            {
                m_light.enabled = true;
                m_sunUp = true;
            }
            if (transform.rotation.eulerAngles.y > 70 && transform.rotation.eulerAngles.y < 290) // checks whether night is starting to fall
            {
                m_premptiveSunUp = false;
            }
            else
            {
                m_premptiveSunUp = true;
            }
        }
    }

    public bool GetSunUp()
    {
        return m_sunUp;
    }

    public bool GetPremptiveSunUp()
    {
        return m_premptiveSunUp;
    }

    private Vector3 m_rotator;
    private Light m_light;
    private Vector3 m_backrotator = new Vector3(0, 360, 0);
    private Vector3 m_switcher = new Vector3(0, 180, 0);
    private bool m_sunUp = false;
    private bool m_premptiveSunUp = false;
    private LevelSettings settings;
}
