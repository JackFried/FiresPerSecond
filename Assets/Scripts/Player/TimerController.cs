/*****************************************************************************
// File Name :         TimeController.cs
// Author :            Jack Fried
// Creation Date :     April 8, 2025
//
// Brief Description : Controls the timer function of the UI.
*****************************************************************************/

using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    //Setting variables
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float currentTime;

    public TMP_Text TimeText { get => timeText; set => timeText = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }


    /// <summary>
    /// Controls time ticking per frame
    /// </summary>
    void Update()
    {
        //Ticks up time by frames
        CurrentTime += Time.deltaTime;

        //Returns a time interval in seconds
        TimeSpan time = TimeSpan.FromSeconds(CurrentTime);
        //Formats time variable into specified string structure
        TimeText.text = time.ToString(@"mm\:ss\:ff");
    }
}
