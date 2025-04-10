/*****************************************************************************
// File Name :         WinPlatform.cs
// Author :            Jack Fried
// Creation Date :     March 31, 2025
//
// Brief Description : Controls the platform that lets you finish the level.
*****************************************************************************/

using UnityEngine;

public class WinPlatform : MonoBehaviour
{
    //Setting variables
    private GameObject playerObject;
    private PlayerResources playerResources;

    [SerializeField] private GameObject winOn;
    [SerializeField] private GameObject winOff;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Finds the player for reference
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerResources = playerObject.GetComponent<PlayerResources>();

        //Sets initial enabled conditions
        winOn.SetActive(false);
        winOff.SetActive(true);
    }

    /// <summary>
    /// The code being called every frame
    /// </summary>
    void Update()
    {
        //Controls if it can be interacted with or not (if there are no enemies left, it can be)
        if (playerResources.TotalEnemies == 0)
        {
            winOn.SetActive(true);
            winOff.SetActive(false);
        }
        else
        {
            winOn.SetActive(false);
            winOff.SetActive(true);
        }
    }
}
