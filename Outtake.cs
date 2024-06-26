using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Outtake : MonoBehaviour
{
    public GameObject[] teleportPoints;
    public int maxNum = 3;
    public InputAction outtakeTrigger;
    public bool outtakeTriggerIsAnalog = false;
    public GameObject intake;
    
    private GameObject[] objects;
    private Rigidbody[] rigidBodies;
    
    private void OnEnable()
    {
        outtakeTrigger.Enable();
    }
    private void OnDisable()
    {
        outtakeTrigger.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (outtakeTriggerIsAnalog)
        {
            if (outtakeTrigger.ReadValue<float>() > 0.5f)
            {
                OnOuttakeTrigger();
            }
        }
        else
        {
            if (outtakeTrigger.IsPressed())
            {
                OnOuttakeTrigger();
            }
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
                objects[i].GetComponent<BoxCollider>().enabled = true;
                //rigidBodies[i].freezeRotation = false;

            }
        }

        intake.GetComponent<IntakeOuttake>().collisions = new GameObject[maxNum];
        intake.GetComponent<IntakeOuttake>().rigidBodies = new Rigidbody[maxNum];

    }
}
