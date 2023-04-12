using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameData : MonoBehaviour
{
    public static int totalCoins = 0; // A variable to store the total number of coind
    public static bool[] levelsCompleted = { false, false }; // An array to store which of the levels have been completed
    public static bool[] OutfitsBought = { false }; // An array to store which outfits have been bought
    public static int SavedGame;
    public static bool saveFile;

    public void AddCoins() // Procedure to add coins to the total
    {
        totalCoins += EndOfLevel.coinTally; // Takes the coins from the End Of level script and adds it to the total coins
    }

    public void LevelOne() // Sets level 1 to completed
    {
        levelsCompleted[0] = true; // The first element in the levels completed array becomes true
    }

    public void LevelTwo() // Sets level 2 to completed
    {
        levelsCompleted[1] = true; // The second element in the levels completed array becomes true
    }

    public void RunnerBought() // Sets the Runner outfit to bought 
    {
        OutfitsBought[0] = true; // The first element in the outfits bought array becomes true
    }

    public int TotalCoins() // Function to return the total number of coins
    {
        return totalCoins;
    }

    public void SavePlayer() // Is a procedure to save the game data
    {
        SaveSystem.SavePlayer(SavedGame); // Calls the procedure in the save system script
    }

    public void LoadPlayer(int save) // Is a procedure to save the game data
    {
        SavedData data = SaveSystem.LoadPlayer(save); 
        // Defines a new variable of type SavedData to what's returned from the load player function in the Save System script

        if (saveFile == true)
        {
            totalCoins = data.totalCoins; // Sets the total coins as the total coins that have been retrieved from the file
            levelsCompleted = data.levelsCompleted; // Sets the levels completed as the levels completed that have been retrieved from the file
            OutfitsBought = data.OutfitsBought; // Sets the outfits bought as the outfits bought that have been retrieved from the file
        }
        

       
    }

    public void SaveGame1()
    {
        SavedGame = 1;
        LoadPlayer(SavedGame);
        DisplaySave();
    }

    public void SaveGame2()
    {
        SavedGame = 2;
        LoadPlayer(SavedGame);
        DisplaySave();
    }

    public void SaveGame3()
    {
        SavedGame = 3;
        LoadPlayer(SavedGame);
        DisplaySave();
    }

    void DisplaySave()
    {
        if (saveFile == false)
        {
            GameObject.Find("GameText").GetComponent<TextMeshProUGUI>().text = "No saved data";
        }
        else
        {
            GameObject.Find("GameText").GetComponent<TextMeshProUGUI>().text = "Coins: " + totalCoins;
        }
    }

}
