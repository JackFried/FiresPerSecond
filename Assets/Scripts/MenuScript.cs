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
    [SerializeField] private bool isMainMenu;
    [SerializeField] private bool cursorVisible;

    [SerializeField] private string thisLevel;

    private GameObject dataObject;
    private PersistentData persistentData;


    /// <summary>
    /// Called on the first frame
    /// </summary>
    void Awake()
    {
        //Unlocks the cursor and shows it
        if (cursorVisible == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isMainMenu == false)
        {
            //Finds the persistent data object, to get the previous level time
            dataObject = GameObject.FindGameObjectWithTag("PersistentData");
            persistentData = dataObject.GetComponent<PersistentData>();

            thisLevel = persistentData.CurrentLevel;
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
    /// Replays the current level, using the persistent data object
    /// </summary>
    public void ReplayLevel()
    {
        SceneManager.LoadScene(thisLevel);
    }

    /// <summary>
    /// Loads the next level, using the persistent data object
    /// </summary>
    public void NextLevel()
    {
        if (thisLevel == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (thisLevel == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
