using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOptions : MonoBehaviour
{

    public static int hpCost = 20;
    public static int ammoCost = 10;
    static GameManager _gameManager;
    GameObject ShopUI;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        ShopUI = GameObject.FindGameObjectWithTag("ShopUI");
    }

    public void BuyHP() {
        if(_gameManager.CanAfford(hpCost)) {
            _gameManager.TakeMoney(hpCost);
            print("INCREASE HP");
            ShopUI.SetActive(false);
        }
    }

    public void BuyAmmo() {
        if(_gameManager.CanAfford(ammoCost)) {
            _gameManager.TakeMoney(ammoCost);
            ShopUI.SetActive(false);
            print("INCREASE AMMO");
        }
    }
}
