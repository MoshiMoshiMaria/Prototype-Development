using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour {

    public GameObject canvas;
    private SpriteRenderer padlock;
    private PlayerMovement player;
    private bool minigamecheck;
    private string answer;
    private string playerInput;
    private SpriteRenderer[] images = new SpriteRenderer[8];

    private bool answerShown;
    private bool holdImages;

    // Use this for initialization
    void Start () {
        canvas = gameObject; // Target empty game object as canvas for images.
        padlock = GameObject.Find("HackingMinigame").GetComponent<SpriteRenderer>(); // Find the trigger for the minigames and target the renderer for images.
        player = GameObject.Find("Player").GetComponent<PlayerMovement>(); // Target the player references in PlayerMovement.
        canvas.SetActive(false);
        // Initialisation of the sprites within the images array.
        images[0] = gameObject.transform.Find("arrowUpBlank").GetComponent<SpriteRenderer>();
        images[1] = gameObject.transform.Find("arrowDownBlank").GetComponent<SpriteRenderer>();
        images[2] = gameObject.transform.Find("arrowRightBlank").GetComponent<SpriteRenderer>();
        images[3] = gameObject.transform.Find("arrowLeftBlank").GetComponent<SpriteRenderer>();
        images[4] = gameObject.transform.Find("arrowUpFill").GetComponent<SpriteRenderer>();
        images[5] = gameObject.transform.Find("arrowDownFill").GetComponent<SpriteRenderer>();
        images[6] = gameObject.transform.Find("arrowRightFill").GetComponent<SpriteRenderer>();
        images[7] = gameObject.transform.Find("arrowLeftFill").GetComponent<SpriteRenderer>();
        // End of initialisation.
        for (int x = 0; x < 8; x++) // Set all of the images default to false.
        {
            images[x].enabled = false;
        }
        answer = "dummy"; // Dummy value to hold.
        playerInput = ""; // Initialise player input as empty
        answerShown = false; // Default answer to be hidden.
    }

	void Update () {
        if (answerShown) // If the answer images is visible:
        {
            if (answer.Length == playerInput.Length) // If the player inputs the right length:
            {
                if (playerInput == answer) // And the answer matches:
                {
                    minigamecheck = true;
                    HideCanvas();
                    Debug.Log("Answer Correct");
                    padlock.enabled = false; // End the minigame
                }
                else // Otherwise: Dont release the platform and remain enabled so the player can retry.
                {
                    minigamecheck = false;
                    HideCanvas();
                    Debug.Log("Answer Incorrect");
                }
            }
            else // Otherwise check inputs.
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    playerInput += "0";
                    StartCoroutine(InputChecker(4));
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    playerInput += "1";
                    StartCoroutine(InputChecker(5));
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    playerInput += "2";
                    StartCoroutine(InputChecker(6));
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    playerInput += "3";
                    StartCoroutine(InputChecker(7));
                }
            }
        }
    }

    void CreateAnswer() // Makes the answer the player must match:
    {
        answer = "";
        int answerLength;
        answerLength = Random.Range(3, 7); // Answer may be between 3 and 7 arrows long (random).

        int current;
        for (int x = 0; x < answerLength; x++)
        {
            current = Random.Range(0, 4); // Choose random arrow for the combination until length met.
            answer += current.ToString(); // Add to the checking string.
        }
        Debug.Log(answer); // Used to test if the game worked.
    }

    public void ShowCanvas() // Makes the images visible to the player when active.
    {
        canvas.SetActive(true);
        player.enabled = false; // restrict the player movement while in the minigame to prevent them moving away from the platform.
        player.StopMovement();
        CreateAnswer(); // Make the answer on minigame start.
        StartCoroutine(DisplayAnswer());
        answerShown = true;
    }

    public void HideCanvas() // Make the images invisible again on completion.
    {
        for (int x = 0; x < 8; x++) // Reset the images to false.
        {
            images[x].enabled = false;
        }
        answer = "dummy";
        playerInput = "";
        answerShown = false;
        canvas.SetActive(false);
        player.enabled = true;
    }

    IEnumerator DisplayAnswer()
    {
        int index = 0;
        for (int x = 0; x < answer.Length; x++)
        {
            if (answer[x] == '0') index = 0;
            if (answer[x] == '1') index = 1;
            if (answer[x] == '2') index = 2;
            if (answer[x] == '3') index = 3;
            images[index].enabled = true;
            yield return new WaitForSeconds(0.5f); // Delay display of the answers.
            images[index].enabled = false;
            yield return new WaitForSeconds(0.25f);
        }
    }
    IEnumerator InputChecker(int input)
    {
        images[input].enabled = true;
        yield return new WaitForSeconds(0.5f);
        images[input].enabled = false;
    }
    public bool CheckMinigameFinished()
    {
        return minigamecheck; 
    }
}
