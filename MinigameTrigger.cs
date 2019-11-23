using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour {

    public Minigame canvas; // Targets Minigame script to find the canvas reference.
    private MinigameTrigger trigger;
    private KeyCode hackKey = KeyCode.S; // Sets the key to press to activate the minigame as 's'.
    private bool incollider = false; // Default of in range to play the minigame.

    [SerializeField]
    private PlatformMovement platform; // Grab the platform object from the editor.

    void Start ()
    {
        trigger = gameObject.GetComponent<MinigameTrigger>(); // Finds the padlock that activates the platform minigame.
    }

    void Update ()
    {
        if (incollider == true) // If the player is in the range:
        {
            if (Input.GetKeyDown(hackKey)) // And they press the minigame key:
            {
                canvas.ShowCanvas(); // Activate the minigame canvas.
            }
            if (canvas.CheckMinigameFinished() == true) // If the game is completed:
            {
                trigger.enabled = false; // Disable the padlock.
		            platform.MakeActive(); // Make the platform activated so it can move
                Destroy(gameObject); // Destroy the padlock.
            }
        }
    }

    void OnTriggerStay2D(Collider2D PlayerCheck) // If the overlapping object is the player:
    {
        if (PlayerCheck.name == "Player")
        {
            incollider = true; // Set them as in range.
        }
    }

    void OnTriggerExit2D(Collider2D PlayerCheck) // If the player leaves the collider overlap:
    {
        if (PlayerCheck.name == "Player")
        {
            incollider = false; // Set them as out of range.
        }
    }
}
