/*****************************************************************************
// File Name :         EnemySpawnEffect.cs
// Author :            Jack Fried
// Creation Date :     March 31, 2025
//
// Brief Description : Sets the effect that plays both when an enemy spawns
                       AND when an enemy is destroyed.
*****************************************************************************/

using UnityEngine;

public class EnemySpawnEffect : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float scaleIncrease;
    [SerializeField] private Renderer render;
    [SerializeField] private float fadeRate;
    private float currentFade;

    private float randomXRot;
    private float randomYRot;
    private float randomZRot;


    /// <summary>
    /// Sets initial fade comparison and also gives the object a random rotation
    /// </summary>
    void Start()
    {
        currentFade = 1f;

        randomXRot = Random.Range(0f, 359f);
        randomYRot = Random.Range(0f, 359f);
        randomZRot = Random.Range(0f, 359f);
        transform.eulerAngles = new Vector3(randomXRot, randomYRot, randomZRot);
    }

    /// <summary>
    /// Increases in size and fades over time; once alpha is 0, deletes itself
    /// </summary>
    void Update()
    {
        if (render.material.color.a > 0)
        {
            //Set new color (with fade)
            Color newColor = new Color(render.material.color.r, render.material.color.g, render.material.color.b,
                currentFade);
            render.material.color = newColor;
            currentFade -= fadeRate;

            //Increase scale
            transform.localScale += new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
