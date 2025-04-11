/*****************************************************************************
// File Name :         BulletScript.cs
// Author :            Jack Fried
// Creation Date :     March 24, 2025
//
// Brief Description : Controls the player bullets.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //Setting variables
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float deleteTimer;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Scales speed to reasonable values
        speed /= 100;

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
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("MeleeEnemy")) //Damages and destroys when hitting a melee enemy
        {
            other.gameObject.GetComponent<MeleeEnemy>().CurrentHp -= damage;

            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("RangedEnemy")) //Damages and destroys when hitting a ranged enemy
        {
            other.gameObject.GetComponent<RangedEnemy>().CurrentHp -= damage;

            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("SuperRangedEnemy")) //Damages and destroys when hitting a super ranged enemy
        {
            other.gameObject.GetComponent<SuperRangedEnemy>().CurrentHp -= damage;

            Instantiate(hitEffect, transform.position, transform.rotation);
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
