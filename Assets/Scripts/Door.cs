using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Door : MonoBehaviour
{
    public RoomCell[] cellsToActivate;
    public int cost = 20;

    public AudioClip doorLocked;
    public AudioClip doorUnlock;

    GameObject button;
    TextMeshProUGUI costUI;
    GameManager gameManager;
    AudioSource audioSource;

    void Awake() {
        button = GameObject.FindGameObjectWithTag("PurchaseButton");
    }

    void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        costUI = GameObject.FindGameObjectWithTag("CostUI").GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if(gameManager.CanAfford(cost)) {
                button.SetActive(true);
                button.GetComponent<Button>().onClick.AddListener(() => OpenDoor());
            }
            else {
                costUI.enabled = true;
                costUI.text = "COSTS $" + cost;
                audioSource.PlayOneShot(doorLocked);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if(button.activeSelf) {
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.SetActive(false);
            } else {
                costUI.enabled = false;
            }
            
        }
    }

    public void OpenDoor() {
        StartCoroutine(UnlockDoor());
    }

    IEnumerator UnlockDoor() {
        audioSource.PlayOneShot(doorUnlock);
        foreach(RoomCell cell in cellsToActivate) {
            cell.Activate();
        }
        gameManager.TakeMoney(cost);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        foreach(Collider2D collide in gameObject.GetComponents<Collider2D>()) {
            collide.enabled = false;
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
