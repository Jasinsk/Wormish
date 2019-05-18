using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float rotationAngle = 1;

    void Start()
    {
        m_rotator = new Vector3(0, rotationAngle, 0);
        m_light = GetComponent<Light>();
    }

    void Update()
    {
        transform.Rotate(m_rotator, Space.Self);
        //if (transform.rotation.y > 90 && transform.rotation.y < 270)
        //{
        //    m_light.enabled = false;
        //}
        //else
        //{
        //    m_light.enabled = true;
        //}
        //Debug.Log(transform.rotation.y);
        
    }

    private Vector3 m_rotator;
    private Light m_light;
}
