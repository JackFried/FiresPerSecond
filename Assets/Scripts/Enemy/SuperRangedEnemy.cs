/*****************************************************************************
// File Name :         SuperRangedEnemy.cs
// Author :            Jack Fried
// Creation Date :     April 11, 2025
//
// Brief Description : Controls the stronger ranged enemy.
*****************************************************************************/

using System.Collections;
using UnityEngine;

public class SuperRangedEnemy : MonoBehaviour
{
    //Setting variables
    [SerializeField] private int startHp;
    private int currentHp;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireDelay;
    [SerializeField] private int fireCount;
    [SerializeField] private float voleyDelay;

    private GameObject playerObject;
    private GameObject playerHead;
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
        playerHead = GameObject.FindGameObjectWithTag("PlayerHead");
        playerResources = playerObject.GetComponent<PlayerResources>();

        //Calls looping shoot function on spawn
        StartCoroutine(FireVoley());
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
        transform.LookAt(playerHead.transform.position);
    }

    /// <summary>
    /// Fires the burst of bullets
    /// </summary>
    /// <returns></returns>
    IEnumerator FireVoley()
    {
        yield return new WaitForSeconds(fireDelay);

        for (int i = 0; i < fireCount; i++)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(shootSfx, transform.position, 1f);
            yield return new WaitForSeconds(voleyDelay);
        }

        //Loops function
        StartCoroutine(FireVoley());
    }
}
