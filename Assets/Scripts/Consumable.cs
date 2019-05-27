using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for the creation and use of all consumable items
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
    // This function is called when the consumable item comes into contact with our worm 
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
    // Causes all consumable item effects
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
