using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private int coinCount = 0;
    private bool grenadeUnlocked = false;
    
    TextMeshProUGUI moneyUI;
    TextMeshProUGUI costUI;
    GameObject purchaseButton;

    private void Awake() {
        // Don't Destroy on Load
        if(GameObject.FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += SceneChange;

        purchaseButton = GameObject.FindGameObjectWithTag("PurchaseButton");
        costUI = GameObject.FindGameObjectWithTag("CostUI").GetComponent<TextMeshProUGUI>();
        GameObject.FindGameObjectWithTag("ShopUI").SetActive(false);
        purchaseButton.SetActive(false);
        costUI.enabled = false;
        
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void SceneChange(Scene current, Scene next) {
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void UpdateUI() {
        moneyUI.text = "COINS: " + coinCount;
    }

    public void ResetCoins(){
        coinCount = 0;  
        UpdateUI();
    }

    public void AddMoney(int moneyNum) 
    {
        coinCount += moneyNum;
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
    #if !UNITY_WEBGL
        // Esc to Exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    #endif
        
    }
}
