using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
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
        m_wormController.FeedMe();
    }

    private WormController m_wormController;
}
