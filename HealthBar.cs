using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private PlayerFighting player; // Target the player object.
    private float maxHealth; // The maximum health of the player.
    private float width; // The width of the health bar.
    private float currentHealth; // The current health of the player.
    private Transform greenBar; // The object transform for the health bar.
    private float startXPos; // The start and end position of the health bar.
    private float finalXPos;
    private float difference;

    void Start () {
        maxHealth = player.GetHealth(); // Find the initial value of the players health, set it to the max.
        greenBar = transform.Find("greenBar"); // Locate the player health bar.
        width = greenBar.GetComponent<SpriteRenderer>().bounds.size.x; // Get the length of the x position and set it as the width.
        Debug.Log(width);
        startXPos = greenBar.position.x; // Set the starting and ending position of the health bar.
        finalXPos = startXPos - (width/2);
        Debug.Log(startXPos);
        Debug.Log(finalXPos);
    }

    void Update () {
        startXPos = transform.Find("redBar").position.x; // Set the starting position for the red bar (sits under the green bar to show when health is under 100%).
        finalXPos = startXPos - (width / 2);
        float newBarLength = currentHealth / maxHealth; // Set the new bar length as a percentage of the remaining health.
        float difference = (startXPos - finalXPos) * (1 - newBarLength);
        greenBar.localScale = new Vector3(newBarLength, greenBar.localScale.y, greenBar.localScale.z); // Update the size of the green health bar.
        if (currentHealth <= 0)
        {
            greenBar.position = new Vector3(finalXPos, greenBar.position.y, greenBar.position.z);
        }
        else
        {
            greenBar.position = new Vector3(startXPos - difference, greenBar.position.y, greenBar.position.z);
        }

    }

    void FixedUpdate()
    {
        currentHealth = player.GetHealth(); // Update the current player health.
    }
}
