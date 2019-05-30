using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Used to make the main game music audio source not destroyable on scene changes 
public class DontDestroy : MonoBehaviour
{
    public string objectTag;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(objectTag); // find the object with the proper tag

        if (objs.Length > 1) // make sure there is only one such object
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject); // make it not destroyable
    }
}