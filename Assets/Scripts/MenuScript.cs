/*****************************************************************************
// File Name :         MenuScript.cs
// Author :            Jack Fried
// Creation Date :     March 31, 2025
//
// Brief Description : Generic controller script for all menuing.
*****************************************************************************/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //Setting variables
    [SerializeField] private bool isMainMenu;

    //Used by the level select screen to load records
    [SerializeField] private bool loadSaveData;
    [SerializeField] private TMP_Text Stage1RecordText;
    [SerializeField] private TMP_Text Stage2RecordText;
    [SerializeField] private TMP_Text Stage3RecordText;


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

        if (loadSaveData == true)
        {
            LoadTimes();
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

    /// <summary>
    /// Retrieves stage record times and displays them (for the level select screen only)
    /// </summary>
    private void LoadTimes()
    {
        //Gets the record
        float stage1LoadedTime = PlayerPrefs.GetFloat("Stage1Time");
        //Returns a time interval in seconds
        TimeSpan time1 = TimeSpan.FromSeconds(stage1LoadedTime);
        //Formats time variable into specified string structure
        Stage1RecordText.text = time1.ToString(@"mm\:ss\:ff");

        //Gets the record
        float stage2LoadedTime = PlayerPrefs.GetFloat("Stage2Time");
        //Returns a time interval in seconds
        TimeSpan time2 = TimeSpan.FromSeconds(stage2LoadedTime);
        //Formats time variable into specified string structure
        Stage2RecordText.text = time2.ToString(@"mm\:ss\:ff");

        //Gets the record
        float stage3LoadedTime = PlayerPrefs.GetFloat("Stage3Time");
        //Returns a time interval in seconds
        TimeSpan time3 = TimeSpan.FromSeconds(stage3LoadedTime);
        //Formats time variable into specified string structure
        Stage3RecordText.text = time3.ToString(@"mm\:ss\:ff");
    }
}
