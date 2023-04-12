using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public GameData gameData; // Makes a public variable so that we can call functions in the game data script
    Scene menu; // Makes a variable of type scene, called menu
    TextMeshProUGUI CoinText; // Makes a variable so we can write to the coins number text box
    int numCoins; // Makes a local variable called numCoins to store the total coins
    string selected; // A variable to store which item in the shop has been selected
    int cost; // Will store the cost of the item that's about to be purchased


    void Start()
    {
        menu = SceneManager.GetActiveScene(); // The menu variable is equal to the current scene we are in

        if (menu.name == "LevelSelectMenu" || menu.name == "ShopMenu") // If the name of the scene is level select or shop menu
        {
            //GameData.totalCoins = 30; // Just to test whether the buying works, I start myself with 20 coins
            ShowCoins(); // Calls a local procedure
        }

        if (menu.name == "ShopMenu") // If the name of the scene is the shop menu
        {

            if (GameData.OutfitsBought[0] == true) // If the runner outfit has been bought
            {
                Destroy(GameObject.Find("coins1")); // Destroy the coins1 object
                GameObject.Find("OwnedText").GetComponent<TextMeshProUGUI>().text = "SELECTED"; 
                // Change the text to show the runner outfit has been bought
            }
        }

        if (menu.name == "LevelSelectMenu") // If the name of the scene is the level select menu
        {
            if (GameData.levelsCompleted[0] == true) // If the first level has been completed
            {
                GameObject.Find("Button2").GetComponent<Image>().color = new Color(8f / 255f, 105f / 255f, 243f / 255f);
                // Changes the colour of the button to blue, to show it's not unlocked

                Destroy(GameObject.Find("padlockImage2")); // Removes the padlock image
            }
        }

    }


    public void RunnerOutfit() // A procedure called when the Runner outfit is selected
    {
        GameObject.Find("InfoText").GetComponent<TextMeshProUGUI>().text = "With this outfit," +
            " you get increased movement speed, meaning that you can also sprint faster. This outfit is good " +
            "if you need to be quick to get past security cameras or guards."; // Puts the information in the textbox

        if (GameData.OutfitsBought[0] == false) // Checks it hasn't been bought so you can't buy it twice
            selected = "runner"; // Shows the runner item has been selected
        
    }


    void ShowCoins()
    {
        numCoins = gameData.TotalCoins(); // Set the numCoins to the total coins from the Game data script
        CoinText = GameObject.Find("NumberOfCoins").GetComponent<TextMeshProUGUI>(); // Sets the coins text box to our text variable
        CoinText.text = numCoins.ToString(); // Writes the number of coins to the textbox as a string
    }

    public void Purchase()
    {
        if (selected == "runner") // The object selected is the runner
        {
            cost = int.Parse(GameObject.Find("NumberCoins1").GetComponent<TextMeshProUGUI>().text);
            // Set the cost to be the number of coins displayed under the item to be purchased

            if (cost <= GameData.totalCoins) // If the cost is less than the total coins that the user has
            {
                gameData.RunnerBought(); // Call the function that updates the game data to say the runner has been bought
                Destroy(GameObject.Find("coins1")); // Destroy the coins1 object
                selected = ""; // Resets the item selected so it can't be bought twice
                GameObject.Find("OwnedText").GetComponent<TextMeshProUGUI>().text = "SELECTED";
                // Change the text to show the runner outfit has been bought

                GameData.totalCoins -= cost; // Subtract the coins that were just spent
                ShowCoins(); // Calls a local procedure

                gameData.SavePlayer(); // Calls the save player procedure in the game data script
            }
        }  
    }


    public void QuitGame() // Defines a procedure that will end the program
    {
        Application.Quit(); // Quits the application
        Debug.Log("Quitting..."); // To check that it runs, put this in the console
    }

    public void LoadLevelSelect() // A procedure to load the level select menu
    {
        SceneManager.LoadScene("LevelSelectMenu"); // Loads the level select menu
    }
   
    public void ReturnMainMenu() // A procedure to load the main menu
    {
        SceneManager.LoadScene("MainMenu"); // Loads the main menu
    }

    public void LevelOne() // Loads level 1
    {
        SceneManager.LoadScene("Level1"); 
    }

    public void LevelTwo() // Loads level 2, but only when level 1 has been completed
    {
        if (GameData.levelsCompleted[0] == true)
            SceneManager.LoadScene("Level2");

    }

    public void ShopMenu() // Loads the shop menu
    {
        SceneManager.LoadScene("ShopMenu");
    }

    
}
