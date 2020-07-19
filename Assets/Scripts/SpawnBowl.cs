using UnityEngine;

public class SpawnBowl : MonoBehaviour
{
    // Array of bowls that can be spawned at the beginning of the game
    public GameObject[] bowls;

    /**
     * Generates a random bowl to start the game
     */

    public void SpawnFirstBowl()
    {
        // Gets a random object in the bowl array and generates it at the spawner's location
        Instantiate(bowls[Random.Range(0, bowls.Length)], this.transform.position, this.transform.rotation, this.transform);
    }
}