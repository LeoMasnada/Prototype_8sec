using UnityEngine;

public class DropZone : MonoBehaviour
{
    // Array of objects existing and usable
    public GameObject[] objectLibrary;

    // Reference to the transform to place the new object as a child of
    public Transform objectParent;

    // Flag to determine if the mouse is above the play area or not
    private bool mouseHovering;

    /**
     * Called when the first update after the object is created happens
     */

    private void Start()
    {
        // Lowers the flag stating that the mouse is above the play area
        mouseHovering = false;
    }

    /**
     * Summons an invoke to create a new object with a set time
     */

    public void SpawnNewObject(float timer)
    {
        // Creates the invoke with a dynamic delay
        Invoke("Spawn", timer);
    }

    /**
     * Creates a new object randomly selected in the library of objects and places it down
     */

    private void Spawn()
    {
        // Gets a random object in the library of objects and creates it, setting it as a child of the given parent object
        GameObject obj = Instantiate(objectLibrary[Random.Range(0, objectLibrary.Length)], objectParent);
        // Moves the object inside of the play area
        obj.transform.position = this.transform.position;
    }

    /**
     * Getter for the flag that indicates if the mouse is on the play area or not
     */

    public bool IsMouseHovering()
    {
        return mouseHovering;
    }

    /**
     * Detects when the mouse is set above the area and raises the flag
     */

    private void OnMouseEnter()
    {
        mouseHovering = true;
    }

    /**
     * Detects when the mouse leaves the area and lowers the flag
     */

    private void OnMouseExit()
    {
        mouseHovering = false;
    }
}