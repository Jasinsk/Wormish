using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Worm;
    public GameObject Sun;
    public float terrainSpeed = 0.5f;
    public int terrainLength;
    public int terrainWidth;
    public Material[] materials;

    public static bool daytime;
    public static bool premptiveDayTime;
    void Start()
    {
        Application.targetFrameRate = 60;

        if (platforms == null)
        {
            platforms = GameObject.FindGameObjectsWithTag("Platform");
        }
        m_wormController = Worm.GetComponent<WormController>();
        m_lightController = Sun.GetComponent<LightController>();

        daytime = m_lightController.GetSunUp();
        premptiveDayTime = m_lightController.GetPremptiveSunUp();
    }

    void Update()
    {
        if (m_gameRunning)
        {
            foreach (GameObject platform in platforms)
            {
                GoForward(platform);
                if (platform.transform.position.z < -10)
                {
                    RegenerateTerrain(platform);
                }
                if (platform.transform.position.x > (terrainWidth/ 2))
                {
                    TeleportTerrain(platform, true);
                }
                else if (platform.transform.position.x < (-terrainWidth/ 2))
                {
                    TeleportTerrain(platform, false);
                }
                if (platform.GetComponent<Platform>().GetHitWorm() && m_wormController.GetCharging())
                {
                    DestroyRock(platform);
                }
                else if (platform.GetComponent<Platform>().GetHitWorm())
                {
                    KillWorm();
                }
                if (platform.GetComponent<Platform>().GetDroppedWorm())
                {
                    DropWorm();
                }
                if (m_wormController.GetFoodStatus() < 0)
                {
                    StarveWorm();
                }
                daytime = m_lightController.GetSunUp();
                premptiveDayTime = m_lightController.GetPremptiveSunUp();
            }
        }
    }

    private void KillWorm()
    {
        m_wormController.Die();
        m_gameRunning = false;
    }
    private void DropWorm()
    {
        m_wormController.Drop();
        m_gameRunning = false;
    }

    private void StarveWorm()
    {
        m_wormController.Starve();
        m_gameRunning = false;
    }

    private void DestroyRock(GameObject Rock)
    {
        Rock.GetComponent<Platform>().DestroyRock();
    }

    public void GoForward(GameObject platform)
    {
            platform.transform.Translate(0, 0, -terrainSpeed);
    }

    public void GoLeft()
    {
        if (m_wormController.GetRunning())
        {
            foreach (GameObject platform in platforms)
            {
                IEnumerator coroutine = GoLeftCoroutine(platform);
                StartCoroutine(coroutine);
            }
        }
    }

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
    public void GoRight()
    {
        if (m_wormController.GetRunning())
        {
            foreach (GameObject platform in platforms)
            {
                IEnumerator coroutine = GoRightCoroutine(platform);
                StartCoroutine(coroutine);
            }
        }
    }

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
    private void RegenerateTerrain(GameObject platform)
    {
        platform.GetComponent<Platform>().CheckIfRockOrHole();
        platform.transform.Translate(0, 0, terrainLength);
        platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, - 2.5f + Random.Range(-0.3f,0.3f), platform.transform.localPosition.z);

    }

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

    private GameObject[] platforms;
    private float m_turnDuration = 0.1f;
    private WormController m_wormController;
    private LightController m_lightController;
    private bool m_gameRunning = true;
}
