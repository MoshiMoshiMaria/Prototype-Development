using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) // If the player touches the collider over the door:
        {
            SceneManager.LoadSceneAsync("Level2"); // Take them to the next level.
        }
    }
}
