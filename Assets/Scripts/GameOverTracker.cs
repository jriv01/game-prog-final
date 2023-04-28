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
        finalCash.text = "Total Cash Collected: " + publicvar.cashAmount;
    }

    
}
