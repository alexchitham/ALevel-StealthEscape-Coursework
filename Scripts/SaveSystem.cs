using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void SavePlayer(int save) // Makes a static procedure so it can be called from anywhere
    {
        BinaryFormatter formatter = new BinaryFormatter(); // Creates a binary formatter that will produce the binary file
        string path;
        if (save == 1)
        {
            path = Application.persistentDataPath + "/player1.txt"; // Finds a relevant place to store the file that's specific to the operating system that the user is using, 
            // then I give it a name, which could be anything, I chose txt so it can at least be opened directly to see whats in it
        }
        else if (save == 2)
        {
            path = Application.persistentDataPath + "/player2.txt";
        }
        else
        {
            path = Application.persistentDataPath + "/player3.txt";
        }



        FileStream stream = new FileStream(path, FileMode.Create); // Creates a basic file, that is saved in the location described by Path

        SavedData data = new SavedData(); // Creates a new instance of my custom object, and assigns it values by calling the constructor class
        // So this is what collects the data that needs to be stored

        formatter.Serialize(stream, data); // We then convert our basic stream file to a binary format
        stream.Close(); // Close the file
        Debug.Log("Save"); // A message to the console to show that this code has run
    }

    public static SavedData LoadPlayer(int save) // Makes a static procedure so it can be called from anywhere
    {
        string path; 

        if (save == 1)
        {
            path = Application.persistentDataPath + "/player1.txt"; // Sets the path variable to same as when we saved the file
        }
        else if (save == 2)
        {
            path = Application.persistentDataPath + "/player2.txt"; // Sets the path variable to same as when we saved the file
        }
        else
        {
            path = Application.persistentDataPath + "/player3.txt"; // Sets the path variable to same as when we saved the file
        }

        
        if (File.Exists(path)) // If the file exists
        {
            GameData.saveFile = true;

            BinaryFormatter formatter = new BinaryFormatter(); // Create a new binary formatter that will read the data we stored in the file
            FileStream stream = new FileStream(path, FileMode.Open); // Opens the file at the location described by path

            SavedData data = formatter.Deserialize(stream) as SavedData; // Creates a new instance of the custom object, 
            // and read the data from the file, and saves the data to the instance

            stream.Close(); // Closes the file
            return data; // Returns the data that was just retrieved from the file to the place it was called
        }
        else // If the file doesn't exist
        {
            GameData.saveFile = false;
            return null; // Return nothing
        }   

    }


}
