using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField]
    public GameObject start; // Start and end points for the movement animation.
    public GameObject end;
    public float speed; // Speed the platform should move at, variable so they can vary in difficulty.
    private float actualTime;
    private float moveTime;
    private float movement;
    private bool reverse = false;
    private bool moving;
    private Vector3 velocity;
    private bool isActive;

    void Start()
    {
        isActive = false; // Default platforms as inactive.
    }
    void FixedUpdate()
    {
        if (isActive)
        {
            moveTime = (Vector2.Distance(start.transform.position, end.transform.position) * speed); // The time taken to move from one object to the other (start to end, end to start).
            if (transform.position == end.transform.position) // If the platform is moving from the end:
            {
                if (reverse == false)
                {
                    actualTime = Time.time;
                }
                reverse = true; // Make it move in reverse.
            }
            else if (transform.position == start.transform.position) // Otherwise:
            {
                if (reverse == true)
                {
                    actualTime = Time.time;
                }
                reverse = false; // Move from start to end.
            }

            if (reverse == false) // If moving forwards:
            {
                movement = speed * (Time.time - actualTime);
                transform.position = Vector3.Lerp(start.transform.position, end.transform.position, movement); // Go from start to end.
            }
            else if (reverse == true) // Otherwise:
            {
                movement = speed * (Time.time - actualTime);
                transform.position = Vector3.Lerp(end.transform.position, start.transform.position, movement); // Move from end to start.
            }

            if (moving) // If the platform is moving:
            {
                transform.position += (velocity * Time.deltaTime); // Update direction.
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision) // If the player overlaps with the collider:
    {
        if (collision.gameObject.name.Equals ("Player"))
        {
            moving = true;
            collision.collider.transform.SetParent(transform); // Make the player a child of the platform (so it moves with it and doesnt fall off).
        }
    }
    void OnCollisionExit2D(Collision2D collision) // If the player leaves the collider:
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            collision.collider.transform.SetParent(null); // Remove it from the platform hierarchy.
        }
    }
    public void MakeActive()
    {
        isActive = true;
    }

    public bool GetActive()
    {
        return isActive;
    }
}
