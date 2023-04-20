using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomCell[] cellsToActivate;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            foreach(RoomCell cell in cellsToActivate) {
                cell.Activate();
            }
            Destroy(gameObject);
        }
    }
}
