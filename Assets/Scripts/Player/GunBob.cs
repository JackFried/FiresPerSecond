/*****************************************************************************
// File Name :         GunBob.cs
// Author :            Jack Fried
// Creation Date :     May 6, 2025
//
// Brief Description : Causes the gun to have a bob effect (to simulate
                       walking) while the player is moving.
*****************************************************************************/

using UnityEngine;

public class GunBob : MonoBehaviour
{
    //Setting variables
    [SerializeField] private GameObject lowPoint;
    [SerializeField] private GameObject highPoint;

    [SerializeField] private float riseSpeed;
    [SerializeField] private float lowerSpeed;
    private float lowerSpeedStart;
    [SerializeField] private float lowerSpeedIncrease;

    private GameObject playerObject;
    private PlayerMovement playerMovement;

    private bool isMoving;
    private bool isMovingUp;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Finds the player for reference
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerObject.GetComponent<PlayerMovement>();

        //Sets initial movement states
        isMoving = false;
        isMovingUp = true;
        lowerSpeedStart = lowerSpeed;
    }

    /// <summary>
    /// Controls if the bob can start (has to not be currently happening at the moment)
    /// The player has to be moving and grounded
    /// </summary>
    void Update()
    {
        if (playerMovement.PlayerInputGlobal != Vector2.zero && playerMovement.IsGrounded == true)
        {
            if (isMoving == false)
            {
                isMoving = true;
            }    
        }
    }

    /// <summary>
    /// The bob functionality
    /// </summary>
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            if (isMovingUp == true) //While the gun is moving towards the higher position
            {
                //Move towards the higher position
                transform.position = Vector3.MoveTowards(transform.position, highPoint.transform.position, riseSpeed);

                if (transform.position == highPoint.transform.position) //If at the target, start going down
                {
                    lowerSpeed = lowerSpeedStart;
                    isMovingUp = false;
                }
            }
            else //While the gun is moving towards the lower position
            {
                //Move towards the lower position, gaining speed over time
                transform.position = Vector3.MoveTowards(transform.position, lowPoint.transform.position, lowerSpeed);
                lowerSpeed += lowerSpeedIncrease;

                if (transform.position == lowPoint.transform.position) //If at the target, reset
                {
                    isMoving = false;
                    isMovingUp = true;
                }
            }
        }
    }
}
