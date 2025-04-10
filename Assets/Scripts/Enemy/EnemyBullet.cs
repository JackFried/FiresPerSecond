/*****************************************************************************
// File Name :         EnemyBullet.cs
// Author :            Jack Fried
// Creation Date :     March 27, 2025
//
// Brief Description : Controls the enemy bullets.
*****************************************************************************/

using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Setting variables
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float speed;
    [SerializeField] private float deleteTimer;

    private GameObject playerObject;
    private PlayerResources playerResources;


    /// The code to be called on initial object creation
    void Start()
    {
        //Scales speed to reasonable values
        speed /= 100;

        //Gets the player resources script through the player object
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerResources = playerObject.GetComponent<PlayerResources>();

        //Calls function to destroy the bullet over time
        StartCoroutine(BulletDecay());
    }

    /// <summary>
    /// The code being called every frame, in relation to delta time
    /// </summary>
    private void FixedUpdate()
    {
        //Moves in the facing direction at the given speed
        transform.position += (transform.forward * speed);
    }

    /// <summary>
    /// Bullet trigger collision reactions
    /// </summary>
    /// <param name="other"> The target collision </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) //Destroys when hitting general surfaces
        {
            //Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("PlayerBody")) //Damages the player and destroys self
        {
            //Instantiate(hitEffect, transform.position, transform.rotation);
            playerResources.TakeDamage();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Controls the bullet eventually despawning if it hits nothing
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletDecay()
    {
        yield return new WaitForSeconds(deleteTimer);
        Destroy(gameObject);
    }
}
