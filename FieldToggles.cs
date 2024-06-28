using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldToggles : MonoBehaviour
{
    [SerializeField]
    private GameObject fieldSwitcher;
    [SerializeField]
    private GameObject botSwitcher;
    [SerializeField]
    private UnityEvent centerstageSwitch;
    [SerializeField]
    private UnityEvent centerstageBotsSwitch;
    [SerializeField]
    private UnityEvent powerPlaySwitch;
    [SerializeField]
    private UnityEvent powerPlayBotsSwitch;
    [SerializeField]
    private UnityEvent relicRecoverySwitch;
    [SerializeField]
    private UnityEvent relicRecoveryBotsSwitch;
    private bool[] loadingFields;
    // Start is called before the first frame update
    void Awake()
    {
        string storage = PlayerPrefs.GetString("Activated");
        loadingFields = new bool[storage.Length];
        for(int i = 0; i < storage.Length; i++)
        {
            if(storage[i] == '1')
            {
                loadingFields[i] = true;
            } else
            {
                loadingFields[i] = false;
            }
        }
        if (loadingFields[0]) 
        {
            centerstageSwitch.Invoke();
        }
        if (loadingFields[1])
        {
            //centerstageBotsSwitch.Invoke();
        }
        if (loadingFields[2])
        {
            powerPlaySwitch.Invoke();
        }
        if (loadingFields[3])
        {
            powerPlayBotsSwitch.Invoke();
        }
        if (loadingFields[4])
        {
            relicRecoverySwitch.Invoke();
        }
        if (loadingFields[5])
        {
            relicRecoveryBotsSwitch.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
