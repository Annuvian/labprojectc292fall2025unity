using UnityEngine;

// For a description of anything not described here, go to the PlayerController.
public class TriggerZone : MonoBehaviour
{
    // Notice we are not setting the spawnPosition now, we'll do that in Start().
    // Also it's private, which means it will NOT show up in the Inspector, if it was, we could just type in values easily in the Inspector in the Editor.
    [Tooltip("Location where the object will spawn.")]
    private Vector2 spawnPosition;
    [Tooltip("Total amount of powerups this TriggerZone can spawn.")]
    int spawnCount = 2;

    [Tooltip("A reference to the GameObject this TriggerZone will spawn when entered.")]
    [SerializeField] GameObject thingToSpawn;

    private void Start()
    {
        // Get the world position of this object, and add 3 in the positive Y axis (straight up).
        // This is going to basically gives us a new location representing 3 meters directly above this object's position.
        // NOTICE: In Unity X axis is right/left, Y is up/down, and Z is forward/back.
        // In Unreal, X axis is forward/back, Y axis is right/left, and Z axis is up/down.
        // What makes it even more confusing is they're still ordered the same. So in both engines it's Vector3(x, y, z)
        // So, the direction (1, 2, 3) in Unity is the same direction as (3, 1, 2) in Unreal.
        spawnPosition = transform.position + new Vector3(0, 3, 0);
    }

    // This is an event that's automatically fired when any other object enters the trigger collider of the object this script is on.
    // We only can define what happens in response to this event being fired.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // First check the Collider2D component of the thing that hit the TriggerZone.
        // Access the GameObject that has the Collider2D that hit us attached to it.
        // Check to see if its tag is "Player". If so...
        if (collision.gameObject.tag == "Player")
        {
            // After the event has fired (something entered the trigger zone)...
            if (spawnCount > 0)
            {
                // Call SpawnItem on the reference to the GameObject in the field at the top of the class.
                SpawnItem(thingToSpawn);
                // Decrease spawnCount by 1.
                spawnCount--;
                // Check to see if the spawnCount is 0 or less (spawner can't spawn any more pickups)
                if (spawnCount <= 0)
                {
                    /// Destroy this TriggerZone if the spawnCount is 0 or less.
                    // Same as DestroyActor Node in Unreal.
                    Destroy(gameObject);
                }
            }
        }
    }

    // Our method for spawning an instance of whatever GameObject is passed in as an argument.
    void SpawnItem(GameObject item)
    {
        // Instantiate is a method that spawns something, it's the same as the SpawnActorOfClass Node in Unreal.
        // The parameters for it are <GameObject to spawn>, <position to spawn it at>, <rotation to spawn it at>.
        // Quaternion.identity simply means spawn it with whatever the default spawn for that GameObject is as defined in the Prefab (which is like a Blueprint).
        Instantiate(thingToSpawn, spawnPosition, Quaternion.identity);
    }
}