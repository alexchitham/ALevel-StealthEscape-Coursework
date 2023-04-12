using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public static int coinNumber; // Creates a variable to keep track of the coins collected in the current level
    TextMeshProUGUI timerText; // Creates a variable so the timer text can be editted
    float time; // Creates a float for the total time remaining
    public static int mins; // Creates an integer for the minutes remaining
    public static float secs; // Creates a float for the seconds remaining


    private void Start()
    {
        coinNumber = 0;
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>(); // Attaches the timer text to the local variable timerText

        if (gameObject.name == "GameManager2") // If the object this script is attached to is GameManager 2, so if it's level 2
        {
            timerText.text = "4:00"; // Set the timer to 4 minutes
            time = 240; // This is the time in seconds allocated for level 2
        }
        else // If it is level 1
        {
            timerText.text = "3:00"; // Set the timer to 3 minutes
            time = 180; // This is the time in seconds allocated for level 1
        }

    }

    private void Update()
    {
        time -= Time.deltaTime; // Every frame the time should decrease, so it is a timer
        if (time >= 180) mins = 3; // If the time left is more than 180 seconds, there is 3 minutes left
        else if (time >= 120) mins = 2; // If time left is more than 120 seconds, there is 2 minutes left
        else if (time >= 60) mins = 1; // If time left is more than 60 seconds, there is 1 minute left
        else mins = 0; // Otherwise there is less than a minute left

        secs = Mathf.Floor(time - (60 * mins)); // The seconds left will be the total time minus the number of minutes
        // It is also rounded down now, rather than just rounded

        if (secs < 10) timerText.text = "0" + mins + ":0" + secs; // Corrects the format when there are less than 10 seconds left
        else timerText.text = "0" + mins + ":" + secs; // The text that should be visible should follow the format 00:00

        if (time < 1) // If there is no time left
        {
            EndOfLevel.end = "loseTime"; // Sets the static variable to loseTime
            SceneManager.LoadScene("EndLevelMenu"); // Loads the End Level Menu
        }
          

    }

    public void CoinTally()
    {
        coinNumber++;
    }
}
