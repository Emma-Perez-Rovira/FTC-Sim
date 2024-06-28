using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private InputAction openMenu;
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
    public void Update()
    {
        if (openMenu != null) 
        {
            if (openMenu.WasPressedThisFrame())
            {
                toMenu();
            }
        }
    }
}
