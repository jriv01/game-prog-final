using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverTracker : MonoBehaviour
{

    public TextMeshProUGUI finalCash;
    // Start is called before the first frame update
    void Start()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();

        string cashCollected = "Total Cash Collected: " + _gameManager.totalCoinsCollected;
        string finalScore = "Final Score: " + _gameManager.score;

        finalCash.text = cashCollected + "\n\n" + finalScore;
    }

    
}
