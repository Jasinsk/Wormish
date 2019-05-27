using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for player inputs on PC
public class PlayerController : MonoBehaviour
{

    public GameObject Worm;
    public GameObject GameController;
    void Start()
    {
        m_gameController = GameController.GetComponent<GameController>();
        m_wormController = Worm.GetComponent<WormController>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            m_gameController.GoLeft();
            m_wormController.RollLeft();

        }
        else if (Input.GetKeyDown("right"))
        {
            m_gameController.GoRight();
            m_wormController.RollRight();
        }
        else if(Input.GetKeyDown("up"))
        {
            m_wormController.Charge();
        }
    }

    private GameController m_gameController;
    private WormController m_wormController;
}
