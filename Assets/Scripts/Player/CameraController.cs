/*****************************************************************************
// File Name :         CameraController.cs
// Author :            Jack Fried
// Creation Date :     March 15, 2025
//
// Brief Description : Has the camera rotate based on mouse input, featuring
                       vertical clamps.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;
    [SerializeField] private Transform playerOrient;

    [SerializeField] private PlayerInput mouseInput;
    private InputAction mouseX;
    private InputAction mouseY;

    private float currentXIn;
    private float currentYIn;
    private float xRotation;
    private float yRotation;

    [SerializeField] private bool isMainCam;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Setting up mouse inputs
        mouseInput.currentActionMap.Enable();
        mouseX = mouseInput.currentActionMap.FindAction("Horizontal");
        mouseY = mouseInput.currentActionMap.FindAction("Vertical");

        mouseX.started += MouseX_started;
        mouseX.canceled += MouseX_canceled;
        mouseY.started += MouseY_started;
        mouseY.canceled += MouseY_canceled;

        if (isMainCam == true) //Checks if this is the primary camera
        {
            //Locks the cursor to the middle of the screen and hides it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    /// <summary>
    /// Read horizontal mouse input
    /// </summary>
    /// <param name="obj"> Input taken from </param>
    private void MouseX_started(InputAction.CallbackContext obj)
    {
        currentXIn = obj.ReadValue<float>() * Time.deltaTime * xSensitivity;
    }

    /// <summary>
    /// Cancel horizontal mouse input read on no input
    /// </summary>
    /// <param name="obj"> Input taken from </param>
    private void MouseX_canceled(InputAction.CallbackContext obj)
    {
        currentXIn = 0f;
    }

    /// <summary>
    /// Read vertical mouse input
    /// </summary>
    /// <param name="obj"> Input taken from </param>
    private void MouseY_started(InputAction.CallbackContext obj)
    {
        currentYIn = obj.ReadValue<float>() * Time.deltaTime * ySensitivity;
    }

    /// <summary>
    /// Cancel vertical mouse input read on no input
    /// </summary>
    /// <param name="obj"> Input taken from </param>
    private void MouseY_canceled(InputAction.CallbackContext obj)
    {
        currentYIn = 0f;
    }

    /// <summary>
    /// Controls rotation every frame
    /// </summary>
    void Update()
    {
        //Control mouse input
        yRotation += currentXIn;
        xRotation -= currentYIn;

        //Clamps vertical camera angle to stop straight up and straight down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (isMainCam == true) //Checks if this is the primary camera
        {
            //Rotate camera orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            //Rotate player vertical orientation
            playerOrient.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            //Rotate camera orientation (back view)
            transform.rotation = Quaternion.Euler(xRotation, yRotation + 180, 0);
        }
    }


    /// <summary>
    /// Resets inputs on destroy
    /// </summary>
    private void OnDestroy()
    {
        mouseX.started -= MouseX_started;
        mouseX.canceled -= MouseX_canceled;
        mouseY.started -= MouseY_started;
        mouseY.canceled -= MouseY_canceled;
    }
}
