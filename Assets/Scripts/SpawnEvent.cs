using System.Collections;
using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    // Array of events created
    public GameObject[] events;

    /**
     * Creates a paper plane event and adds it to the hierarchy
     */

    public void SpawnPaperPlane()
    {
        // Creates an empty object called "Event Holder"
        GameObject holder = new GameObject("EvenHolder");
        // Sets the newly created object as a child of the EvenSpawner object
        holder.transform.SetParent(transform);

        // Creates a new paper plane object
        GameObject plane = Instantiate(events[0]);
        // Sets the new paper plane as child of the EventHolder created above
        plane.transform.SetParent(holder.transform);

        // Generates a new position with a random y value between -0.5 and 1 in the local position compared to the parent object
        Vector3 posEvent = new Vector3(0f, Random.Range(-0.5f, 1f), 0f);
        // Gives the new position to the EventHolder object, making the plane appear at a different height than the others
        holder.transform.position = posEvent;
    }
}