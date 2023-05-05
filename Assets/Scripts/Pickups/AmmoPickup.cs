using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    GameManager gameManager;
    public AudioClip pickupSound;
    void Start(){
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            gameManager.addAmmo(30);
            gameManager._audioSource.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
