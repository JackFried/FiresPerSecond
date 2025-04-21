/*****************************************************************************
// File Name :         HeartPickup.cs
// Author :            Jack Fried
// Creation Date :     April 17, 2025
//
// Brief Description : Controls the behavior of the heart pick-up object.
*****************************************************************************/

using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
