using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for controlling all acion pertaining to our worm
public class WormController : MonoBehaviour
{
    //assigning public variables
    public float wormScaleMax = 5; // maximum stretch of worm during running
    public float wormScaleMin = 2; // minum stretch
    public Color starterColor; //begining color of the worm
    public GameObject nightlight; // light object that illuminates during the night
    public AudioClip deathSqueak; // audio clip that is used upon worm death

    void Start()
    {
        // assigning all required variables
        m_audioSource = GetComponent<AudioSource>(); 
        m_material = GetComponent<Renderer>().material; // exposing material to allow for dynamic color changing
        m_material.color = starterColor;
        m_rigidbody = GetComponent<Rigidbody>(); // rigidbody used to turn on gravity upon fall
        m_nightlight = nightlight.GetComponent<Light>();
        settings = GameObject.FindGameObjectWithTag("LevelSettings").GetComponent<LevelSettings>(); // finding the settings object on the scene

        if (!settings.GetDayCycleSet()) // checking the day/night setting
        {
            m_nightlight.enabled = false;
        }
    }

    void Update()
    {
        if (m_running)
        {
            if (m_enlarging) // streching part of the running worm animation
            {
                if (transform.localScale.z < wormScaleMax)
                {
                    transform.localScale += new Vector3(-0.01f, -0.01f, 0.15f); // making the worm slightly longer
                }
                else
                {
                    m_enlarging = false;
                }
            }
            else // contracting part of the worm running animation
            {
                if (transform.localScale.z > wormScaleMin)
                {
                    transform.localScale += new Vector3(0.01f, 0.01f, -0.15f); // making the worm slightly shorter
                }
                else
                {
                    m_enlarging = true;
                }
            }
            if (transform.localScale.z > 2.99 && transform.localScale.z < 3.01) // reseting worm size to guarantee consistant size
            {
                transform.localScale = new Vector3(0.7f, 0.7f, 3);
            }
            m_material.color = Color.Lerp(starterColor, Color.white, 1 - m_foodStatus / 1000f); // changing worm color together with the fall of the worms food status

            if (GameController.premptiveDayTime && settings.GetStarvationSet())
                {
                m_foodStatus -= 1; // lowering food status
                if (m_nightlight.enabled) // turning off the headlight when day comes around
                {
                    StartCoroutine("LightFadeOutCoroutine"); // fading out the light cocurrently
                }
            }
            else
            {
                m_nightlight.intensity -= 0.001f; // lowering headlight intensity
                if (!m_nightlight.enabled && (settings.GetDayCycleSet() || !settings.GetDaySet())) // turning on the headlight upon nighttime
                {
                   StartCoroutine("LightFadeInCoroutine"); // fading in the light cocurrently
                }
            }
            
        }
    }
    // Fades in our headlight
    private IEnumerator LightFadeInCoroutine()
    {
        float temp_intensity = m_nightlight.intensity;
        m_nightlight.intensity = 0;
        m_nightlight.enabled = true;
        while (m_nightlight.intensity < temp_intensity) // raising the intensity until it reaches the set maximum
        {
            m_nightlight.intensity += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        m_nightlight.intensity = temp_intensity;
    }
    // Fades out our headlight
    private IEnumerator LightFadeOutCoroutine()
    {
        float temp_intensity = m_nightlight.intensity;
        while (m_nightlight.intensity > 0) // lowering the intensity until it fades out
        {
            m_nightlight.intensity -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        m_nightlight.enabled = false;
        m_nightlight.intensity = temp_intensity;
    }
     // Replenishes food meter
    public void FeedMe()
    {
        m_foodStatus = 1000;
    }
    // Replenishes the headlight intensity
    public void LightMeUp()
    {
        m_nightlight.intensity = 0.9f;
    }
    // kills the worm
    public void Die()
    {
        m_audioSource.PlayOneShot(deathSqueak); // plays death sound
        m_material.color = Color.red; // changes worm color
        m_running = false; // changes the variable signifying that the game is no longer playing
    }
    // kills our orm through starvation
    public void Starve()
    {
        m_audioSource.PlayOneShot(deathSqueak); // plays death sound
        m_material.color = Color.white; // changes worm color
        m_running = false; // changes the variable signifying that the game is no longer playing
    }
    // kills our worm through droping
    public void Drop()
    {
        m_audioSource.PlayOneShot(deathSqueak);
        m_running = false;
        StopAllCoroutines(); // stops all cocurrent processes
        StartCoroutine("PanicAnimation"); // plays the panic animation that ends with the worm falling down the hole
    }
    // Plays the animation seen before our worm falls into a hole
    private IEnumerator PanicAnimation()
    {
        int t = 40;
        while (t > 0)
        {
            if (m_enlarging) // plays the flailing animation by stretching and contracting the worm
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
        m_rigidbody.useGravity = true; // turns on gravity for our worm to make it fall
        m_rigidbody.isKinematic = false;
        yield break;
    }
    // called when the worm starts charging
    public void Charge()
    {
        if (m_running)
        {
            m_charging = true; 
            StartCoroutine("ChargingCoroutine");
        }
    }
    // Plays charing animation
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
        m_charging = false;
        yield break;
    }
    // rolls the worm left
    public void RollLeft()
    {
        if (m_running)
        {
            RandomizeAudioPitch(); // randomizes sound pitch
            m_audioSource.PlayOneShot(m_audioSource.clip); // plays rolling sound
            StartCoroutine("RollLeftCoroutine");
        }
    }
    // cocurrently plays the rolling animation
    private IEnumerator RollLeftCoroutine()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -180);

        float t = 0;
        while (t < m_rotationDuration)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t / m_rotationDuration); // rotating the worm over time
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, 1);
        yield break;
    }
    // rolls the worm right
    public void RollRight()
    {
        if (m_running)
        {
            RandomizeAudioPitch(); // randomizes sound pitch
            m_audioSource.PlayOneShot(m_audioSource.clip); // plays roll sound
            StartCoroutine("RollRightCoroutine");
        }
    }
    // plays the rotating right animation
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
    // randomizes the pitch of our audio source. Used to avoid repetetive identical sounds playing multiple times
    public void RandomizeAudioPitch()
    {
        m_audioSource.pitch = 1 + Random.Range(-0.2f, 0.2f);
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
    private float m_rotationDuration = 0.2f; // the duration of the rotation animations
    private Material m_material;
    private Rigidbody m_rigidbody;
    private Light m_nightlight;
    private bool m_charging = false;
    private int m_foodStatus = 1000;
    private LevelSettings settings;
    private AudioSource m_audioSource;
}
