using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Singleton of the game controller
    public static GameController instance;

    // Score and highest session score of the player
    public static int score;

    // Singleton for the highest score done in this session
    public static int highestScore;

    // Reference to the objects to update with the score and highest score on the UI
    public TextMeshProUGUI scoreTMP, highestScoreTMP;

    // Reference to the Object notifying a game over
    public TextMeshProUGUI gameOverTxt;

    // Reference to the script detecting the objects that fall off the screen
    public GameOverZone gameOver;

    // Reference to the script spawning new objects
    public DropZone spawnArea;

    // Reference to the script spawning the bowl to drop the forms in
    public SpawnBowl bowlSpawner;

    // Reference to the script spawning events depending on the score
    public SpawnEvent eventSpawnPoint;

    // Reference to the script that dictates the behavior of the last spawned object
    [HideInInspector]
    public ObjectControls currentObject;

    /**
     * Called on the load of the script
     */

    private void Awake()
    {
        // Second part of the Singleton setup for this script
        if (instance == null) { instance = this; }
        // Second part of the Singleton setup for the high score
        if (highestScore == null) { highestScore = 0; }
    }

    /**
     * Called when the first update after the object is created
     */

    private void Start()
    {
        // Creates the first object to play with without delay
        spawnArea.SpawnNewObject(0f);

        //Initialises the score for the current attempt
        score = 0;

        // Creates a new bowl randomly chosen
        bowlSpawner.SpawnFirstBowl();
    }

    /**
     * Called at each frame update
     */

    private void Update()
    {
        // Updates the flags if the cursor is or isn't inside the play area
        IsMouseInZone();

        // Updates the flags if the user has stopped clicking on the screen
        DropObject();

        // Updates the flags if the current object is considered landed or not
        ObjectLanded();

        // Updates the two text objects of the UI with the current scores and highscore
        scoreTMP.text = "Score: " + score;
        highestScoreTMP.text = "Highest: " + highestScore;
    }

    /**
     * Updates flags to verify if the object can be considered landed or not and manages when can a new object be spawned
     */

    private void ObjectLanded()
    {
        // If a reference to an object exists, if this object if considered landed and if the game is not considered lost
        if (currentObject && currentObject.Landed() && !gameOver.gameOverFlag)
        {
            // Frees the reference holder of the 'current object'
            currentObject = null;

            // Calls for a new object to be spawned in 2 seconds
            spawnArea.SpawnNewObject(2f);

            // Adds a point to the current score
            score++;

            // If the current score exceeds the highest score registered for this session
            if (score > highestScore)
            {
                // Saves current score in highest
                highestScore = score;
            }

            // When the player reaches a score dividable by 5
            if (score % 5 == 0)
            {
                // Creates a new paper plane in the scene
                eventSpawnPoint.SpawnPaperPlane();
            }
        }
    }

    /**
     * Shows the player they have lost the game with a text box and calls for the scene to be restarted
     */

    public void GameOver()
    {
        // Enables the text object with an animation so the player sees they have lost the game
        gameOverTxt.gameObject.SetActive(true);

        // Sets the RestartScene() fuction to be executed in 2 seconds
        Invoke("RestartScene", 2f);
    }

    /**
     * Called to restart a scene
     */

    private void RestartScene()
    {
        // Recovers the name of the currently loaded scene and makes the SceneManager load that scene again
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /**
     * Updates flags if the cursor is placed in the play area
     */

    private void IsMouseInZone()
    {
        // If an object to control exists
        if (currentObject != null)
            // Does the play area see the cursor above itself
            if (spawnArea.IsMouseHovering())
            {
                // Rasises the flag saying the cursor can be followed in the play area
                currentObject.followMouse = true;
            }
            // The play area doesn't have the cursor above itself
            else
            {
                // Lowers the flag saying the cursor cannot be followed in the play area
                currentObject.followMouse = false;
            }
    }

    /**
     * Updates the flags for the object to stop following the mouse and fall down
     */

    private void DropObject()
    {
        // If the user stops clicking/holding, the current object is referenced and the object was tracking the mouse position
        if (Input.GetMouseButtonUp(0) && currentObject && currentObject.followMouse)
        {
            // Calls the fall command of the object controls script
            currentObject.Fall();
        }
    }
}