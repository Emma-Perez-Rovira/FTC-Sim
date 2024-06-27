using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class ExampleMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField]
    private bool usingGamepad = true;
    [SerializeField]
    private InputAction forwardKey;
    [SerializeField]
    private InputAction rightKey;
    [SerializeField]
    private InputAction backKey;
    [SerializeField]
    private InputAction leftKey;
    [SerializeField]
    private InputAction leftRotate;
    [SerializeField]
    private InputAction rightRotate;
    private float forwardPressed = 0;
    private float rightPressed = 0;
    private float backPressed = 0;
    private float leftPressed = 0;
    private int lRotatePressed = 0;
    private int rRotatePressed = 0;
    private float speedMult = 0;
    [SerializeField]
    private float accelerationRate = 0.5f;
    public float forwardSpeedDifferenceOverStrafe = 1.2f;
    private InputAction slowMode;
    private Vector2 leftJoystick;
    private Vector2 rightJoystick;
    public Rigidbody rb;
    public float rotSpeed = 5f;
    public float maxSpeed = 50;
    private float[] acceleration = new float[4];

    public Boolean onFloor = true;

    
    private void Awake()
    {
        //leftJoystick = playerInput.actions["LeftJoy"];
        //rightJoystick = playerInput.actions["RightJoy"];
        slowMode = playerInput.actions["Slowmode"];
        if (leftJoystick.IsUnityNull() || rightJoystick.IsUnityNull() || slowMode.IsUnityNull()) 
        {
            Debug.Log("LJ: " + !leftJoystick.IsUnityNull() + " RJ: " + !rightJoystick.IsUnityNull() + " SM: " + !slowMode.IsUnityNull());
            Debug.Log("Map: " + !playerInput.IsUnityNull());
        }
    }
    public void OnLeftJoy(InputAction.CallbackContext ctx) => leftJoystick = ctx.ReadValue<Vector2>();
    public void OnRightJoy(InputAction.CallbackContext ctx) => rightJoystick = ctx.ReadValue<Vector2>();
    void OnEnable()
    {
        forwardKey.Enable();
        rightKey.Enable();
        leftKey.Enable();
        backKey.Enable();
        leftRotate.Enable();
        rightRotate.Enable();

        //leftJoystick.Enable();
        //rightJoystick.Enable();
        slowMode.Enable();
    }
    void OnDisable()
    {
        forwardKey.Disable();
        rightKey.Disable();
        leftKey.Disable();
        backKey.Disable();
        leftRotate.Disable();
        rightRotate.Disable();

        //leftJoystick.Disable();
        //rightJoystick.Disable();
        slowMode.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }



    
        
        // Update is called once per frame

    private void FixedUpdate()
    {
        
        
        if (usingGamepad)
        {
            doStuffGamepad();
        }
        else
        {
            doStuffKeyboard();
        }
        
        
    }
    private void doStuffGamepad()
    {

        if (slowMode.IsPressed())
        {
            speedMult = 0.5f;
        }
        else 
        { 
            speedMult = 1f;
        }
        Vector2 leftJoy = leftJoystick;
        Vector2 rightJoy = rightJoystick * rotSpeed;

        float speedDifX = maxSpeed - Math.Abs(rb.velocity.x);
        float speedDifZ = maxSpeed - Math.Abs(rb.velocity.z);
        speedDifX *= accelerationRate;
        speedDifZ *= accelerationRate;
        Vector2 speedMod = new Vector2(speedDifX, speedDifZ);

        float rotationMultY = rb.rotation.y;
        Vector2 rotationMult = new Vector2(Mathf.Abs(Mathf.Cos(rotationMultY)), Mathf.Abs(Mathf.Sin(rotationMultY)));
        rotationMult.Normalize();
        rotationMult *= forwardSpeedDifferenceOverStrafe;
        float rotationMultNum = Math.Abs(Vector2.Dot(speedMod, rotationMult));

        //Vector3 movement = new Vector3(speedDifX * (forwardPressed - backPressed), rb.velocity.y * Time.fixedDeltaTime, speedDifZ * (rightPressed - leftPressed));
        Vector3 movement = new Vector3(leftJoy.y, rb.velocity.y * Time.fixedDeltaTime, -leftJoy.x) * (speedMod.magnitude + rotationMultNum);
        rb.AddForce(movement * speedMult);
        Quaternion rotation = Quaternion.Euler(new Vector3(0,1,0) * (rightJoy.x));
        rb.MoveRotation(rb.rotation * rotation);
    }

    private void doStuffKeyboard()
    {
        if (forwardKey.IsPressed())
        {
            forwardPressed = 1;
            if (acceleration[0] < maxSpeed)
            {
                acceleration[0] += 0.1f;
            }
        }
        else
        {
            forwardPressed = 0;
            acceleration[0] = 0;
        }
        if (rightKey.IsPressed())
        {
            rightPressed = 1;
            if (acceleration[1] < maxSpeed)
            {
                acceleration[1] += 0.1f;
            }
        }
        else 
        { 
            rightPressed = 0;
            acceleration[1] = 0;
        }
        if (leftKey.IsPressed())
        {
            leftPressed = 1;
            if (acceleration[2] < maxSpeed)
            {
                acceleration[2] += 0.1f;
            }
        }
        else
        {
            leftPressed = 0;
            acceleration[2] = 0;
        }
        if (backKey.IsPressed())
        {
            backPressed = 1;
            if (acceleration[3] < maxSpeed)
            {
                acceleration[3] += 0.1f;
            }
        }
        else
        {
            backPressed = 0;
            acceleration[3] = 0;
        }

        if (leftRotate.IsPressed())
        {
            lRotatePressed = 1;
        } 
        else
        {
            lRotatePressed = 0;
        }
        if (rightRotate.IsPressed())
        {
            rRotatePressed = 1;
        }
        else
        {
            rRotatePressed = 0;
        }
        //Debug.Log("Direction: " + (forwardPressed - backPressed) + ", " + (rightPressed - leftPressed));
        //Vector3 movement = new Vector3((forwardPressed - backPressed) * Math.Abs(acceleration[0] - acceleration[3]), rb.velocity.y * Time.fixedDeltaTime, (rightPressed - leftPressed) * Math.Abs(acceleration[1] - acceleration[2]));
        //movement.y = rb.velocity.y;
        //rb.AddForce(movement);
       // rb.velocity = movement;
       // Debug.Log(movement + " |0-3|: " + Math.Abs(acceleration[0] - acceleration[3]) + " |1-2|: " + Math.Abs(acceleration[1] - acceleration[2]));
        //rb.AddForce(movement * Speed);

        float speedDifX = maxSpeed - Math.Abs(rb.velocity.x);
        float speedDifZ = maxSpeed - Math.Abs(rb.velocity.z);
        speedDifX *= accelerationRate;
        speedDifZ *= accelerationRate;
        Vector2 speedMod = new Vector2(speedDifX, speedDifZ);
        //speedMod.Normalize();

        float rotationMultY = rb.rotation.y;
        Vector2 rotationMult = new Vector2(Mathf.Abs(Mathf.Cos(rotationMultY)), Mathf.Abs(Mathf.Sin(rotationMultY)));
        rotationMult.Normalize();
        rotationMult *= forwardSpeedDifferenceOverStrafe;
        float rotationMultNum = Math.Abs(Vector2.Dot(speedMod, rotationMult));

        //Vector3 movement = new Vector3(speedDifX * (forwardPressed - backPressed), rb.velocity.y * Time.fixedDeltaTime, speedDifZ * (rightPressed - leftPressed));
        Vector3 movement = new Vector3((forwardPressed - backPressed), rb.velocity.y * Time.fixedDeltaTime, (rightPressed - leftPressed)) * (speedMod.magnitude + rotationMultNum);
        rb.AddForce(movement);
        //Debug.Log("MOVMENT FORCE ADDED: " + speedMod + " Movement: " + movement + "   " + speedMod.magnitude + "  " + rotationMultY);
        //Quaternion rot = rb.rotation;
        //rb.MoveRotation()


    }

}