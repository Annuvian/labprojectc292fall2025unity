using UnityEngine;

// Go to the PlayerController class for an explanation of all of this.
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [Tooltip("Movement speed in meters per second.")]
    [SerializeField] float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Our movement method.
    void Move()
    {
        // Continuously move our Enemy in the X axis at a rate determined by our moveSpeed, and made framerate-independent using Time.deltaTime.
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    // This event is automatically called whenever this object's collider touches another object's collider.
    // We are simply defining the reaction we want to happen when this automatic trigger is fired.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Every time the enemy hits something, their moveSpeed will be set to the opposite sign (+ to - or - to +)
        // Because we're multiplying the movement direction vector by moveSpeed in Move(), this will change the direction the enemy is moving.
        // So, basically every time the enemy hits something, it will turn around and head in the opposite direction it was already moving in.
        moveSpeed *= -1.0f;
    }
}