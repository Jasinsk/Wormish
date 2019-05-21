using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
{
    private void OnMouseDown()
    {
        Application.LoadLevel("MenuScene");
    }
}
