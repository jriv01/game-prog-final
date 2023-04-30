using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 5;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            GameManager _gameManager = FindObjectOfType<GameManager>();
            _gameManager.Heal(healAmount);
            _gameManager._audioSource.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
