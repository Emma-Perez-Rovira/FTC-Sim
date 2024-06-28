using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OpenMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuFirst;
    [SerializeField]
    private GameObject pauseMenuFirst;

    [SerializeField]
    private InputAction openMenu;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject mainMenu;
    private void OnEnable()
    {
        openMenu.Enable();
    }
    private void OnDisable()
    {
        openMenu.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (openMenu.WasPressedThisFrame())
        {
            OnMenuOpen();
        }
    }
    public void OnMenuOpen()
    {
        menu.SetActive(!menu.activeInHierarchy);
        if (menu.activeInHierarchy) 
        {
            EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
            Time.timeScale = 0.0f;
        } else
        {
            Time.timeScale = 1.0f;
        }
    }
    public void MainMenuToggle()
    {
        if (!mainMenu.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(mainMenuFirst);
            menu.SetActive(false);

        }
        else
        {
            Time.timeScale = 1.0f;
            mainMenu.SetActive(false);
        }
    }

}
