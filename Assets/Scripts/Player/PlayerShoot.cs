/*****************************************************************************
// File Name :         PlayerMovement.cs
// Author :            Jack Fried
// Creation Date :     March 25, 2025
//
// Brief Description : Controls the shooting functionality of the player
                       object.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    //Setting variables
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerResources playerResources;

    [SerializeField] private GameObject gunObject;
    [SerializeField] private Transform gunOriginalPosition;
    [SerializeField] private Transform gunRecoilPosition;
    [SerializeField] private float recoilTime;
    [SerializeField] private GameObject reloadObject;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform shootLocation;
    [SerializeField] private GameObject referenceRotation;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float randomSpread;
    [SerializeField] private float randomSpreadAccurate;
    [SerializeField] private int bulletCount;
    [SerializeField] private int bulletCountAccurate;
    [SerializeField] private float pushForce;
    [SerializeField] private float horizontalInfluence;
    [SerializeField] private float verticalInfluence;

    private GameObject overlay;
    private PauseMenu pauseMenu;

    private InputAction shoot;

    [SerializeField] private AudioClip shootSfx;


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Finds the pause menu script through the HUD object
        overlay = GameObject.FindGameObjectWithTag("HUD");
        pauseMenu = overlay.GetComponent<PauseMenu>();

        //Setting up player inputs
        playerInput.currentActionMap.Enable();
        shoot = playerInput.currentActionMap.FindAction("Shoot");

        shoot.started += Shoot_started;
    }

    /// <summary>
    /// Spawns bullets at the given position and angle on shoot input, with some inaccuracy, and uses ammo
    /// </summary>
    /// <param name="obj"> Input </param>
    private void Shoot_started(InputAction.CallbackContext obj)
    {
        //If the game is paused, prevents the gun from firing
        if (pauseMenu.IsPaused == false)
        {
            if (playerResources.CurrentAmmo > 0)
            {
                //Has the gun recoil on fire
                StartCoroutine(GunRecoil());

                //Spends 1 ammo, from the player resources script, and starts a reload
                //(The timing of the reload is handled in the reload object itself)
                playerResources.CurrentAmmo -= 1;
                Instantiate(reloadObject);

                //Fires inaccurate bullets (more spread)
                for (int i = 0; i < bulletCount; i++)
                {
                    //Get random thresholds
                    float randX = referenceRotation.transform.rotation.x + Random.Range(-randomSpread, randomSpread);
                    float randY = referenceRotation.transform.rotation.y + Random.Range(-randomSpread, randomSpread);
                    Quaternion randomRotation = Quaternion.Euler(randX + verticalInfluence, randY + horizontalInfluence, 0);

                    //Spawns the bullet with the given angle data
                    Instantiate(bullet, shootLocation.transform.position,
                        referenceRotation.transform.rotation * randomRotation);
                }

                //Fires accurate bullets (less spread)
                for (int i = 0; i < bulletCountAccurate; i++)
                {
                    //Get random thresholds
                    float randX = referenceRotation.transform.rotation.x + Random.Range(-randomSpreadAccurate, randomSpreadAccurate);
                    float randY = referenceRotation.transform.rotation.y + Random.Range(-randomSpreadAccurate, randomSpreadAccurate);
                    Quaternion randomRotation = Quaternion.Euler(randX + verticalInfluence, randY + horizontalInfluence, 0);

                    //Spawns the bullet with the given angle data
                    Instantiate(bullet, shootLocation.transform.position,
                        referenceRotation.transform.rotation * randomRotation);
                }

                //If not on the ground, give reverse force
                if (playerMovement.IsGrounded == false)
                {
                    rb.AddForce(-referenceRotation.transform.forward * pushForce, ForceMode.Impulse);
                }

                AudioSource.PlayClipAtPoint(shootSfx, transform.position, 1f);
            }
        }
    }

    /// <summary>
    /// Resets inputs on destroy
    /// </summary>
    private void OnDestroy()
    {
        shoot.started -= Shoot_started;
    }

    /// <summary>
    /// Causes the recoil motion on the gun by shifting its location temporarily
    /// </summary>
    /// <returns></returns>
    IEnumerator GunRecoil()
    {
        gunObject.transform.position = gunRecoilPosition.transform.position;
        yield return new WaitForSeconds(recoilTime);
        gunObject.transform.position = gunOriginalPosition.transform.position;
    }
}
