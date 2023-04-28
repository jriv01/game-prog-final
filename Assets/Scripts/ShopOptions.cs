using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOptions : MonoBehaviour
{

    public static int hpCost = 20;
    public static int ammoCost = 10;
    static GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void BuyHP() {
        if(_gameManager.CanAfford(hpCost)) {
            _gameManager.TakeMoney(hpCost);
            print("INCREASE HP");
        }
    }

    public void BuyAmmo() {
        if(_gameManager.CanAfford(ammoCost)) {
            _gameManager.TakeMoney(ammoCost);
            print("INCREASE AMMO");
        }
    }
}
