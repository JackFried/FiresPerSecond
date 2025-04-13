/*****************************************************************************
// File Name :         AcidScript.cs
// Author :            Jack Fried
// Creation Date :     April 11, 2025
//
// Brief Description : Causes the acid obstacle to deal damage to the player.
*****************************************************************************/

using UnityEngine;

public class AcidScript : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float bounceHeight;
    private GameObject playerObject;
    private Rigidbody playerRb;
    private PlayerResources playerResources;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Finds the player for reference
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRb = playerObject.GetComponent<Rigidbody>();
        playerResources = playerObject.GetComponent<PlayerResources>();
    }

    /// <summary>
    /// Acid trigger collision reactions
    /// </summary>
    /// <param name="other"> The target collision </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody")) //Damages the player and causes them to bounce
        {
            playerResources.TakeDamage();
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(transform.up * bounceHeight, ForceMode.Impulse);
        }
    }
}
