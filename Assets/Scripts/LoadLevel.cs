using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string levelToLoad = "";

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
