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
    // Start is called before the first frame update
    public void OnSlidesUp(InputAction.CallbackContext ctx) => slidesButton = ctx.action.triggered;
    public void OnSlidesDown(InputAction.CallbackContext ctx) => slidesDownButton = ctx.action.triggered;
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
                outtake.transform.Rotate(50 * (slideSegments[0].transform.localPosition.z / maxExtension), 0, 0);
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
    }
}
