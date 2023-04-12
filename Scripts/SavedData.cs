using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SavedData // This is my own custom object called Saved Data
{
    // These are the 3 properties that my custom object has
    public int totalCoins;
    public bool[] levelsCompleted;
    public bool[] OutfitsBought;


    public SavedData() // This is the constructor class for my object
    {
        // All the variables are assigned their values from the Game data script
        totalCoins = GameData.totalCoins;
        levelsCompleted = GameData.levelsCompleted;
        OutfitsBought = GameData.OutfitsBought;
    }

  


}
