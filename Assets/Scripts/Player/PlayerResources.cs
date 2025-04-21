/*****************************************************************************
// File Name :         PlayerResources.cs
// Author :            Jack Fried
// Creation Date :     March 25, 2025
//
// Brief Description : Manages player resources, like health and ammo.
*****************************************************************************/

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    //Setting variables
    //Health and ammo variables
    [SerializeField] private int maxHp;
    [SerializeField] private Image hpUI;
    [SerializeField] private int maxAmmo;
    [SerializeField] private TMP_Text enemyCount;
    [SerializeField] private int totalEnemies;
    private int currentHp;
    private int currentAmmo;

    [SerializeField] private Animator hpAnim;
    [SerializeField] private Animator ammoAnim;

    [SerializeField] private GameObject ammo3Gun;
    [SerializeField] private GameObject ammo2Gun;
    [SerializeField] private GameObject ammo1Gun;
    [SerializeField] private GameObject ammo0Gun;

    private float currentIframes;
    [SerializeField] private float iframeSecOnDamage;
    [SerializeField] private float hpFlashDelay;

    private GameObject persistentData;


    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public int TotalEnemies { get => totalEnemies; set => totalEnemies = value; }


    /// <summary>
    /// The code to be called on initial object creation
    /// </summary>
    void Start()
    {
        //Setting up initial stats
        CurrentHp = MaxHp;
        CurrentAmmo = MaxAmmo;
        currentIframes = 0;

        //Find persistent data object
        persistentData = GameObject.FindGameObjectWithTag("PersistentData");
    }

    /// <summary>
    /// The code being called every frame
    /// </summary>
    void Update()
    {
        //Updates ammo HUD element to reflect current health
        //The float at the end of each Play function is arbitrary but roughly links to the desired animation frame
        if (CurrentHp == 3)
        {
            hpAnim.Play("HealthAnim", 0, 0f);
        }
        else if (CurrentHp == 2)
        {
            hpAnim.Play("HealthAnim", 0, 0.5f);
        }
        else if (CurrentHp == 1)
        {
            hpAnim.Play("HealthAnim", 0, 0.75f);
        }
        else
        {
            hpAnim.Play("HealthAnim", 0, 0.9f);
        }

        //Updates ammo HUD element to reflect current ammo
        //The float at the end of each Play function is arbitrary but roughly links to the desired animation frame
        if (CurrentAmmo == 3)
        {
            ammoAnim.Play("AmmoAnim", 0, 0f);

            //Sets the indicator that's on the gun itself
            ammo3Gun.SetActive(true);
            ammo2Gun.SetActive(false);
            ammo1Gun.SetActive(false);
            ammo0Gun.SetActive(false);
        }
        else if (CurrentAmmo == 2)
        {
            ammoAnim.Play("AmmoAnim", 0, 0.5f);

            //Sets the indicator that's on the gun itself
            ammo3Gun.SetActive(false);
            ammo2Gun.SetActive(true);
            ammo1Gun.SetActive(false);
            ammo0Gun.SetActive(false);
        }
        else if (CurrentAmmo == 1)
        {
            ammoAnim.Play("AmmoAnim", 0, 0.75f);

            //Sets the indicator that's on the gun itself
            ammo3Gun.SetActive(false);
            ammo2Gun.SetActive(false);
            ammo1Gun.SetActive(true);
            ammo0Gun.SetActive(false);
        }
        else
        {
            ammoAnim.Play("AmmoAnim", 0, 0.9f);

            //Sets the indicator that's on the gun itself
            ammo3Gun.SetActive(false);
            ammo2Gun.SetActive(false);
            ammo1Gun.SetActive(false);
            ammo0Gun.SetActive(true);
        }

        //Controls losing and restarting the level
        if (CurrentHp <= 0)
        {
            //Reloads the current scene
            SceneManager.LoadScene("LevelLose");
        }
        if (CurrentHp > 3)
        {
            CurrentHp = 3;
        }

        //Controls iframe data (goes down on a per-second basis)
        if (currentIframes > 0)
        {
            currentIframes -= (1 * Time.deltaTime);
        }
        else
        {
            currentIframes = 0;
        }

        //Updates enemy counter
        enemyCount.text = TotalEnemies.ToString();
    }

    /// <summary>
    /// If not in iframe state, take damage
    /// </summary>
    public void TakeDamage()
    {
        if (currentIframes <= 0)
        {
            CurrentHp -= 1;
            currentIframes = iframeSecOnDamage;
            StartCoroutine(UIFlash()); //Run iframe display animation
        }
    }

    /// <summary>
    /// Causes the HP UI element to flash when in iframes
    /// </summary>
    /// <returns></returns>
    IEnumerator UIFlash()
    {
        while (currentIframes > 0)
        {
            if (hpUI.enabled == true)
            {
                hpUI.enabled = false;
            }
            else
            {
                hpUI.enabled = true;
            }

            yield return new WaitForSeconds(hpFlashDelay);
        }

        //Re-enables the element after animation finishes
        hpUI.enabled = true;
    }
}
