using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Color[] GrassColors;
    public Color[] RockColors;
    public Color DestroyedRock;
    public GameObject Apple;
    public GameObject Grass;
    public GameObject LightFlower;
    void Start()
    {
        m_collider = GetComponent<Collider>();
        m_material = GetComponent<Renderer>().material;
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && m_isRock)
        {
            m_hitWorm = true;
        }
        else if (other.tag == "Player" && m_isHole)
        {
            m_dropedWorm = true;
        }

    }

    private IEnumerator RaiseRockCoroutine()
    {
        m_material.color = RockColors[Random.Range(0, 3)];
        while ( transform.localScale.y < 17)
        {
            transform.localScale += new Vector3(0, 0.07f, 0);
            yield return new WaitForSeconds(0.02f);
        }
        transform.localScale = new Vector3(5, 17, 5);
        yield break;
    }

    private IEnumerator DropHoleCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        int t = 30;
        bool side = true;
        while (t > 0)
        {
            if(side)
            {
                transform.localPosition += new Vector3(0.1f, 0, 0);
                side = !side;
                t -= 1;
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                transform.localPosition += new Vector3(-0.1f, 0, 0);
                side = !side;
                t -= 1;
                yield return new WaitForSeconds(0.02f);
            }
        }
        while (transform.localPosition.y > -5)
        {
            transform.localPosition += new Vector3(0, -0.5f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        GetComponent<MeshRenderer>().enabled = false;
        transform.localScale = new Vector3(0.1f, 10, 0.1f);
        transform.localPosition = new Vector3(transform.localPosition.x, -2.5f, transform.localPosition.z);

        yield break;
    }

    public void CheckIfRockOrHole()
    {
        DestroyChildren();
        //GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        float rand = Random.value;
        if(rand < 0.05)
        {
            m_isHole = false;
            m_isRock = true;
            m_collider.isTrigger = true;
            //GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

            StartCoroutine("RaiseRockCoroutine");
        }
        else if (rand > 0.9)
        {
            m_material.color = GrassColors[Random.Range(0, 3)];
            transform.localScale = new Vector3(5, 5, 5);

            m_isRock = false;
            m_isHole = true;
            m_collider.isTrigger = true;

            StartCoroutine("DropHoleCoroutine");
        }
        else if (rand > 0.05 && rand < 0.07 && GameController.premptiveDayTime)
        {

            m_isHole = false;
            GetComponent<MeshRenderer>().enabled = true;
            m_isRock = false;
            m_collider.isTrigger = false;
            m_material.color = GrassColors[Random.Range(0,3)];
            transform.localScale = new Vector3(5, 5, 5);
            SpawnApple();
        }
        else if (rand > 0.05 && rand < 0.07)
        {

            m_isHole = false;
            GetComponent<MeshRenderer>().enabled = true;
            m_isRock = false;
            m_collider.isTrigger = false;
            m_material.color = GrassColors[Random.Range(0, 3)];
            transform.localScale = new Vector3(5, 5, 5);
            SpawnLightFlower();
        }
        else if (rand > 0.1 && rand < 0.2)
        {
            m_isHole = false;
            GetComponent<MeshRenderer>().enabled = true;
            m_isRock = false;
            m_collider.isTrigger = false;
            m_material.color = GrassColors[Random.Range(0, 3)];
            transform.localScale = new Vector3(5, 5, 5);
            //SpawnGrass();
        }
        else
        {
            m_isHole = false;
            GetComponent<MeshRenderer>().enabled = true;
            m_isRock = false;
            m_collider.isTrigger = false;
            m_material.color = GrassColors[Random.Range(0, 3)];
            transform.localScale = new Vector3(5, 5, 5);
        }
    }
    public void RandomGrassColor()
    {
        GetComponent<Renderer>().material.color = GrassColors[Random.Range(0, 3)];
    }
    public void DestroyRock()
    {
        m_collider.isTrigger = false;
        m_hitWorm = false;
        m_material.color = DestroyedRock;
        transform.localScale = new Vector3(5, 5, 5);
    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SpawnApple()
    {
        m_apple = Instantiate(Apple, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        m_apple.transform.parent = transform;
        m_hasApple = true;
    }

    public void SpawnLightFlower()
    {
        m_flower = Instantiate(LightFlower, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
        m_flower.transform.parent = transform;
        m_flower.transform.localEulerAngles += new Vector3(45, 0, 45);
        m_hasApple = true;
    }

    public void SpawnGrass()
    {
        GameObject m_grass = Instantiate(Grass, new Vector3(transform.position.x - 0.94f, transform.position.y - 56.29f + 2.5f, transform.position.z + 60.29f), Quaternion.identity);
        m_grass.transform.parent = transform;
        m_hasGrass = true;
    }

    public bool GetHitWorm()
    {
        return m_hitWorm;
    }
    public void SetHitWorm(bool state)
    {
        m_hitWorm = state;
    }
    public bool GetDroppedWorm()
    {
        return m_dropedWorm;
    }
    private Collider m_collider;
    private Material m_material;
    private bool m_isRock = false;
    private bool m_isHole = false;
    private bool m_hitWorm = false;
    private bool m_dropedWorm = false;
    private bool m_hasApple = false;
    private bool m_hasGrass = false;
    private GameObject m_apple;
    private GameObject m_flower;
}
