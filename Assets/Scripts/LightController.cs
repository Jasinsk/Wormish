using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float rotationAngle = 1;

    void Start()
    {
        m_rotator = new Vector3(0, -rotationAngle, 0);
        m_light = GetComponent<Light>();
    }

    void Update()
    {
        transform.Rotate(m_rotator, Space.Self);
        if (transform.rotation.eulerAngles.y > 90 && transform.rotation.eulerAngles.y < 270)
        {
            m_light.enabled = false;
            m_sunUp = false;
        }
        else if (transform.rotation.eulerAngles.y > 360)
        {
            transform.Rotate(m_backrotator, Space.Self);
        }
        else
        {
            m_light.enabled = true;
            m_sunUp = true;
        }
        if (transform.rotation.eulerAngles.y > 70 && transform.rotation.eulerAngles.y < 290)
        {
            m_premptiveSunUp = false;
        }
        else
        {
            m_premptiveSunUp = true;
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
    private bool m_sunUp;
    private bool m_premptiveSunUp;
}
