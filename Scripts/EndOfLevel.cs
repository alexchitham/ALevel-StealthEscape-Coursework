using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndOfLevel : MonoBehaviour
{
    string currentLevel; // Makes a variable to store the name of the current level
    TextMeshProUGUI coinText; // Makes a variable so we can edit the coins textbox on the end of level menu
    public GameData gameData; 
    public static int coinTally; // A public static variable containing the coins collected from a level
    int mins; // Local variable to store the remaining minutes
    float secs; // Local variable to store the remaining seconds
    TextMeshProUGUI timeText; // Makes a variable so we can edit the time textbox on the end of level menu
    
    public static string end; // A variable containing how the user ended the level

    private void Start()
    {
        currentLevel = PlayerControls.levelName; // The current level is set to the level name from the Player controls script
        coinTally = Levels.coinNumber; // As coinNumber in the levels script is static, I can access its value directly
        

        if (end != "win") // If the user did not complete the level
        {
            GameObject.Find("WinLevel").SetActive(false); // Hide the win level screen
            GameObject.Find("LoseLevel").SetActive(true); // Show the lose level screen
        }
        else
        {
            GameObject.Find("WinLevel").SetActive(true); // Show the win level screen
            GameObject.Find("LoseLevel").SetActive(false); // Hide the lose level screen
            coinText = GameObject.Find("coinText").GetComponent<TextMeshProUGUI>(); // Set the coin text variable to the text component of the coinText object
            
            

            if (currentLevel == "Level1")
            {
                GameData.levelsCompleted[0] = true;
                coinText.text = coinTally.ToString() + "/4"; // Write the number of coins collected in the textbox
            }
            else
            {
                GameData.levelsCompleted[1] = true;
                coinText.text = coinTally.ToString() + "/4"; // Write the number of coins collected in the textbox
            }






            gameData.AddCoins(); // Adds the coins collected to the total
            gameData.SavePlayer(); // Calls the save player procedure
        }
        if (end == "loseTime") // If the use lost as the timer ran out
        {
            GameObject.Find("Title Text").GetComponent<TextMeshProUGUI>().text = "You ran out of time!"; // Change text to 'you ran out of time'
        }

        mins = Levels.mins; // The mins variable is given the value of mins in the levels script
        secs = Mathf.Floor(Levels.secs); // The secs variable is given the value of secs in the levels script, and rounded down
        timeText = GameObject.Find("timeText").GetComponent<TextMeshProUGUI>(); // Get the timeTextLose object

        if (secs < 10) timeText.text = "0" + mins + ":0" + secs; // Corrects the format when there are less than 10 seconds left
        else timeText.text = "0" + mins + ":" + secs; // The text that should be visible should follow the format 00:00

    }

    public void RepeatLevel()
    {
        if (currentLevel == "Level1") // If the user just played level 1
            SceneManager.LoadScene("Level1"); // Load level 1
        else SceneManager.LoadScene("Level2"); // Otherwise, load level 2
        


    }


}
