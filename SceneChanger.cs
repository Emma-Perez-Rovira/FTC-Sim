using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void toGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void startScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
