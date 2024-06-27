using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Outtake : MonoBehaviour
{

    private Boolean outtakeTrigger;

    public GameObject[] teleportPoints;
    public int maxNum = 3;
    public bool outtakeTriggerIsAnalog = false;
    public GameObject intake;
    
    private GameObject[] objects;
    private Rigidbody[] rigidBodies;

    public void OnOuttake(InputAction.CallbackContext ctx) => outtakeTrigger = ctx.action.triggered;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        if (outtakeTrigger)
        {
            OnOuttakeTrigger();
        }
        
    }
    private void OnOuttakeTrigger()
    {
       
        
        intake.GetComponent<IntakeOuttake>().constantMoveOff();
        objects = intake.GetComponent<IntakeOuttake>().collisions;
        rigidBodies = intake.GetComponent<IntakeOuttake>().rigidBodies;

        for (int i = 0; i < objects.Length; i++)
        {
            Debug.Log("Outtake: " + i);
            if (objects[i] != null)
            {
                objects[i].transform.position = teleportPoints[i].transform.position;
                rigidBodies[i].useGravity = true;
                rigidBodies[i].velocity = Vector3.zero;
                if (objects[i].tag == "Pixel" || objects[i].tag.IndexOf("Nested Object Top") != -1) 
                { 
                    for(int j = 0; j < objects[i].transform.childCount; j++)
                    {
                        objects[i].transform.GetChild(j).GetComponent<BoxCollider>().enabled = true;
                    }
                }
                else
                {
                    objects[i].GetComponent<BoxCollider>().enabled = true;
                }
                if (objects[i].tag.IndexOf("(F)") == -1) rigidBodies[i].freezeRotation = false;

            }
        }

        intake.GetComponent<IntakeOuttake>().collisions = new GameObject[maxNum];
        intake.GetComponent<IntakeOuttake>().rigidBodies = new Rigidbody[maxNum];

    }
}
