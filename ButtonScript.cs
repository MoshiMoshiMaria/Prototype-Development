using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private bool isMuted; // Bool used to check whether the mute button is active.
    private Text muteText; // Text uesd to visualise the activated mute button.
    public Dropdown resDropDown; // Used to target the dropdown of resolutions.

    // Menu Buttons
    void Start()
    {
        isMuted = false;// By default, audio is not muted.
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Cutscene"); // On click, go to the cutscene.
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("Options"); // On click, go to the options menu.
    }

    public void QuitButton()
    {
        Debug.Log("Game Exited"); // Used in editor to confirm that the application exits on bulid.
        Application.Quit(); // Closes the game application.
    }

    // Cutscene Buttons

        public void StartButton()
    {
        SceneManager.LoadScene("In_Game"); // Used to enter the first level of the game from the cutscene.
    }

    // Game Over Buttons

    public void RetryButton()
    {
        SceneManager.LoadScene("In_Game"); // Restarts the level on game-over.
    }

    public void ReturnMenuButton()
    {
        SceneManager.LoadScene("Main_Menu"); // Used to go back to the main menu from other areas of the game interface.
    }

    //Options Buttons

    public void ResolutionDropDown() // On intereaction with the dropdown bar of resolutions:
    {
        Resolution currentRes = Screen.currentResolution; // Set the current resolution to the screen's resolution.
        Debug.Log(currentRes); // Used to check whether resolution was updated.
        resDropDown = GameObject.Find("Resolution Dropdown").GetComponent<Dropdown>(); // Make sure that reference of dropdown is found.
        if (resDropDown.value == 0)
        {
            // If the first dropdown value, set to 600 x 800 fullscreen
            Screen.SetResolution(600, 800, true);
            Debug.Log("0");
        }
        else if (resDropDown.value == 1)
        {
            // set to 640 x 480 windowed
            Screen.SetResolution(640, 480, false);
            Debug.Log("1");
        }
        else
        {
            // set to 1920 x 1080 windowed
            Screen.SetResolution(1920, 1080, false);
            Debug.Log("2");
        }

    }


    public void MuteButton()
    {
        muteText = GameObject.Find("audiotext").GetComponent<Text>(); // Locate the mute button text.
        // on click audio mute
        if (isMuted)
        {
            AudioListener.volume = 1f; // Set the volume of the audio listener to full.
            muteText.text = " "; // Hide the text that defines whether the button is active.
            isMuted = false; // Set to not muted.
        }
        else // if muted already, un-mute
        {
            AudioListener.volume = 0f; // Silence the audio listener
            muteText.text = "x"; // Fill the button with an x to indicate that mute is active.
            isMuted = true; // Set to muted.
        }
        Debug.Log(AudioListener.volume); // Check whether the audio is muted or not.

    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits"); // Load the credits scene on click.
    }


}
