using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    GameObject options;

    // Start is called before the first frame update
    void Awake()
    {
        options = GameObject.FindGameObjectWithTag("ShopUI");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            options.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            options.SetActive(false);
        }
    }
}
