using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    
    public AudioClip deathSound;
    public AudioClip damageSound;
    GameObject deathEffectPrefab;
    AudioSource _audioSource;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage) {
        // Lose hp & check if the enemy died
        hp -= damage;
        if(hp <= 0) {
            StartCoroutine(Die());
        } else {
            _audioSource.PlayOneShot(damageSound);
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed() {
        // Flash sprite red for half a second
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.5f);
        renderer.color = new Color32(255, 255, 255, 255);
    }

    IEnumerator Die() {
        // Disable components
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        // Play death components
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        _audioSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
