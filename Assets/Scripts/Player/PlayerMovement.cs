/*****************************************************************************
// File Name :         PlayerMovement.cs
// Author :            Jack Fried
// Creation Date :     March 15, 2025
//
// Brief Description : Controls the movement of the player object.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //General variables
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;
    private Vector3 playerMovement;
    private Vector3 moveDir;
    [SerializeField] private Camera mainCamera;

    //Drag-related variables
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDrag;
    private bool isGrounded;

    //Jump-related variables
    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;
    private bool canJump;

    //FOV change variables
    [SerializeField] private float maxFovChange;
    [SerializeField] private float fovChangeOT;
    [SerializeField] private float fovTargetScale;
    [SerializeField] private float fovReturnMult;
    private float currentFovChange;
    private float initialFov;
    private float targetFov;

    //Audio clips
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip healSfx;

    private GameObject overlay;
    private PauseMenu pauseMenu;

    private PlayerResources playerResources;

    

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Setting up player inputs
        playerInput.currentActionMap.Enable();

        //Finds the resources script
        playerResources = gameObject.GetComponent<PlayerResources>();

        //Finds the pause menu script through the HUD object
        overlay = GameObject.FindGameObjectWithTag("HUD");
        pauseMenu = overlay.GetComponent<PauseMenu>();

        //Sets the starting FOV
        initialFov = mainCamera.fieldOfView;
        targetFov = 0;
        currentFovChange = 0;
    }


    /// <summary>
    /// Read movement inputs and causes the player to move
    /// </summary>
    /// <param name="iValue"> The input read </param>
    void OnMove(InputValue iValue)
    {
        Vector2 inputMovementValue = iValue.Get<Vector2>(); //Reads the input value

        //X and Y input are applied to player X and Z
        playerMovement.x = inputMovementValue.x;
        playerMovement.z = inputMovementValue.y;
    }

    /// <summary>
    /// Reads jump input and causes the player to jump
    /// </summary>
    void OnJump()
    {
        if (canJump == true)
        {
            //Reset Y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //Apply upwards force
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            AudioSource.PlayClipAtPoint(jumpSfx, transform.position, 1f);

            canJump = false;
        }
    }


    /// <summary>
    /// The code being called every frame
    /// </summary>
    void Update()
    {
        SpeedControl();

        //Calculates movement direction based on the camera's direction
        moveDir = orientation.forward * playerMovement.z + orientation.right * playerMovement.x;

        //Check for ground using raycasting, using half the player's height plus a little more
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);

        //Air-related processes
        if (IsGrounded == true)
        {
            //Drag control (if on the ground, apply the drag; otherwise don't)
            rb.drag = groundDrag;
            canJump = true; //When on the ground, also reset the jump


            if (pauseMenu.IsPaused == false)
            {
                //FOV stuff (grounded)
                //If FOV is above the initial value, rapidly decrease it based on the over-time speed
                if (currentFovChange > 0)
                {
                    currentFovChange -= fovChangeOT * fovReturnMult;
                }
                else
                {
                    currentFovChange = 0;
                }
                mainCamera.fieldOfView = (initialFov + currentFovChange);
            }
        }
        else
        {
            //Drag control (if on the ground, apply the drag; otherwise don't)
            rb.drag = 0;
            canJump = false; //When off the ground, disable jumping


            if (pauseMenu.IsPaused == false)
            {
                //FOV stuff (mid-air)
                //Calculates total current horizontal magnitude, used as a modifier for FOV change
                float playerMoveAverage = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);

                //As player move speed increases, so does the target FOV change (to a cap)
                if (targetFov > maxFovChange)
                {
                    targetFov = maxFovChange;
                }
                else
                {
                    targetFov = (playerMoveAverage * fovTargetScale);
                }

                //Have the FOV change over time based on a predetermined rate, to eventually match the target FOV
                if (currentFovChange < targetFov)
                {
                    currentFovChange += fovChangeOT;
                }
                else if (currentFovChange > targetFov)
                {
                    currentFovChange -= fovChangeOT;
                }
                mainCamera.fieldOfView = (initialFov + currentFovChange);
            }
        }
    }

    /// <summary>
    /// The code being called every frame, in relation to delta time
    /// </summary>
    private void FixedUpdate()
    {
        //Ground vs. air movement
        if (IsGrounded == true)
        {
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDir.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }
    }

    /// <summary>
    /// Caps movement speed
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //Current flat velocity

        //Limiting current velocity based on the movement speed as a max
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }    
    }

    /// <summary>
    /// Controls the player taking contact damage from the various enemy types
    /// </summary>
    /// <param name="collision"> The target collision </param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("RangedEnemy") || collision.gameObject.CompareTag("MeleeEnemy"))
        {
            playerResources.TakeDamage();
        }
    }

    /// <summary>
    /// Controls what happens when the player interacts with the win platform
    /// </summary>
    /// <param name="collision"> The target collision </param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WinOn"))
        {
            SceneManager.LoadScene("LevelWin");
        }
        else if (collision.gameObject.CompareTag("FinalWin")) //Win platform on Stage 3
        {
            SceneManager.LoadScene("LevelWinFinal");
        }
    }

    /// <summary>
    /// Controls what happens when the player interacts with a given trigger
    /// </summary>
    /// <param name="other"> The target collision </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heart")) //Heart Pick-up (heals)
        {
            if (playerResources.CurrentHp < 3) //If not at max HP, restore 1 health and destroy
            {
                playerResources.CurrentHp += 1;
                AudioSource.PlayClipAtPoint(healSfx, transform.position, 1f);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Instakill")) //Out-of-bounds instakill
        {
            playerResources.CurrentHp = 0;
        }
    }
}
