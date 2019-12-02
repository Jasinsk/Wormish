using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHitter : MonoBehaviour
{
    void Start()
    {
        m_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_gameController.KillWorm();
        }
    }

    private GameController m_gameController;
}
