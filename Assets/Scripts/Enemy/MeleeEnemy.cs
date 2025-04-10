/*****************************************************************************
// File Name :         MeleeEnemy.cs
// Author :            Jack Fried
// Creation Date :     March 29, 2025
//
// Brief Description : Controls the standard melee enemy.
*****************************************************************************/

using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemy : MonoBehaviour
{
    //Setting variables
    [SerializeField] private int startHp;
    private int currentHp;

    [SerializeField] private NavMeshAgent agent;

    private GameObject playerObject;
    private PlayerResources playerResources;

    [SerializeField] private GameObject destroyEffect;

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
            Destroy(gameObject);
        }
        else
        {
            //Track the player
            agent.SetDestination(playerObject.transform.position);
        }

        //Keep facing the player
        transform.LookAt(playerObject.transform.position);
    }
}
