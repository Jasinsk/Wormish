using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public bool apple = false;
    public bool flower = false;
    void Start()
    {
        m_wormController = GameObject.FindGameObjectWithTag("Player").GetComponent<WormController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetEaten();
            GetComponent<MeshRenderer>().enabled = false;
            DestroyChildren();
        }
    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void GetEaten()
    {
        if (apple)
        {
            m_wormController.FeedMe();
        }
        if (flower)
        {
            m_wormController.LightMeUp();
        }
        
    }

    private WormController m_wormController;
}
