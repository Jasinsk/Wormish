﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class responsible for the floating apple animation
public class Floating : MonoBehaviour
{
    public float rotationDuration = 1;
    public float maxHeight = 1.5f;
    public float minHeight = 0.5f;

    void Start()
    {
        StartCoroutine("RotatingCoroutine");
    }

    void Update()
    {
        if (m_rising) // apple floating animation
        {
            if (transform.localPosition.y < maxHeight)
            {
                transform.localPosition += new Vector3(0, 0.01f, 0);
            }
            else
            {
                m_rising = false;
            }    
        }
        else
        {
            if (transform.localPosition.y > minHeight)
            {
                transform.localPosition -= new Vector3(0, 0.01f, 0);
            }
            else
            {
                m_rising = true;
            }
        }
    }
    // used to make the apple rotate while floating
    private IEnumerator RotatingCoroutine()
    {
        Quaternion rotator = Quaternion.Euler(0, 15f, 0); // the quaternion which will be used to rotate our object
        while (true)
        {
            Quaternion currentRotation = transform.localRotation;
            transform.rotation = rotator * currentRotation; // calculate new quaternion value after rotation
            yield return new WaitForSeconds(0.2f);
        }
    }

    private bool m_rising = true;
}
