using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFieldSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] FieldStuff;
    [SerializeField]
    private GameObject[] paths;
    [SerializeField]
    private GameObject[] NPCBots;
    [SerializeField]
    private Boolean startEnabled;
    private Boolean enabled;
    // Start is called before the first frame update
    void Start()
    {
        enabled = startEnabled;
        if (enabled)
        {
            for(int i = 0; i < FieldStuff.Length; i++)
            {
                FieldStuff[i].SetActive(true);
            }
            for (int i = 0; i < NPCBots.Length; i++)
            {
                NPCBots[i].SetActive(true);
            }
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < FieldStuff.Length; i++)
            {
                FieldStuff[i].SetActive(false);
            }
            for (int i = 0; i < NPCBots.Length; i++)
            {
                NPCBots[i].SetActive(false);
            }
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i].SetActive(false);
            }
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
            for (int i = 0; i < FieldStuff.Length; i++)
            {
                FieldStuff[i].SetActive(true);
            }
            for (int i = 0; i < NPCBots.Length; i++)
            {
                NPCBots[i].SetActive(true);
            }
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < FieldStuff.Length; i++)
            {
                FieldStuff[i].SetActive(false);
            }
            for (int i = 0; i < NPCBots.Length; i++)
            {
                NPCBots[i].SetActive(false);
            }
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i].SetActive(false);
            }
        }
    }
}
