using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectControls : MonoBehaviour
{
    // Sound the object plays when colliding with something and the source it plays it through
    public AudioSource audioSource; public AudioClip clip;

    // Flag for if the object need to follow the mouse position or not
    [HideInInspector]
    public bool followMouse;

    // Flag for if the next object can be spawned or not
    [HideInInspector]
    private bool canRespawn;

    // Flag for if the object has to be affected by gravity or not
    private bool fall;

    // Reference to the object's rigidbody
    private Rigidbody2D objBody;

    /**
     * Function to make the object get affected by gravity, called when the user stops clicking on the screen
     * */

    public void Fall()
    {
        // Raise the two flags to make the object stop following the mouse and fall, then gives the object a gravity scale of 1
        fall = true;
        followMouse = false;
        objBody.gravityScale = 1f;
    }

    /**
     * Tests if the object has an absolute velocity low enough to consider it stable after having been dropped
     */

    public bool Landed()
    {
        return math.abs(objBody.velocity.x) <= 0.1f && math.abs(objBody.velocity.y) <= 0.1f && canRespawn;
    }

    /**
     * Called when the script is first loaded
     * Sets the reference to the rigidbody and ensures it doesn't get influenced by gravity
     */

    private void Awake()
    {
        objBody = this.GetComponent<Rigidbody2D>();
        objBody.gravityScale = 0f;
    }

    /**
     * Makes the object follow the mouse while the user hasn't dropped it
     */

    private void FollowMouse()
    {
        // The flag indicates the user hasn't dropped the object yet
        if (!fall)
        {
            // Makes the object spin on itself slowly
            this.transform.Rotate(new Vector3(0, 0, 1));

            // The user still has their cursor/finger in the play zone
            if (followMouse)
            {
                // Recovers the position of the cursor according to its position on the canvas
                Vector3 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Resets the z value to 0 to avoid depth order problems
                tmp.z = 0;

                // Shifts the object's position to the cursor's
                this.transform.position = tmp;
            }
        }
    }

    /**
     * When the object collides with another collider
     */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Plays the bumping sound at 5% volume
        audioSource.PlayOneShot(clip, 0.05f);
    }

    /**
     * When the object has exited a trigger collider
     */

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifies if the exitted collider is the play area
        if (collision.tag == "DropZone")
        {
            // Raises the flag to call in another object
            canRespawn = true;
        }
    }

    /**
     * Called on the first update after the object is created
     */

    private void Start()
    {
        // Generates a random color for the object using the Hue, Saturation, Value format
        this.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0f, 360f) / 360, 1f, 1f);

        // Generates a factor to use for the size and mass of the object (between 0.5 and 1.5 default size)
        float differenceRatio = 1 + Random.Range(-50f, 50f) / 100f;

        // Multiplies the scale on x and y of the object by the factor generated above
        this.transform.localScale = new Vector3(0.5f * differenceRatio, 0.5f * differenceRatio, 1f);

        // Multiplies the mass by the factor generated above
        objBody.mass *= differenceRatio;

        // Initializes the flags so the object stays idle in the play area, spinning and not falling down
        followMouse = false;
        fall = false;
        canRespawn = false;

        // Gives the controller a reference to this script on the last generated object
        GameController.instance.currentObject = this;
    }

    /**
     * Called at each frame update
     */

    private void Update()
    {
        // Updates or not the position of the object depending on the cursor position and the state of the object
        FollowMouse();
    }
}