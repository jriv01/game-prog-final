using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponName;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Player _player = FindObjectOfType<Player>();
            _player.SetWeapon(weaponName);
            _player._audioSource.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
