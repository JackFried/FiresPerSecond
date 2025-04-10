/*****************************************************************************
// File Name :         BulletHitEffect.cs
// Author :            Jack Fried
// Creation Date :     March 27, 2025
//
// Brief Description : Controls the effect played when a player bullet hits
                       something.
*****************************************************************************/

using UnityEngine;

public class BulletHitEffect : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float scaleIncrease;
    [SerializeField] private Renderer render;
    [SerializeField] private float fadeRate;
    private float currentFade;


    /// <summary>
    /// Sets initial fade comparison
    /// </summary>
    void Start()
    {
        currentFade = 1f;
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
