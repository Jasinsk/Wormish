using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsAspectRatioFixer : MonoBehaviour
{
    private void Awake()
    {
        //Set screen size for Standalone
#if UNITY_STANDALONE
        Screen.SetResolution(432, 768, false);
        Screen.fullScreen = false;
#endif
    }
}
