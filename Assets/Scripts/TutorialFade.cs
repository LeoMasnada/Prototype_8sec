using UnityEngine;

public class TutorialFade : MonoBehaviour
{
    /**
     * Called on the first update after the object is created
     */

    private void Start()
    {
        // Creates an invoke to remove the object after 5 seconds after the object starts
        Invoke("Remove", 5f);
    }

    /**
     * Deactivates the game object
     */

    private void Remove()
    {
        this.gameObject.SetActive(false);
    }

    /**
     * Called on each update of frame
     */

    private void Update()
    {
        // If the user has clicked on the canvas
        if (Input.GetMouseButtonDown(0))
        {
            // Stops the invoke from happening
            CancelInvoke("Remove");
            // Disables the Tutorial object
            this.gameObject.SetActive(false);
        }
    }
}