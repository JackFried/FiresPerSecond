/*****************************************************************************
// File Name :         WinSetTime.cs
// Author :            Jack Fried
// Creation Date :     April 8, 2025
//
// Brief Description : Gets the level time and displays it on the win screen.
                       Also keeps track of records for the given stage.
*****************************************************************************/

using System;
using TMPro;
using UnityEngine;

public class WinSetTime : MonoBehaviour
{
    //Setting variables
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject newRecordText;
    private float savedTime;

    private int currentLevel;

    private GameObject dataObject;
    private PersistentData persistentData;

    private float comparisonTime;


    /// <summary>
    /// The code being called on the first frame
    /// </summary>
    void Awake()
    {
        //Finds the persistent data object, to get the previous level time
        dataObject = GameObject.FindGameObjectWithTag("PersistentData");
        persistentData = dataObject.GetComponent<PersistentData>();

        //Sets previous stage time to locally saved time
        savedTime = persistentData.BestTime;

        //Returns a time interval in seconds
        TimeSpan time = TimeSpan.FromSeconds(savedTime);
        //Formats time variable into specified string structure
        timeText.text = time.ToString(@"mm\:ss\:ff");

        //Gets level number, for finding best time data
        currentLevel = persistentData.CurrentLevelInt;

        //Allows the data object to be destroyed
        persistentData.CanDelete = true;


        //Checks the record of the current stage
        //If current time is better than the record, overwrite it
        if (currentLevel == 1)
        {
            //Gets the record
            comparisonTime = PlayerPrefs.GetFloat("Stage1Time");

            if (savedTime < comparisonTime || comparisonTime == 0) //Compares the record
            {
                //Sets new record
                PlayerPrefs.SetFloat("Stage1Time", savedTime);
                newRecordText.SetActive(true);
            }
        }
        else if (currentLevel == 2)
        {
            //Gets the record
            comparisonTime = PlayerPrefs.GetFloat("Stage2Time");

            if (savedTime < comparisonTime || comparisonTime == 0) //Compares the record
            {
                //Sets new record
                PlayerPrefs.SetFloat("Stage2Time", savedTime);
                newRecordText.SetActive(true);
            }
        }
        else if (currentLevel == 3)
        {
            //Gets the record
            comparisonTime = PlayerPrefs.GetFloat("Stage3Time");

            if (savedTime < comparisonTime || comparisonTime == 0) //Compares the record
            {
                //Sets new record
                PlayerPrefs.SetFloat("Stage3Time", savedTime);
                newRecordText.SetActive(true);
            }
        }
    }
}
