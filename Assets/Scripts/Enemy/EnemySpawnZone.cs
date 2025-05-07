/*****************************************************************************
// File Name :         EnemySpawnZone.cs
// Author :            Jack Fried
// Creation Date :     March 28, 2025
//
// Brief Description : Script for areas that spawn enemy groups when entered.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    //Setting variables
    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private List<GameObject> wallList = new List<GameObject>();
    [SerializeField] private float wallDelay;

    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private AudioClip spawnSfx;

    private bool isEnabled;


    /// <summary>
    /// Sets enabled on start to off by default
    /// </summary>
    void Start()
    {
        isEnabled = false;
    }

    /// <summary>
    /// The code being called every frame
    /// </summary>
    void Update()
    {
        //Check for if all enemies are cleared, then remove temporary walls and destroy self
        //(Destroys self in the remove walls function)
        if (enemyList.Count == 0)
        {
            if (isEnabled == true)
            {
                RemoveWalls();
                isEnabled = false;
            }
        }

        //Remove empty enemy list entries
        for (int i = enemyList.Count - 1; i > -1; i--) //Go backwards through the list to delete blank entries
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// When the player enters the area, enable all attached enemies and temporary walls
    /// </summary>
    /// <param name="other"> The target collision </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            if (isEnabled == false)
            {
                EnableEnemies();
                StartCoroutine(EnableWalls());
                AudioSource.PlayClipAtPoint(spawnSfx, other.gameObject.transform.position, 0.5f);
                isEnabled = true;
            }
        }
    }

    /// <summary>
    /// Enables all attached enemy objects from the list
    /// </summary>
    private void EnableEnemies()
    {
        for (int i = 0; i < enemyList.Count; i++) //Goes through each element of the list sequentially
        {
            enemyList[i].SetActive(true); //Enables current element in the list

            GameObject effect = Instantiate(spawnEffect);
            effect.transform.position = enemyList[i].transform.position;
        }
    }

    /// <summary>
    /// Enables all attached wall objects from the list
    /// </summary>
    /// <returns></returns>
    IEnumerator EnableWalls()
    {
        yield return new WaitForSeconds(wallDelay);
        for (int i = 0; i < wallList.Count; i++) //Goes through each element of the list sequentially
        {
            wallList[i].SetActive(true); //Enables current element in the list
        }
    }

    /// <summary>
    /// Destroys all walls and self afterwards
    /// </summary>
    private void RemoveWalls()
    {
        for (int i = 0; i < wallList.Count; i++) //Goes through each element of the list sequentially
        {
            Destroy(wallList[i]); //Destroys current element in the list
        }

        Destroy(gameObject);
    }
}
