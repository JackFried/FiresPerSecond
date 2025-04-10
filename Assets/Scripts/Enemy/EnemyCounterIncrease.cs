/*****************************************************************************
// File Name :         EnemyCounterIncrease.cs
// Author :            Jack Fried
// Creation Date :     March 28, 2025
//
// Brief Description : Used solely for ticking up the total enemy count
                       externally from the enemies themselves.
*****************************************************************************/

using UnityEngine;

public class EnemyCounterIncrease : MonoBehaviour
{
    //Setting variables
    private GameObject playerObject;
    private PlayerResources playerResources;


    /// <summary>
    /// The code being called on start
    /// </summary>
    void Start()
    {
        //Finds the resources script through the player
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerResources = playerObject.GetComponent<PlayerResources>();

        //Increases total enemy count
        playerResources.TotalEnemies += 1;
    }
}
