/*****************************************************************************
// File Name :         TeleporterScript.cs
// Author :            Jack Fried
// Creation Date :     April 10, 2025
//
// Brief Description : Causes the player to teleport to a given location on
                       contact with attached object.
*****************************************************************************/

using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    //Setting variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject toLocation;


    /// <summary>
    /// Teleports player on contact
    /// </summary>
    /// <param name="other"> The target collision </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            player.transform.position = toLocation.transform.position;
            Debug.Log(player.transform.position);
        }
    }
}
