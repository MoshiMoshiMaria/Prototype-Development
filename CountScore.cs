using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountScore : MonoBehaviour {

    private float gameScore; // Float to allow for revised score values in the future, store the score the player has earned.
    public Text scoreText; // Targets the UI GameObject that will display the score value to the player.

    void Start ()
    {
        gameScore = 0; // Initialise gameScore to 0.
        UpdateScoreText(); // Update the displayed value so it is not empty until the first enemy is defeated.
    }

    public void AddScore() // Called when an enemy is defeated to add score.
    {
        gameScore += 50; // Increment the score by the standard value if enemy is not defined.
        UpdateScoreText(); // Update text to display new score.
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + gameScore; // Alters the text to be e.g. 'Score: 200'
    }

    void ResetScore()
    {
        gameScore = 0; // When necessary, e.g. player dies, reset the score to its initial value
        UpdateScoreText();
    }
}
