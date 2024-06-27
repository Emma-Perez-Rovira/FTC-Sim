using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntakeOuttake : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    private Boolean playerInput;
    [SerializeField]
    private int maxCount = 1;
    [SerializeField]
    private GameObject[] points;
    public GameObject[] collisions;
    public Rigidbody[] rigidBodies;
    public Boolean overflow  = false;
    public Boolean constantMove = true;
    private Boolean isFound = false;


    public void OnIntake(InputAction.CallbackContext ctx) => playerInput = ctx.action.triggered;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

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
        if (playerInput) { 
        Debug.Log("Collided with" +  other.ToString());
        if (other == null) return;
            if (other.gameObject.tag == "Scoring Element" || other.gameObject.tag == "Scoring Element (F)" || other.gameObject.tag == "Pixel sub")
            {
                if (other.gameObject.tag == "Pixel sub")
                {
                    other.GetComponentInParent<Rigidbody>().freezeRotation = true;
                    GameObject parent = other.transform.parent.gameObject;
                    for (int j = 0; j < parent.transform.childCount; j++)
                    {
                        parent.transform.GetChild(j).GetComponent<BoxCollider>().enabled = false;
                    }
                    other.GetComponentInParent<BoxCollider>().enabled = false;
                }
                else
                {
                    other.GetComponent<Rigidbody>().freezeRotation = true;
                    other.GetComponent<BoxCollider>().enabled = false;
                }
                
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
                        if (other.gameObject.tag == "Pixel sub")
                        {
                            collisions[i] = other.transform.root.gameObject;
                            rigidBodies[i] = other.GetComponentInParent<Rigidbody>();
                        }
                        else
                        {
                            collisions[i] = other.gameObject;
                            rigidBodies[i] = other.GetComponent<Rigidbody>();
                        }
                        
                        isFound = true;
                    }
                }
                isFound = false;
            }

        }
    }
}
