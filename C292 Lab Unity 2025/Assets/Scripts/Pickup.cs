using UnityEngine;

// Refer to the PlayerController class for anything not explained in here.
public class Pickup : MonoBehaviour
{
    [Tooltip("Amount of speed boost this Pickup grants.")]
    [SerializeField] float boostAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // After something collides with the pickup, check to see if that thing is tagged as the "Player".
        // Notice we're using the collision component of the thing that hit us, to then access the main GameObject the collision component is on,
        // Then checking the tag of the GameObject.
        if (collision.gameObject.tag == "Player")
        {
            // If it is something tagged "Player"...
            // Access the collision component of what hit us, access the GameObject the component is attached to, search for a PlayerController script as a component,
            // Then if it finds one, call the MoveSpeedUpgrade() method and pass in our boostAmount.
            // Essentially the same as the CastTo... Node in Unreal.
            collision.gameObject.GetComponent<PlayerController>().MoveSpeedUpgrade(boostAmount);

            // Destroy the GameObject that this script is on. Same as the DestroyActor Node in Unreal.
            Destroy(gameObject);
        }
    }
}