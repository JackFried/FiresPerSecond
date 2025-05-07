/*****************************************************************************
// File Name :         PauseMenu.cs
// Author :            Jack Fried
// Creation Date :     March 31, 2025
//
// Brief Description : Controls the operation of the pause screen.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Setting Variables
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip unpauseSound;
    private bool isPaused;

    private GameObject persistentData;
    private GameObject playerObject;

    public bool IsPaused { get => isPaused; set => isPaused = value; }


    /// <summary>
    /// Sets initial unpaused conditions on start
    /// </summary>
    void Start()
    {
        IsPaused = false;
        pauseScreen.SetActive(false);

        //Find persistent data object
        persistentData = GameObject.FindGameObjectWithTag("PersistentData");

        //Finds the player for reference
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }


    /// <summary>
    /// Pauses the game on pause input
    /// </summary>
    /// <param name="iValue"> The input read </param>
    void OnPause(InputValue iValue)
    {
        PauseState();
    }

    /// <summary>
    /// Manages the pausing and unpausing of the game
    /// </summary>
    public void PauseState()
    {
        if (IsPaused == false)
        {
            AudioSource.PlayClipAtPoint(pauseSound, playerObject.transform.position, 1f);

            //Make the pause screen appear and stop the game from running
            pauseScreen.SetActive(true);
            IsPaused = true;
            Time.timeScale = 0;

            //Unlocks the cursor and shows it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            //Make the pause screen disappear and continue the game's running
            pauseScreen.SetActive(false);
            IsPaused = false;
            Time.timeScale = 1;

            //Locks the cursor to the middle of the screen and hides it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            AudioSource.PlayClipAtPoint(unpauseSound, playerObject.transform.position, 1f);
        }
    }

    /// <summary>
    /// Reloads the current level scene
    /// </summary>
    public void RestartLevel()
    {
        //Deletes persistent data object
        Destroy(persistentData);

        PauseState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Brings the game to the main menu
    /// </summary>
    public void ToMenu()
    {
        //Deletes persistent data object
        Destroy(persistentData);

        PauseState();
        SceneManager.LoadScene("MainMenu");
    }
}
