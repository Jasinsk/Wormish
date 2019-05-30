using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for input control on Android
public class AndroidInputController : MonoBehaviour
{
    public GameObject Worm;
    public GameObject GameController;

    void Start()
    {
        m_gameController = GameController.GetComponent<GameController>();
        m_wormController = Worm.GetComponent<WormController>();
        dragDistance = Screen.height * 10 / 100; //dragDistance is 15% height of the screen
    }

    void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); 
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position

                //Check if drag distance is greater than the minimum
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement
                        if ((lp.x > fp.x)) 
                        {   //Right swipe
                            m_gameController.GoRight();
                            m_wormController.RollRight();
                        }
                        else
                        {   //Left swipe
                            m_gameController.GoLeft();
                            m_wormController.RollLeft();
                        }
                    }
                    else
                    {   
                        if (lp.y > fp.y)  
                        {   //Up swipe
                            m_wormController.Charge();
                        }
                        else
                        {   //Down swipe
                        }
                    }
                }
                else
                {   // if it's not a drag then it's a tap
                }
            }
        }
    }

    private GameController m_gameController;
    private WormController m_wormController;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
}
