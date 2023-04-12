using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public PlayerControls playerControls;
    bool retrievable; // Creates a new boolean variable called retrievable
    public GreenCircle GreenCircle; // This allows us to call procedures in the Green Circles script
    public KeyNumber KeyNumber; // This allows us to call procedures in the Key Number script
    public Levels levels;

    public void OnTriggerEnter2D(Collider2D Coins) // This Unity function is called when 2 objects enter a collision and one is a trigger
    {
        retrievable = true; // Sets the boolean variable to true when the objects are in contact
    }

    public void OnTriggerExit2D(Collider2D Coins) // This Unity function is called when 2 objects exit a collision and one is a trigger
    {
        retrievable = false; // Sets the boolean variable to false when the objects leave contact
        GreenCircle.Deactivate(); // Calls the procedure called Deactivate that's in the GreenCircle script
    }
    private void Update() // Called every frame
    {
        if (retrievable == true) // Checks to see if the player and the coin are in contact
        {
            GreenCircle.Activate(); // Calls the procedure called Activate that's in the GreenCircle script


            if (Input.GetKeyDown(KeyCode.O)) // Checks to see if the 'E' key is pressed
            {
                int numberKeys = KeyNumber.Key(); // Calls a function to return the current value of keys the player has

                if (gameObject.name == "Treasure") // If the object being collected is the treasure
                {
                    Destroy(gameObject); // Destroys the object that this script is attached, so the coin that the player collided with
                    playerControls.TreasureCollected(); // Call the treasure collected procedure
                }

                if ((gameObject.CompareTag("door")) && (numberKeys > 0)) // If the object being interacted with is a door and the player has at least 1 key
                {
                    Destroy(gameObject); // Destroy the door, so opens it
                    numberKeys--; // Decreases the number of keys as you use one opening the door
                    KeyNumber.SetKeys(numberKeys); // Calls the procedure in the Key Number script and passes in the numberKeys variable
                }
                    
                if (gameObject.CompareTag("keys")) // If the tag of the object being collected is keys
                {
                    Destroy(gameObject); // Destroys the object
                    numberKeys++; // Increases the number of keys as you just picked one up
                    KeyNumber.SetKeys(numberKeys); // Needs to change the number of keys in the other script so it can be displayed on the screen
                }

                if (gameObject.CompareTag("coins")) // If the object being collected is a coin
                {
                    Destroy(gameObject); // Destroy the coin, so you've picked it up
                    levels.CoinTally(); // Calls a procedure in the Levels script
                }
            }
        }


    }


    
    
       
       
    
}
