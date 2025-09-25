// Gives us access to classes in the UnityEngine namespace.
using UnityEngine;

// A "class" in Unity is the same as a Blueprint in Unreal.
// Notice it inherits from MonoBehaviour. This is the same as all objects in the world in Unreal inheriting from Actor.
// In Unity a GameObject is the same thing as an Actor in Unreal, something that can be placed in the world.
public class PlayerController : MonoBehaviour
{
    // Any variables placed here are accessible in the entire class and are technically called "fields".
    // [SerializeField] is a special attribute unique to Unity that allows a field to be displayed inside the Editor in the Inspector (Details) panel.
    // It's unique cause it exposes it to the Editor WITHOUT making it have to be public (which also exposes it to the Editor).
    // The [Tooltip()] attribute below is a Unity thing that allows us to give a little tooltip hint to any field.
    // When you hover over the field in the Inspector, it will show the tooltip.
    // The [Header()] attribute is also a Unity thing that gives a header that displays in the Inspector. Used to group fields visually for easier understanding.
    [Header("Player Stats")]
    [Tooltip("Movement speed in meters per second.")]
    [SerializeField] float moveSpeed;
    [Tooltip("Jump force in Newtons.")]
    [SerializeField] float jumpForce;

    // You can also put references to other classes, objects, and components up here which will also make them accessible in the whole class.
    // I like to separate my data fields from my reference fields but that's not necessary.
    [Header("References")]
    [Tooltip("A reference to a specific Rigidbody2D component. We'll use it to store a reference to the one on the Player themselves.")]
    [SerializeField] Rigidbody2D rb2d;
    [Tooltip("A reference to thing we want to spawn.")]
    [SerializeField] GameObject enemyToSpawn;

    // Start() is the same thing as EventBeginPlay in Unreal.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // We could have just dragged and dropped the RigidBody2d component from our player into this field in the Inspector.
        // This is just another way to do it without having to do that. This searches this GameObject with this script for a Rigidbody2D component.
        // It then stores a reference to it in the rb2d field.
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update() is the same thing as EventTick in Unreal.
    // Update is called once per frame
    void Update()
    {
        // We're calling the Move() method we defined down below every single frame.
        // This will allow our player to instantly move according to player input a little bit each frame.
        Move();

        // We are checking every single frame to see if the player pressed the Spacebar key.
        // The frame the player presses it down, only one time on that frame, it will call our Jump() method we made below.
        // Note: You can also use a key set up in the Input Manager like this: Input.GetButtonDown("Jump").
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Here we check to see if the player pressed LeftShift this frame, if they did, call StartSprinting().
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSprinting();
        }
        // If they did NOT press LeftShift this frame, check to see if they released LeftShift this frame. If they did, call StopSprinting().
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprinting();
        }

        // Check every single frame if the player pressed the E key. If they did...
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Spawn an enemy!
            SpawnEnemy();
        }
    }

    // This custom method is for handling all the movement of our player.
    // Methods in Unity are the same thing as functions in Unreal.
    // For those who don't remember, "void" simply means we're saying this method does not return anything.
    void Move()
    {
        // This makes a local float variable called x and assigns it the value of the current Input value of "Horizontal".
        // "Horizontal" is a default Unity button mapped to a, d, left, and right arrows. It returns a value from -1 to 1 based on which key is being pressed.
        // If you're pressing a or left arrow, it returns -1, if you press d or right arrow, it returns 1, if you aren't pressing any of those, it returns 0.
        float x = Input.GetAxis("Horizontal");

        // Here we create and store a new Vector2 which is simply an (x, y) value. Notice for the x value we're using the x variable we made above.
        // Notice we also are multiplying the vector by Time.deltaTime to make it frame-rate-independent, and by moveSpeed so our moveSpeed field affects the magnitude of this vector.
        // Think of the (x, 0) as defining the direction (-1 or 1) on the X axis of the world, then we use moveSpeed to set the speed of movement.
        Vector2 movementVector = new Vector2(x, 0) * Time.deltaTime * moveSpeed;

        // This accesses the Transform component on whatever object has this script attached to it.
        // It then calls the Translate() method of the Transform class.
        // The Transform() method takes a Vector as a parameter and simply moves the object in the direction and distance of the vector.
        transform.Translate(movementVector);
    }

    // Method to handle jumping.
    void Jump()
    {
        // This accesses the Rigidbody2D component we stored earlier and calls its AddForce() method.
        // AddForce() here is taking a Vector2 representing the direction and magnitude of jump power.
        // We're also telling it we want to apply the entire force immediately and not over time using ForceMode2D.Impulse.
        // Note: Vector2.up is just shorthand for new Vector2(0, 1).
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Method to permanently increase the Player's maximum movement speed. Pickups can use this to boost player stats.
    // Notice it's public. It HAS to be public for another class to call the method.
    public void MoveSpeedUpgrade(float speed)
    {
        moveSpeed += speed;
    }

    // Methods to start and stop sprinting.
    // Simply increases the player's moveSpeed and the other one brings it back to what it was before.
    void StartSprinting()
    {
        moveSpeed += 5;
    }

    void StopSprinting()
    {
        moveSpeed -= 5;
    }

    // Method to spawn a new enemy into the world.
    void SpawnEnemy()
    {
        // Instantiate is the same as SpawnActorFromClass in Unreal.
        // We give it: A GameObject to spawn, the position in the world to spawn it at, a rotation it should start with.
        // Quaternion.identity means spawn it with the object's default rotation settings as defined in the Prefab.
        // Notice we're adding (0, 5, 0) to the position. This means we'll spawn the enemy 5 meters directly above the player's head.
        Instantiate(enemyToSpawn, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
    }
}