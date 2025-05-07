/*****************************************************************************
// File Name :         CameraController.cs
// Author :            Jack Fried
// Creation Date :     March 15, 2025
//
// Brief Description : Has the camera rotate based on mouse input, featuring
                       vertical clamps.
*****************************************************************************/

using UnityEngine;
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

    private float zRotation;
    [SerializeField] private float zRotSpeed;
    [SerializeField] private float zRotMax;

    [SerializeField] private bool isMainCam;

    private GameObject playerObject;
    private PlayerMovement playerMovement;


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

        //Finds the player for reference
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerObject.GetComponent<PlayerMovement>();

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

        //Controls the tilt of the camera (based on left and right player movement; only while the game is unpaused)
        if (playerMovement.PauseMenuObj.IsPaused == false)
        {
            if (playerMovement.PlayerMovementVar.x != 0)
            {
                if (playerMovement.PlayerMovementVar.x < 0) //If moving left, tilt left
                {
                    if (zRotation > -zRotMax)
                    {
                        zRotation -= zRotSpeed;
                    }
                    else
                    {
                        zRotation = -zRotMax;
                    }
                }
                else if (playerMovement.PlayerMovementVar.x > 0) //If moving right, tilt right
                {
                    if (zRotation < zRotMax)
                    {
                        zRotation += zRotSpeed;
                    }
                    else
                    {
                        zRotation = zRotMax;
                    }
                }
            }
            else //If not moving left or right, tilt back to the default rotation
            {
                if (zRotation < 0)
                {
                    zRotation += zRotSpeed;
                }
                else if (zRotation > 0)
                {
                    zRotation -= zRotSpeed;
                }
            }
        }

        //Clamps vertical camera angle to stop straight up and straight down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (isMainCam == true) //Checks if this is the primary camera
        {
            //Rotate camera orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
            //Rotate player vertical orientation
            playerOrient.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            //Rotate camera orientation (back view)
            transform.rotation = Quaternion.Euler(xRotation, yRotation + 180, zRotation);
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
