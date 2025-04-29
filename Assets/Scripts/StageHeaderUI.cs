/*****************************************************************************
// File Name :         StageHeaderUI.cs
// Author :            Jack Fried
// Creation Date :     April 29, 2025
//
// Brief Description : Controls the beginning of stage name UI object.
*****************************************************************************/

using System.Collections;
using UnityEngine;

public class StageHeaderUI : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float speedIncrease;
    private float currentSpeed;
    [SerializeField] private float moveDelay;

    private bool isMoving;
    private float targetY;

    private GameObject overlay;
    private PauseMenu pauseMenu;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Sets initial state
        currentSpeed = 0;
        isMoving = false;
        targetY = transform.position.y + 300;

        //Calls the wait function
        StartCoroutine(StartMoving());

        //Finds the pause menu script through the HUD object
        overlay = GameObject.FindGameObjectWithTag("HUD");
        pauseMenu = overlay.GetComponent<PauseMenu>();
    }

    /// <summary>
    /// The code called every frame, in relation to delta time
    /// </summary>
    void FixedUpdate()
    {
        //Starts when the wait function finishes
        if (isMoving == true)
        {
            //If the object isn't off-screen yet, start moving up, getting faster over time (only while unpaused)
            if (transform.position.y < targetY)
            {
                if (pauseMenu.IsPaused == false)
                {
                    transform.position = transform.position + new Vector3(0, currentSpeed, 0);
                    currentSpeed += speedIncrease;
                }
            }
            else //Once it IS off-screen, destroy it
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(moveDelay);
        isMoving = true;
    }
}
