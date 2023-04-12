using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyNumber : MonoBehaviour
{
    TextMeshProUGUI keysText; // Creates a variable of type TextMeshPro called Text
    int numKeys; // Defines a variable that will store the integer for the number of keys

    void Start()
    {
        keysText = gameObject.GetComponent<TextMeshProUGUI>(); // On the game object this script is attached to, get the component called TextMeshPro and assign its value to our variable
        keysText.text = "0"; // Assigns the text component of our Text variable to 0, as you start with no keys
    }

    public void SetKeys(int NumOfkeys) // A procedure that has a value passed into it when it is called
    {
        numKeys = NumOfkeys; // Updates the integer version of the numKeys variable in this script
        keysText.text = NumOfkeys.ToString(); // Sets the new text as the new value of number of keys, is converted to a string as well
    }

    public int Key() // A function named Key
    {
        return numKeys; // Returns the number of keys to the place where the fuction was called
    }
}


