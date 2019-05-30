using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The main class responsible for running the game and coordinating all of it's particular parts

public class GameController : MonoBehaviour
{
    //Assigning all our public variables

    public GameObject Worm;
    public GameObject Sun;
    public float terrainSpeed = 0.5f; // the speed at which the terrain moves around us
    public float speedStep = 0.00001f; // the step by which the terrain speed is changed
    public float maxTerrainSpeed = 1f; // max speed the terrain wil reach
    public int terrainLength;
    public int terrainWidth;
    public Material[] materials;
    public GameObject diedText;
    public GameObject starvedText;
    public GameObject levelFinishedCanvas;

    public static bool daytime; // a static variable allowing globally to check whether it is currently day
    public static bool premptiveDayTime; // a pre check for the day to allow proper headlight scripting
    void Start()
    {
        Application.targetFrameRate = 60; // Locking the max framerate to 60 to preserve battery life (not many phones have displays that go over 60 FPS)

        if (platforms == null)
        {
            platforms = GameObject.FindGameObjectsWithTag("Platform"); // Creating a list of all terrain blocks
        }
        ScatterTerrain(); // Changes block color and height at game start
        // assigning all required controllers to variables
        m_wormController = Worm.GetComponent<WormController>();
        m_lightController = Sun.GetComponent<LightController>();
        m_levelController = FindObjectOfType<LevelController>();
        // grabing the audio source
        m_audioSource = GetComponent<AudioSource>();

        daytime = m_lightController.GetSunUp();
        premptiveDayTime = m_lightController.GetPremptiveSunUp();
    }

    void Update()
    {
        if (m_gameRunning) // checks if game is not stoped
        {
            // Moving and creating all the platforms responsible for our game terrain
            foreach (GameObject platform in platforms)
            {
                GoForward(platform); // move all platforms forward
                if (platform.transform.position.z < -10)
                {
                    RegenerateTerrain(platform); // take all passed blocks and move them to the front
                }
                if (platform.transform.position.x > (terrainWidth/ 2))
                {
                    TeleportTerrain(platform, true); // moving blocks that we moved away from turning
                }
                else if (platform.transform.position.x < (-terrainWidth/ 2))
                {
                    TeleportTerrain(platform, false); // moving blocks that we moved away from turning
                }
                if (platform.GetComponent<Platform>().GetHitWorm() && m_wormController.GetCharging())
                {
                    DestroyRock(platform); // destroy rock if it was charged through by the worm
                }
                else if (platform.GetComponent<Platform>().GetHitWorm())
                {
                    KillWorm(); // kill worm if it hit a rock 
                }
                if (platform.GetComponent<Platform>().GetDroppedWorm())
                {
                    DropWorm(); // drop worm if it walks onto a hole
                }
                if (m_wormController.GetFoodStatus() < 0)
                {
                    StarveWorm(); // kill worm if it runs out of food
                }
                daytime = m_lightController.GetSunUp(); // update day status
                premptiveDayTime = m_lightController.GetPremptiveSunUp();

                if (terrainSpeed < maxTerrainSpeed)
                {
                    terrainSpeed += speedStep; // speed game up
                }
            }
        }
    }
    // Stops the game and kills our worm after it hits a rock
    private void KillWorm()
    {
        m_wormController.Die();
        diedText.SetActive(true);
        m_gameRunning = false;
    }
    // Stops the game and drops our worm after it hits a hole
    private void DropWorm()
    {
        m_wormController.Drop();
        diedText.SetActive(true);
        m_gameRunning = false;
    }
    // Stops the game and kills our worm when it starves
    private void StarveWorm()
    {
        m_wormController.Starve();
        starvedText.SetActive(true);
        m_gameRunning = false;
    }
    // Called when our worm charges through a rock
    private void DestroyRock(GameObject Rock)
    {
        RandomizeAudioPitch();
        m_audioSource.PlayOneShot(m_audioSource.clip);
        Rock.GetComponent<Platform>().DestroyRock();
    }
    // Used to move platforms backwards creating our terrain movement
    public void GoForward(GameObject platform)
    {
            platform.transform.Translate(0, 0, -terrainSpeed);
    }
    // Called when our worm goes left
    public void GoLeft()
    {
        if (m_wormController.GetRunning() && !m_turning)
        {
            m_turning = true;
            foreach (GameObject platform in platforms)
            {
                IEnumerator coroutine = GoLeftCoroutine(platform);
                StartCoroutine(coroutine);
            }
            StartCoroutine("TurningCooldownCoroutine");
        }
    }
    // Coroutine responsible for the turning animation together with the movement of the platforms when moving left
    private IEnumerator GoLeftCoroutine(GameObject platform)
    {
        Vector3 startPosition = platform.transform.position;
        Vector3 movementVector = new Vector3(0.5f, 0, 0);
        float t = 0;
        while (t < m_turnDuration - 0.01f)
        {
            t += 0.01f;
            platform.transform.position += movementVector;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
    // Called when our worm goes right
    public void GoRight()
    {
        if (m_wormController.GetRunning() && !m_turning)
        {
            m_turning = true;
            foreach (GameObject platform in platforms)
            {
                IEnumerator coroutine = GoRightCoroutine(platform);
                StartCoroutine(coroutine);
            }
            StartCoroutine("TurningCooldownCoroutine");
        }
    }
    // Coroutine responsible for the turning animation together with the movement of the platforms when moving right
    private IEnumerator GoRightCoroutine(GameObject platform)
    {
        Vector3 startPosition = platform.transform.position;
        Vector3 movementVector = new Vector3(-0.5f, 0, 0);
        float t = 0;
        while (t < m_turnDuration - 0.01f)
        {
            t += 0.01f;
            platform.transform.position += movementVector;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
    // Used to recycle terrain blocks that have been passed to create new terrain
    private void RegenerateTerrain(GameObject platform)
    {
        platform.GetComponent<Platform>().CheckIfRockOrHole();
        platform.transform.Translate(0, 0, terrainLength);
        platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, - 2.5f + Random.Range(-0.3f,0.3f), platform.transform.localPosition.z);

    }
    // Responsible for terrain movement to the sides
    private void TeleportTerrain(GameObject platform, bool right)
    {
        if(right)
        {
            platform.transform.Translate(-platform.transform.position.x - terrainWidth/2.0f, 0, 0);
        }
        else
        {
            platform.transform.Translate(-platform.transform.position.x + terrainWidth/2.0f, 0, 0);
        }
    }

    private IEnumerator TurningCooldownCoroutine()
    {
        yield return new WaitForSeconds(m_turnDuration);
        m_turning = false;
    }
    // Makes terrain bocks uneven and randomizes their color
    private void ScatterTerrain()
    {
        foreach (GameObject platform in platforms)
        {
            platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, -2.5f + Random.Range(-0.3f, 0.3f), platform.transform.localPosition.z);
            platform.GetComponent<Platform>().RandomGrassColor();
        }
    }
    // Called after the level duration time has elapsed to signify it's end
    public void EndLevel()
    {
        Debug.Log("Level finished");
        m_gameRunning = false;
        levelFinishedCanvas.SetActive(true);
    }

    public void RandomizeAudioPitch()
    {
        m_audioSource.pitch = 1 + Random.Range(-0.2f, 0.2f);
    }

    public bool GetGameRunning()
    {
        return m_gameRunning;
    }

    private GameObject[] platforms;
    private float m_turnDuration = 0.1f;
    private WormController m_wormController;
    private LightController m_lightController;
    private LevelController m_levelController;
    private bool m_gameRunning = true;
    private AudioSource m_audioSource;
    private bool m_turning = false;
}
