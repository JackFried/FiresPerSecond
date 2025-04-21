/*****************************************************************************
// File Name :         WinSetTime.cs
// Author :            Jack Fried
// Creation Date :     April 8, 2025
//
// Brief Description : Gets the level time and displays it on the win screen.
*****************************************************************************/

using System;
using TMPro;
using UnityEngine;

public class WinSetTime : MonoBehaviour
{
    //Setting variables
    [SerializeField] private TMP_Text timeText;
    private float savedTime;

    private GameObject dataObject;
    private PersistentData persistentData;


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

        //Allows the data object to be destroyed
        persistentData.CanDelete = true;
    }
}
