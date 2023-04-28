using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string firstLevel = "FloorOne";
    public void GoToLevel1()
    {
        SceneManager.LoadScene(firstLevel);
    }

   
}
