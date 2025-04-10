/*****************************************************************************
// File Name :         MenuScript.cs
// Author :            Jack Fried
// Creation Date :     March 31, 2025
//
// Brief Description : Generic controller script for all menuing.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //Setting variables
    [SerializeField] private bool cursorVisible;


    /// <summary>
    /// Unlocks the cursor and shows it on start
    /// </summary>
    void Start()
    {
        if (cursorVisible == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// Loads the specified scene
    /// </summary>
    /// <param name="sceneName"> Scene name to load </param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
