/*****************************************************************************
// File Name :         ReloadScript.cs
// Author :            Jack Fried
// Creation Date :     March 25, 2025
//
// Brief Description : Manages player resources, like health and ammo.
*****************************************************************************/

using System.Collections;
using UnityEngine;

public class ReloadScript : MonoBehaviour
{
    //Setting variables
    private GameObject playerObject;
    private PlayerResources playerResources;

    [SerializeField] private float reloadTime;

    [SerializeField] private AudioClip reloadSfx;


    /// <summary>
    /// Starts one reload on spawn
    /// </summary>
    void Start()
    {
        //Gets the player resources script through the player object
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerResources = playerObject.GetComponent<PlayerResources>();

        //Calls the reload function
        StartCoroutine(Reload());
    }

    /// <summary>
    /// Gives ammo after a delay and deletes self
    /// </summary>
    /// <returns></returns>
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        playerResources.CurrentAmmo += 1;
        AudioSource.PlayClipAtPoint(reloadSfx, playerObject.transform.position, 0.5f);
        Destroy(gameObject);
    }
}
