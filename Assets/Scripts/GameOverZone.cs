using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    // Reference to the controller script of the hierarchy
    public GameController ctrl;

    // Flag that dictates if the game has been lost or not
    [HideInInspector]
    public bool gameOverFlag;

    /**
     * Called when the first update after the object is created happens
     */

    private void Start()
    {
        // Initializes the flag
        gameOverFlag = false;
    }

    /**
     * Called when an object passes through the colliders of the object
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the object that passed through the triggers is a dropped object or the bowl
        if (collision.tag == "DroppedObject" || collision.tag == "Bowl")
        {
            // Rasises the flag to end the game
            gameOverFlag = true;

            // Notifies the controller that the game is lost
            ctrl.GameOver();
        }
    }

    /**
     * Called when an object has exitted the trigger zones
     */

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Destroys the objects to avoid overloading the game with falling objects
        Destroy(collision.gameObject);
    }
}