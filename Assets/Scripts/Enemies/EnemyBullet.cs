using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            FindObjectOfType<GameManager>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
