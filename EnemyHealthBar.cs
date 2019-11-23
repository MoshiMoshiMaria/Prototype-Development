using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField]
    private EnemyAI enemy; // Target the enemy game object
    // Variables used for the heathbar //
    private float maxHealth; // The maximum health of the enemy.
    private float width; // The width of the health bar.
    private float currentHealth; // Float value of the current enemy health.
    private Transform greenBar; // Used to get the position of the green bar in the game world.
    private float startXPos; // The starting position of the health bar.
    private float finalXPos; // The ending position of the health bar.
    private float difference; // The difference in the bar size multiplied by the new percentage of health.
    // End of healthbar variables //

    void Start()
    {
        maxHealth = enemy.GetHealth(); // Initialise the original value of health to the float assigned in EnemyAI.
        greenBar = transform.Find("greenBar"); // Locate the enemy's health bar.
        width = greenBar.GetComponent<SpriteRenderer>().bounds.size.x; // Define width a the length of the bar in the x position.
        Debug.Log(width); // Output width to the console to check it is correct.
        startXPos = greenBar.position.x;
        finalXPos = startXPos - (width / 2);
        Debug.Log(startXPos);
        Debug.Log(finalXPos);
    }

    void Update()
    {
        startXPos = transform.Find("redBar").position.x; // Initialise the positions of the red health bar.
        finalXPos = startXPos - (width / 2);
        float newBarLength = currentHealth / maxHealth; // Length of the bar is updated to the percentage of health remaining.
        float difference = (startXPos - finalXPos) * (1 - newBarLength); // Update the difference.
        greenBar.localScale = new Vector3(newBarLength, greenBar.localScale.y, greenBar.localScale.z); // Update the size of the green health bar.
        if (currentHealth <= 0) // If the health is 0 or less (enemy defeated):
        {
            greenBar.position = new Vector3(finalXPos + 0.1f, greenBar.position.y, greenBar.position.z); // set to invisible size (required to avoid divide by zero error)
        }
        else
        {
            greenBar.position = new Vector3(startXPos - difference + 0.1f, greenBar.position.y, greenBar.position.z);
        }
        Debug.Log("max : " + maxHealth); // Used to check the values were updating correcly: showed where divide by zero erorr occurred.
        Debug.Log("newbar : " + newBarLength);
    }

    void FixedUpdate()
    {
        currentHealth = enemy.GetHealth(); // Update the current health.
    }
}
