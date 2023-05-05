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
    public int initialhealth = 20;
    public int score = 0;
    public int ammoCount = 20;
    private bool grenadeUnlocked = false;
    
    TextMeshProUGUI moneyUI;

    TextMeshProUGUI healthUI;
    TextMeshProUGUI costUI;
    TextMeshProUGUI ammoUI;
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
        ammoUI = GameObject.FindGameObjectWithTag("ammoUI").GetComponent<TextMeshProUGUI>();

        GameObject.FindGameObjectWithTag("ShopUI").SetActive(false);
        purchaseButton.SetActive(false);
        costUI.enabled = false;
        
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("healthinterface").GetComponent<TextMeshProUGUI>();
        scoreUI = GameObject.FindGameObjectWithTag("scoreui").GetComponent<TextMeshProUGUI>();
        ammoUI = GameObject.FindGameObjectWithTag("ammoUI").GetComponent<TextMeshProUGUI>();
        scoreUI = GameObject.FindGameObjectWithTag("scoreui").GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }
    public void addAmmo(int value)
    {
        ammoCount += value;
        UpdateUI();
    }

    public void incrementEnemyScoreCounter(int value) {
        score += value;
        PublicVars.totalScore += value;
        scoreUI.text = "Score: " + score;
    }
    public void Heal(int value) {
        health += value;
        PublicVars.totalHealth += value;
        _audioSource.PlayOneShot(healPlayer);
        UpdateUI();    
    }

    public void TakeDamage(int value) {
        health -= value;
        _audioSource.PlayOneShot(hurtPlayer);
        UpdateUI();
        if (health < 0){
            PublicVars.totalScore = score;
            health = initialhealth;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void SceneChange(Scene current, Scene next) {
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("healthinterface").GetComponent<TextMeshProUGUI>();
        purchaseButton = GameObject.FindGameObjectWithTag("PurchaseButton");
        costUI = GameObject.FindGameObjectWithTag("CostUI").GetComponent<TextMeshProUGUI>();
        GameObject.FindGameObjectWithTag("ShopUI").SetActive(false);
        ammoUI = GameObject.FindGameObjectWithTag("ammoUI").GetComponent<TextMeshProUGUI>();
        scoreUI = GameObject.FindGameObjectWithTag("scoreui").GetComponent<TextMeshProUGUI>();
        purchaseButton.SetActive(false);
        costUI.enabled = false;
        UpdateUI();
    }

    public void UpdateUI() {
        moneyUI.text = "CASH: " + coinCount;
        healthUI.text = "HEALTH: " + health;
        ammoUI.text = "Ammo: " + ammoCount;
        scoreUI.text = "Score: " + score;
    }

    public void ResetCoins(){
        coinCount = 0;  
        totalCoinsCollected = 0;
        UpdateUI();
    }

    public void AddMoney(int moneyNum) 
    {
        PublicVars.totalCash += moneyNum;
        coinCount += moneyNum;
        totalCoinsCollected += moneyNum;
        UpdateUI();
    }
    
    public bool CanAfford(int amount) {
        return coinCount >= amount;
    }

    public void TakeMoney(int amount) {
        coinCount -= amount;

        if(coinCount < 0) coinCount = 0;

        UpdateUI();
    }

    public void UnlockGrenade(bool unlock) {
        grenadeUnlocked = unlock;
    }

    public bool getGrenadeStatus() {return grenadeUnlocked;}

    // Update is called once per frame
    void Update()
    {

    #if !UNITY_WEBGL
        // Esc to Exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    #endif
        
    }
}
