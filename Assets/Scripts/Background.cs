using UnityEngine;

public class Background : MonoBehaviour
{
    /**
     * Update is called once per frame
     */

    private void Update()
    {
        // Creates a temporary point copying the location of the background at the previous frame
        Vector3 tmp = this.transform.position;
        // Adds a unit of distance divided by the time between two frames to the x value, resulting in a movement of the background of 1 unit per second on the x axis
        tmp.x += 1f * Time.deltaTime;

        // If the point ends at a specific location or farther on the x axis
        if (tmp.x >= 11.9f)
        {
            // Shifts the x value back to the first position
            tmp.x = -7.79f;
        }
        // This If statement creates the 'infinite' loop of the background pixel perfect (or almost)

        // Applies the calculated movement
        this.transform.position = tmp;
    }
}