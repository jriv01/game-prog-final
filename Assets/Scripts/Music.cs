using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
     private AudioSource _audioSource;
     private GameObject[] othermusics;
     private bool first = true;
     private void Awake()
     {
         othermusics = GameObject.FindGameObjectsWithTag("Music");
 
         foreach (GameObject obj in othermusics)
         {
             if (obj.scene.buildIndex == -1)
             {
                first = false;
             }
         }
 
         if (!first)
         {
             Destroy(gameObject);
         }
         DontDestroyOnLoad(transform.gameObject);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
