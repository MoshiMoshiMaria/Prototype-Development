using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private PlayerFighting player; // Target the player object.

	void Start ()
  {
        player = GameObject.Find("Player").GetComponent<PlayerFighting>(); // Get the player reference.
	}

    void OnTriggerEnter2D(Collider2D collider) // If the player interacts with the powerup:
    {
        if (collider.gameObject.name == "Player")
        {
            player.AddHealth(24); // Increase the player's health.
            Destroy(gameObject); // Remove the potion from the level.
        }
    }
}
