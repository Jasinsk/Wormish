using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responsible for the menu screen worm animation
public class WormLogoController : MonoBehaviour
{
    public float wormScaleMin = 0.7f;
    public float wormScaleMax = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (m_enlarging)
        {
            if (transform.localScale.y < wormScaleMax)
            {
                transform.localScale += new Vector3(-0.0025f, 0.025f, 0);
            }
            else
            {
                m_enlarging = false;
            }
        }
        else
        {
            if (transform.localScale.y > wormScaleMin)
            {
                transform.localScale += new Vector3(0.0025f, -0.025f, 0);
            }
            else
            {
                m_enlarging = true;
            }
        }
    }

    private bool m_enlarging = true;
}
