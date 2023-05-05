using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    
    public AudioClip trapSound;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            GameManager _gameManager = FindObjectOfType<GameManager>();
            _gameManager.TakeDamage(1);
            _gameManager._audioSource.PlayOneShot(trapSound);
            Destroy(gameObject);
        }
    }
}
