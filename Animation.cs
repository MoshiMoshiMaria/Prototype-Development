using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// General animation based from the platform movement class //
public class Animation : MonoBehaviour {

    public GameObject start; // Starting position of the animation.
    public GameObject end; // Ending position of the animation.
    public float speed; // The speed the animation runs at.
    private float actualTime;
    private float moveTime;
    private float movement;
    private bool reverse = false; // Whether the animation should travel in reverse.
    private bool moving; // Checks whether the object is moving.
    private Vector3 velocity;

	void FixedUpdate () // Same basal function as PlatformMovement, see for comments.
    {
        moveTime = (Vector2.Distance(start.transform.position, end.transform.position) * speed);
        if (transform.position == end.transform.position)
        {
            if (reverse == false)
            {
                actualTime = Time.time;
            }
            reverse = true;
        }
        else if (transform.position == start.transform.position)
        {
            if (reverse == true)
            {
                actualTime = Time.time;
            }
            reverse = false;
        }

        if (reverse == false)
        {
            movement = speed * (Time.time - actualTime);
            transform.position = Vector3.Lerp(start.transform.position, end.transform.position, movement);
        }
        else if (reverse == true)
        {
            movement = speed * (Time.time - actualTime);
            transform.position = Vector3.Lerp(end.transform.position, start.transform.position, movement);
        }

        if (moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
