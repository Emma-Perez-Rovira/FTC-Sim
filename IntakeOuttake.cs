using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntakeOuttake : MonoBehaviour
{
    [SerializeField]
    private int maxCount = 1;
    [SerializeField]
    private GameObject[] points;
    public GameObject[] collisions;
    public Rigidbody[] rigidBodies;
    public Boolean overflow  = false;
    public Boolean constantMove = true;
    private Boolean isFound = false;

    [SerializeField]
    private InputAction intakeTrigger;
    private void OnEnable()
    {
        intakeTrigger.Enable();
    }
    private void OnDisable()
    {
        intakeTrigger.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        collisions = new GameObject[maxCount];
        rigidBodies = new Rigidbody[maxCount];
    }
    public void constantMoveOff()
    {
        constantMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (constantMove)
        {
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i] != null)
                {
                   // Debug.Log("Moving Scoring Elements");
                    collisions[i].gameObject.transform.position = points[i].transform.position;
                    rigidBodies[i].useGravity = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (intakeTrigger.IsPressed()) { 
        Debug.Log("Collided with" +  other.ToString());
        if (other == null) return;
            if (other.gameObject.tag == "Scoring Element")
            {
                other.GetComponent<Rigidbody>().freezeRotation = true;
                other.GetComponent<BoxCollider>().enabled = false;
                constantMove = true;
                Debug.Log("is found: " + isFound);

                for (int i = 0; i < collisions.Length; i++)
                {
                    if (collisions[i] != null)
                    {
                        Debug.Log(collisions[i].ToString());
                    }
                    else
                    {
                        Debug.Log("NULL");
                    }
                    if (collisions[i] == null && !isFound)
                    {
                        collisions[i] = other.gameObject;
                        rigidBodies[i] = other.GetComponent<Rigidbody>();
                        isFound = true;
                    }
                }
                isFound = false;
            }
        }
    }
}
