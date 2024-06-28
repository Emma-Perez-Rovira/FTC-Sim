using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slides : MonoBehaviour
{
    private Boolean slidesButton;
    [SerializeField]
    private GameObject outtake;
    [SerializeField]
    private GameObject[] slideSegments;
    private Boolean slidesDownButton;
    [SerializeField]
    private float maxExtension = 0.08f;
    [SerializeField]
    private float extensionSpeed = 0.0001f;
    [SerializeField]
    private float minExtension = 1.0f;
    private Boolean Extended = false;
    private int multiplyer = 1;
    private float outtakeOutFloat;
    private float outtakeInFloat;
    // Start is called before the first frame update
    public void OnSlidesUp(InputAction.CallbackContext ctx) => slidesButton = ctx.action.triggered;
    public void OnSlidesDown(InputAction.CallbackContext ctx) => slidesDownButton = ctx.action.triggered;
    public void OnOuttakeOut(InputAction.CallbackContext ctx) => outtakeOutFloat = ctx.ReadValue<float>();
    public void OnOuttakeIn(InputAction.CallbackContext ctx) => outtakeInFloat = -ctx.ReadValue<float>();
    private void Awake()
    {
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (slidesButton)
        {
            if (!Extended)
            {
                
                for (int i = 0; i < slideSegments.Length; i++)
                {
                    Boolean parsed = int.TryParse(slideSegments[i].tag, out multiplyer);
                    if(parsed)
                    if (slideSegments[i].transform.localPosition.z < maxExtension * multiplyer)
                        slideSegments[i].transform.localPosition = new Vector3(slideSegments[i].transform.localPosition.x, slideSegments[i].transform.localPosition.y, slideSegments[i].transform.localPosition.z + extensionSpeed);
                }

            }
            
        }
        if(slidesDownButton)
        {
            for (int i = 0; i < slideSegments.Length; i++)
            {
                Boolean parsed = int.TryParse(slideSegments[i].tag, out multiplyer);
                if (parsed)
                    if (slideSegments[i].transform.localPosition.z > minExtension)
                        slideSegments[i].transform.localPosition = new Vector3(slideSegments[i].transform.localPosition.x, slideSegments[i].transform.localPosition.y, slideSegments[i].transform.localPosition.z - extensionSpeed);
            }

        }
        if(Mathf.Abs(outtakeInFloat) > Mathf.Abs(outtakeOutFloat))
        {
            if (outtake.transform.localRotation.z > .50f)
            {
                outtake.transform.Rotate(5f * outtakeInFloat, 0, 0);
            }
        } else
        {
            if (outtake.transform.localRotation.z < .96f)
            {
                outtake.transform.Rotate(5f * outtakeOutFloat, 0, 0);
            }
        }
        Debug.Log(outtakeInFloat + ", " + outtakeOutFloat + ",, " + outtake.transform.localRotation.z);
    }
    public static float degreesToRad(float degrees)
    {
        float radians = (float)(Math.PI / 180) * degrees;
        return (radians);
    }
}
