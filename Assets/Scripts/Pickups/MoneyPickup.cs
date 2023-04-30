using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public int value = 20;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            GameManager _gameManager = FindObjectOfType<GameManager>();
            _gameManager.AddMoney(value);
            _gameManager._audioSource.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
