using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public int totalCoinsCollected = 0;
    public int health = 20;
    public int score = 0;
    private bool grenadeUnlocked = false;
    
    TextMeshProUGUI moneyUI;

    TextMeshProUGUI healthUI;
    TextMeshProUGUI costUI;
    TextMeshProUGUI scoreUI;
    GameObject purchaseButton;
    Player player;

    public AudioSource _audioSource;
    public AudioClip hurtPlayer;
    public AudioClip healPlayer;

    private void Awake() {
        // Don't Destroy on Load
        if(GameObject.FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        SceneManager.activeSceneChanged += SceneChange;

        purchaseButton = GameObject.FindGameObjectWithTag("PurchaseButton");
        costUI = GameObject.FindGameObjectWithTag("CostUI").GetComponent<TextMeshProUGUI>();
        GameObject.FindGameObjectWithTag("ShopUI").SetActive(false);
        purchaseButton.SetActive(false);
        costUI.enabled = false;
        
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("healthinterface").GetComponent<TextMeshProUGUI>();
        scoreUI = GameObject.FindGameObjectWithTag("scoreui").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void incrementEnemyScoreCounter(int value){
        score += value;
        scoreUI.text = "Score: " + score;
    }

    public void decrementHealthCounter(int value){
        health -= value;
        AudioSource.PlayClipAtPoint(hurtPlayer, gameObject.transform.position);
        healthUI.text = "Health: " + health;
        UpdateUI();
    }

    public void incrementHealthCounter(int value){
        health += value;
        AudioSource.PlayClipAtPoint(healPlayer, gameObject.transform.position);
        healthUI.text = "Health: " + health;
        UpdateUI();

    }

    public void TakeDamage(int value) {
        health -= value;
        _audioSource.PlayOneShot(hurtPlayer);
        UpdateUI();
    }

    public void SceneChange(Scene current, Scene next) {
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("healthinterface").GetComponent<TextMeshProUGUI>();
        purchaseButton = GameObject.FindGameObjectWithTag("PurchaseButton");
        costUI = GameObject.FindGameObjectWithTag("CostUI").GetComponent<TextMeshProUGUI>();
        GameObject.FindGameObjectWithTag("ShopUI").SetActive(false);
        purchaseButton.SetActive(false);
        costUI.enabled = false;
        UpdateUI();
    }

    public void UpdateUI() {
        moneyUI.text = "CASH: " + coinCount;
        healthUI.text = "HEALTH: " + health;
    }

    public void ResetCoins(){
        coinCount = 0;  
        totalCoinsCollected = 0;
        UpdateUI();
    }

    public void AddMoney(int moneyNum) 
    {
        coinCount += moneyNum;
        totalCoinsCollected += moneyNum;
        UpdateUI();
    }
    
    public bool CanAfford(int amount) {
        return coinCount >= amount;
    }

    public void TakeMoney(int amount) {
        coinCount -= amount;
        UpdateUI();
    }

    public void UnlockGrenade(bool unlock) {
        grenadeUnlocked = unlock;
    }

    public bool getGrenadeStatus() {return grenadeUnlocked;}

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            SceneManager.LoadScene("GameOver");
        }

    #if !UNITY_WEBGL
        // Esc to Exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    #endif
        
    }
}
