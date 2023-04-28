using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private int coinCount = 0;
    public TextMeshProUGUI coinUI;

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
        GameObject.FindGameObjectWithTag("PurchaseButton").SetActive(false);
        coinUI.text = "MONEY: " + coinCount;
    }
    public void resetCoins(){
        coinCount = 0;
    }
    public void AddMoney(int moneyNum) 
    {
        coinCount += moneyNum;
        GameObject.FindGameObjectWithTag("moneyui").GetComponent<TextMeshProUGUI>().text = "MONEY: " + coinCount;
    }

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
