using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public string demoLevel = "DemoEnvironment";
    public void GoToLevel()
    {
        SceneManager.LoadScene(demoLevel);
    }

}
