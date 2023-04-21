using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public RoomCell[] cellsToActivate;
    GameObject button;

    void Awake() {
        button = GameObject.FindGameObjectWithTag("PurchaseButton");
        button.GetComponent<Button>().onClick.AddListener(() => {print("Clicked");});
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
           button.SetActive(true);
           button.GetComponent<Button>().onClick.AddListener(() => OpenDoor());
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.SetActive(false);
        }
    }

    public void OpenDoor() {
        foreach(RoomCell cell in cellsToActivate) {
            cell.Activate();
        }
        Destroy(gameObject);
    }
}
