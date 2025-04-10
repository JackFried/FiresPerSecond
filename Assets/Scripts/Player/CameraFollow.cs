/*****************************************************************************
// File Name :         CameraFollow.cs
// Author :            Jack Fried
// Creation Date :     March 15, 2025
//
// Brief Description : Causes the attached CameraHolder to follow a given
                       position.
*****************************************************************************/

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Setting variables
    [SerializeField] private Transform trackedPosition;


    /// <summary>
    /// Follow given position every frame
    /// </summary>
    void Update()
    {
        transform.position = trackedPosition.position;
    }
}
