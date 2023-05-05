using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 1;
    
    void OnTriggerEnter2D(Collider2D other) {
        // Tags to ignore
        if(other.CompareTag("Player")) return;
        if(other.CompareTag("Bullet")) return;

        if(other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        if(other.CompareTag("Boss")) {
            other.GetComponent<Bear>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
