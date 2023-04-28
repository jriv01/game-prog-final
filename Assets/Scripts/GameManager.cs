using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public int health = 20;
    
    TextMeshProUGUI moneyUI;

    TextMeshProUGUI healthInterface;
    TextMeshProUGUI costUI;
    TextMeshProUGUI scoreInterface;
    GameObject purchaseButton;

    public AudioSource _audioSource;
    public AudioClip hurtPlayer;
    //public AudioClip healPlayer;

   //public GameObject redDisplay;

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
        healthInterface = GameObject.FindGameObjectWithTag("healthinterface").GetComponent<TextMeshProUGUI>();
        scoreInterface = GameObject.FindGameObjectWithTag("scoreui").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void incrementEnemyScoreCounter(int value){
        publicvar.enemyKilled += value;
        scoreInterface.text = "Score: " + publicvar.enemyKilled;
    }

    public void decrementHealthCounter(int value){
        health -= value;
        AudioSource.PlayClipAtPoint(hurtPlayer, gameObject.transform.position);
        healthInterface.text = "Health: " + health;
        UpdateUI();

        //var imageAttribute =  redDisplay.GetComponent<Image>().color;
        //imageAttribute.a = 0.95f;
        //redDisplay.GetComponent<Image>().color = imageAttribute;
    }

    public void incrementHealthCounter(int value){
        health += value;
       //AudioSource.PlayClipAtPoint(healPlayer, gameObject.transform.position);
        healthInterface.text = "Health: " + health;
        UpdateUI();

    }

    public void SceneChange(Scene current, Scene next) {
        moneyUI = GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    public void UpdateUI() {
        moneyUI.text = "CASH: " + coinCount;
        healthInterface.text = "HEALTH: " + health;
    }

    public void ResetCoins(){
        coinCount = 0;  
        UpdateUI();
    }

    public void AddMoney(int moneyNum) 
    {
        coinCount += moneyNum;
        publicvar.cashAmount += moneyNum;
        UpdateUI();
    }
    
    public bool CanAfford(int amount) {
        return coinCount >= amount;
    }

    public void TakeMoney(int amount) {
        coinCount -= amount;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
    if (health <= 0){
        SceneManager.LoadScene("GameOver");
    }
    // if(redDisplay is not null){
    //     if(redDisplay.GetComponent<Image>().color.a > 0){
    //         var imageAttribute =  redDisplay.GetComponent<Image>().color;
    //         imageAttribute.a = imageAttribute.a - 0.01f;
    //         redDisplay.GetComponent<Image>().color = imageAttribute;
    //     }
    // }
    #if !UNITY_WEBGL
        // Esc to Exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    #endif
        
    }
}
