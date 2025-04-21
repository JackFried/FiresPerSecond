/*****************************************************************************
// File Name :         RangedEnemy.cs
// Author :            Jack Fried
// Creation Date :     March 27, 2025
//
// Brief Description : Controls the standard ranged enemy.
*****************************************************************************/

using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    //Setting variables
    [SerializeField] private int startHp;
    private int currentHp;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireDelay;

    private GameObject playerObject;
    private PlayerResources playerResources;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private AudioClip deathSfx;

    public int CurrentHp { get => currentHp; set => currentHp = value; }


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Sets initial HP
        CurrentHp = startHp;

        //Finds the player for reference
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerResources = playerObject.GetComponent<PlayerResources>();

        //Calls looping shoot function on spawn
        StartCoroutine(FireBullet());
    }

    /// <summary>
    /// The code being called every frame
    /// </summary>
    void Update()
    {
        //Once at 0 HP (or lower), destroy self
        if (currentHp < 0)
        {
            playerResources.TotalEnemies -= 1;
            GameObject effect = Instantiate(destroyEffect);
            effect.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(deathSfx, transform.position, 1f);
            Destroy(gameObject);
        }

        //Keep facing the player
        transform.LookAt(playerObject.transform.position);
    }

    IEnumerator FireBullet()
    {
        yield return new WaitForSeconds(fireDelay);
        Instantiate(bullet, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(shootSfx, transform.position, 1f);

        //Loops function
        StartCoroutine(FireBullet());
    }
}
