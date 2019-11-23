using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float jumpHeight = 6.0f; // The height of the player's jump.
    [SerializeField]
    private float speed = 3.0f; // The speed the player walks at.
    private float playerScale = 0.07f; // The default scale for the player object.
    private float playerWidth;
    private float playerHeight;
    private Animator animator;//For the animator.controller FSM
    private bool isJumping; //for the animator.controller FSM
    private bool isWalking; //For the animator.controller FSM
    private bool isOnGround; // Used to check if the player is touching the ground layer.
    private Vector3 currentScale; // Used for the player scale.
    private bool facingRight = true; // Used to define whether the player is facing left or right.
    public LayerMask groundLayer; // Layer set as the floor.

    void Start ()
    {
        animator = GetComponent<Animator>(); // Target the player's animations.
        playerWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x; // Get the player's width using the length in x.
        playerHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y; // Get the player's height using the length in y.
    }

    void Update() {
    if (Input.GetKeyDown(KeyCode.UpArrow)) // If the player presses up:
    {
        if (isOnGround) // And they are on the ground layer:
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 1 * jumpHeight); // Make the player jump.
            isJumping = true; // Update to say they are jumping.
        }
    }
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) // If the player moves left or right:
    {
        float v = Input.GetAxisRaw("Horizontal"); // Move in the horizontal:
            GetComponent<Rigidbody2D>().velocity = new Vector2(v * speed, GetComponent<Rigidbody2D>().velocity.y); // Set the new velocity:
            isWalking = true; // And update the bool to true.
            if (v < 1) transform.localScale = new Vector3(-playerScale, playerScale, 1); // If the player is moving left: Flip the sprite to invert.
            else transform.localScale = new Vector3(playerScale, playerScale, 1); // Otherwise keep the sprite facing right (default).
    }
    else // Otherwise:
    {
        StopMovement(); // Slow down the player to stop moving.
        isWalking = false; // Reset the bool.
    }
}

    void FixedUpdate ()
    {
        isOnGround = Physics2D.IsTouchingLayers(this.GetComponent<Collider2D>(), groundLayer); // Check if the player is touching the ground layer.
        if (isOnGround) // If they are on the ground.
        {
            isJumping = false; // Reset jump bool.
        }
        // Set the FSM bools.
        animator.SetBool("isJumping",isJumping);
        animator.SetBool("isWalking", isWalking);
}
    public void StopMovement()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void ResetHeight() // Attempt to stop the player's scale reduced when they become a child of the platform.
    {
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(playerWidth, playerHeight);
    }
}
