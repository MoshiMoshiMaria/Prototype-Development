using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
  // Variables used to create camera follow, set to public for access in the inspector //
    public float followSpeed;
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
  // End of variable declaration //

    void Start()
    {
        targetPos = transform.position; // Set the target to the position of the camera on start.
    }

    void FixedUpdate()
    {
        if (target) // If there is a target object set:
        {
            Vector3 posNoZ = transform.position; // Set the z position to the object position so that it is always in view.
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * followSpeed; // Set follow velocity of camera to the magnitude * speed of target

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f); 

        }
    }
}
