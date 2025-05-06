/*****************************************************************************
// File Name :         PersistentData.cs
// Author :            Jack Fried
// Creation Date :     April 8, 2025
//
// Brief Description : Carries variables over between scenes.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    //Setting variables
    [SerializeField] private float bestTime;
    [SerializeField] private TimerController timerController;
    [SerializeField] private PersistentData Instance;

    private GameObject timer;
    private int currentLevelInt;

    [SerializeField] private string currentLevel;

    private bool canDelete;

    public float BestTime { get => bestTime; set => bestTime = value; }
    public string CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public bool CanDelete { get => canDelete; set => canDelete = value; }
    public int CurrentLevelInt { get => currentLevelInt; set => currentLevelInt = value; }


    /// <summary>
    /// Called on the first frame
    /// </summary>
    void Awake()
    {
        //Prevents self from being destroyed until all data is properly taken from it
        canDelete = false;

        //If an instance of this already exists, delete self
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        //Controls scene persistence
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// The code being called every frame
    /// </summary>
    void Update()
    {
        //If in the level scenes, find the timer and keep track of the current time + current level
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2" ||
            SceneManager.GetActiveScene().name == "Level3")
        {
            if (timerController == null)
            {
                timer = GameObject.FindGameObjectWithTag("Timer");
                timerController = timer.GetComponent<TimerController>();
            }

            CurrentLevel = SceneManager.GetActiveScene().name;
            BestTime = timerController.CurrentTime;

            //Getting level number
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                CurrentLevelInt = 1;
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                CurrentLevelInt = 2;
            }
            else if (SceneManager.GetActiveScene().name == "Level3")
            {
                CurrentLevelInt = 3;
            }
        }
        else //If outside of the levels scenes, destroy self
        {
            if (canDelete == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
