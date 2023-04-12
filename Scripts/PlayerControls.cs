using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D PlayerRigidBody; // Need to create a variable to apply the movement speed to, and it is of type Rigid Body 2D
    public Animator animator; // Creates a variable of type animator so the parameters (that dictate the animations) can be changed
    
    float horizontal;  // Defining a variable that will be in control of the horizontal velocity
    float vertical;  // Defining a variable that will be in control of the vertical velocity
    public float Speed;  // This is the default value for speed, it is public so it can be defined through Unity.
    float movementLimiter; // This is what we will multiply the speed by for diagonal movement, the 'f' reiterates it's a float
    public StaminaBar StaminaBar; // This allows us to modify the stamina bar through this player controls script
    int MaxStamina; // Sets the maximum that will define the stamina when the game starts
    float stamina; // This sets the starting stamina
    bool collectedTreasure; // A boolean that keeps track if the treasure has been collected
    
    Scene Level; // Creates a variable to store a scene
    public static string levelName; // Creates a public static string to hold the name of the current level

    
    public void Start()
    {
        Speed = 5; // Defines the value of speed within the code
        movementLimiter = 0.7f; // Defines the value of the movementLimiter variable
        MaxStamina = 10; // Sets the maximum value of stamina
        stamina = MaxStamina; // Starts the stamina with its maximum value
        StaminaBar.SetMaxStamina(MaxStamina); // It calls the SetMaxStamina procedure and passes in the value of MaxStamina
        
        Level = SceneManager.GetActiveScene(); // The level is assigned to the current scene
        levelName = Level.name; // Assigns the name of the current scene to the level name variable
        collectedTreasure = false; // You start the game without the treasure

        if (GameData.OutfitsBought[0] == true) // If the runner outfit has been bought
        {
            animator.SetInteger("Outfit", 1); // Set the outfit parameter to 1
            Speed = 6;
        }
        else animator.SetInteger("Outfit", 0); // Otherwise set the outfit parameter to 0
    }

    private void Update() // Runs every frame
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // This is an in-built function is unity that will get a number between 1 and -1 based on the input from the user
        vertical = Input.GetAxisRaw("Vertical"); // Same as the horizontal, but is for the vertical. It works for WASD, the arrow keys and an xbox controller

        if (horizontal < 0)
        {
            animator.SetFloat("VelocityX", -1);  // Changes the parameter 'VelocityX' to -1, and this is used to change to the left moving animation
            animator.SetFloat("Speed", 1);  // Changes the parameter 'Speed' to 1, meaning the stationary animation will not be playing
            animator.SetFloat("VelocityY", 0); // Sets the vertical velocity to 0, to ensure the animation for vertical movement cannot occur
        }
        else if (horizontal > 0)
        {
            animator.SetFloat("VelocityX", 1);  // Changes the parameter 'VelocityX' to 1, and this is used to change to the right moving animation
            animator.SetFloat("Speed", 1);
            animator.SetFloat("VelocityY", 0);
        }
        else if (horizontal == 0 && vertical == 0)
        {
            animator.SetFloat("Speed", 0);  // Changes the parameter 'Speed' to 0, meaning the stationary animation will be playing
            animator.SetFloat("VelocityX", 0);  // Changes the parameter 'VelocityX' to 0, and this is used to stop any moving animations
            animator.SetFloat("VelocityY", 0); // Changes the parameter 'VelocityY' to 0, and this is used to stop any moving animations
        }
        else if (vertical < 0)
        {
            animator.SetFloat("VelocityY", -1);  // Changes the parameter 'VelocityY' to -1, and this is used to change to the down moving animation
            animator.SetFloat("Speed", 1);  // Changes the parameter 'Speed' to 1, meaning the stationary animation will not be playing
            animator.SetFloat("VelocityX", 0); // Sets the horizontal velocity to 0, to ensure the animation for vertical movement cannot occur
        }
        else if (vertical > 0)
        {
            animator.SetFloat("VelocityY", 1);  // Changes the parameter 'VelocityY' to 1, and this is used to change to the up moving animation
            animator.SetFloat("Speed", 1);
            animator.SetFloat("VelocityX", 0);
        }
       
        if ((Input.GetKey(KeyCode.P)) && (stamina > 0)) // If the left shift key is held down and stamina is more than 0
        {
            stamina -= Time.deltaTime; // Time.delta gives the time since the last frame in seconds, so will be subtracted from stamina
            StaminaBar.SetStamina(stamina); // This calls the SetStamina procedure and passes in the current value of stamina
            if (GameData.OutfitsBought[0] == true) Speed = 10; // Increasing the player speed when sprinting
            else Speed = 8;
        }
        else if (stamina < MaxStamina) // If the shift key is not being pressed, and the stamina is less than maximum
        {
            stamina += (0.666f * (Time.deltaTime)); // Add two thirds of the time between frames, each frame, to the stamina
            StaminaBar.SetStamina(stamina);
            if (GameData.OutfitsBought[0] == true) Speed = 6; // Increasing the player speed when sprinting
            else Speed = 5; // Putting the speed back to its default when the player is not sprinting
        }
    }

    private void FixedUpdate()  // Fixed update is used for physics actions and doesn't run every frame so uses less processing power
    {
        if (horizontal != 0 && vertical != 0) // If both the horizontal and vertical is more than zero
        {
            horizontal *= movementLimiter; // Multiplies the horizontal velocity by 0.7
            vertical *= movementLimiter; // Multiplies the vertical velocity by 0.7
        }

        PlayerRigidBody.velocity = new Vector2(horizontal * Speed, vertical * Speed); // States the horizontal and vertical velocity by multiplying each component by the default speed
    }

    void OnTriggerEnter2D(Collider2D collision) // If the player collides with an object
    {
        if ((collision.name == "Exit") && (collectedTreasure == true)) // If it collides with the exit and has the treasure
        {
            EndOfLevel.end = "win"; // Will set the static variable to win
            SceneManager.LoadScene("EndLevelMenu"); // Loads the End Level Menu
        }
    }

    public void TreasureCollected() // Defines a public procedure
    {
        collectedTreasure = true; // Sets the treasure as collected, so can finish the level
    }
}