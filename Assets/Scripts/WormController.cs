using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    public float wormScaleMax = 5;
    public float wormScaleMin = 2;
    public Color starterColor;
    public GameObject nightlight;

    void Start()
    {
        m_material = GetComponent<Renderer>().material;
        m_material.color = starterColor;
        m_rigidbody = GetComponent<Rigidbody>();
        m_nightlight = nightlight.GetComponent<Light>();
    }

    void Update()
    {
        if (m_running)
        {
            if (m_enlarging)
            {
                if (transform.localScale.z < wormScaleMax)
                {
                    transform.localScale += new Vector3(-0.01f, -0.01f, 0.15f);
                }
                else
                {
                    m_enlarging = false;
                }
            }
            else
            {
                if (transform.localScale.z > wormScaleMin)
                {
                    transform.localScale += new Vector3(0.01f, 0.01f, -0.15f);
                }
                else
                {
                    m_enlarging = true;
                }
            }
            if (transform.localScale.z > 2.99 && transform.localScale.z < 3.01)
            {
                transform.localScale = new Vector3(0.7f, 0.7f, 3);
            }
            m_material.color = Color.Lerp(starterColor, Color.white, 1 - m_foodStatus / 1000f);

            if (GameController.premptiveDayTime)
            {
                m_foodStatus -= 1;
                if (m_nightlight.enabled)
                {
                    StartCoroutine("LightFadeOutCoroutine");
                }
            }
            else
            {
                m_nightlight.intensity -= 0.001f;
                if (!m_nightlight.enabled)
                {
                    StartCoroutine("LightFadeInCoroutine");
                }
            }
            
        }
    }

    private IEnumerator LightFadeInCoroutine()
    {
        float temp_intensity = m_nightlight.intensity;
        m_nightlight.intensity = 0;
        m_nightlight.enabled = true;
        while (m_nightlight.intensity < temp_intensity)
        {
            m_nightlight.intensity += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        m_nightlight.intensity = temp_intensity;
    }

    private IEnumerator LightFadeOutCoroutine()
    {
        float temp_intensity = m_nightlight.intensity;
        while (m_nightlight.intensity > 0)
        {
            m_nightlight.intensity -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        m_nightlight.enabled = false;
        m_nightlight.intensity = temp_intensity;
    }
     
    public void FeedMe()
    {
        m_foodStatus = 1000;
    }

    public void LightMeUp()
    {
        m_nightlight.intensity = 0.9f;
    }
    public void Die()
    {
        m_material.color = Color.red;
        m_running = false;
    }

    public void Starve()
    {
        m_material.color = Color.white;
        m_running = false;
    }

    public void Drop()
    {
        m_running = false;
        StopAllCoroutines();
        StartCoroutine("PanicAnimation");
        
    }
    private IEnumerator PanicAnimation()
    {
        int t = 40;
        while (t > 0)
        {
            if (m_enlarging)
            {
                if (transform.localScale.z < wormScaleMax)
                {
                    transform.localScale += new Vector3(0, 0, 0.7f);
                    t -= 1;
                    yield return new WaitForSeconds(0.005f);
                }
                else
                {
                    m_enlarging = false;
                }
            }
            else
            {
                if (transform.localScale.z > wormScaleMin)
                {
                    transform.localScale += new Vector3(0, 0, -0.7f);
                    t -= 1;
                    yield return new WaitForSeconds(0.005f);
                }
                else
                {
                    m_enlarging = true;
                }
            }
        }
        m_rigidbody.useGravity = true;
        m_rigidbody.isKinematic = false;
        yield break;
    }
    public void Charge()
    {
        if (m_running)
        {
            m_charging = true;
            StartCoroutine("ChargingCoroutine");
        }
    }

    private IEnumerator ChargingCoroutine()
    {
        float m_currentLength = transform.localScale.z;
        bool tmp_running = m_running;
        m_running = false;
        while (true)
        {
                if (transform.localScale.z < wormScaleMax * 1.2f)
                {
                    transform.localPosition += new Vector3(0, 0, 0.5f);
                    transform.localScale += new Vector3(0, 0, 0.7f);
                    yield return new WaitForSeconds(0.005f);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
        }

        while (transform.localScale.z > m_currentLength)
        {
            if (transform.localPosition.z > 0)
            {
                transform.localPosition -= new Vector3(0, 0, 0.5f);
            }
            transform.localScale += new Vector3(0, 0, -0.3f);
            yield return new WaitForSeconds(0.005f);
        }

        transform.localPosition = new Vector3(0, 0.5f, 0);
        m_running = tmp_running;
        //yield return new WaitForSeconds(0.5f);
        m_charging = false;
        yield break;
    }
    public void RollLeft()
    {
        if (m_running)
        {
            StartCoroutine("RollLeftCoroutine");
        }
    }
    private IEnumerator RollLeftCoroutine()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -180);

        float t = 0;
        while (t < m_rotationDuration)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t / m_rotationDuration);
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, 1);
        yield break;
    }
    public void RollRight()
    {
        if (m_running)
        {
            StartCoroutine("RollRightCoroutine");
        }
    }

    private IEnumerator RollRightCoroutine()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 180);

        float t = 0;
        while (t < m_rotationDuration)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t / m_rotationDuration);
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, 1);
        yield break;
    }

    public bool GetRunning()
    {
        return m_running;
    }
    public bool GetCharging()
    {
        return m_charging;
    }
    public int GetFoodStatus()
    {
        return m_foodStatus;
    }

    private bool m_running = true;
    private bool m_enlarging = true;
    private float m_rotationDuration = 0.2f;
    private Material m_material;
    private Rigidbody m_rigidbody;
    private Light m_nightlight;
    private bool m_charging = false;
    private int m_foodStatus = 1000;
}
