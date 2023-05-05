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
        
        string cashCollected = "Total Cash Collected: " + PublicVars.totalCash;
        string finalScore = "Final Score: " + PublicVars.totalScore;

        finalCash.text = cashCollected + "\n\n" + finalScore;
    }

    
}
