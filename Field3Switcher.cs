using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field3Switcher : MonoBehaviour
{
    [SerializeField]
    private GameObject field;
    [SerializeField]
    private Boolean startEnabled;
    private Boolean enabled;
    
    // Start is called before the first frame update
    void Start()
    {
        enabled = startEnabled;
        if (enabled)
        {
            field.SetActive(true);
        }
        else
        {
            field.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void switcher()
    {

        enabled = !enabled;
        if (enabled)
        {
            field.SetActive(true);
        } else
        {
            field.SetActive(false);
        }
    }
    
}
