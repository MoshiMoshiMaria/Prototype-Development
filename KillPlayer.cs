using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {

    [SerializeField]
    private PlayerFighting player; // Target the player.
    private float currentHealth; // Used to check current health.

    void Update()
    {
        if (currentHealth < 1)
        {
            Debug.Log("Player is dead");
            SceneManager.LoadScene("Bad_GameOver"); // Go to the bad game-over scene.
        }
    }

    void FixedUpdate()
    {
        currentHealth = player.GetHealth(); // Check the player health.
    }
}
