using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform Player; // Makes a public variable of type Transform, so the player object's position is accessible to the main camera
    public Camera MainCamera; // Makes a public variable of type Camera, so the camera's size can be altered by a key press
    bool followingPlayer; // The initial state of the camera is that it will be following the player
    private Scene currentScene;


    private void Start()
    {
        followingPlayer = true; // Making following the player the default camera setting 
        currentScene = SceneManager.GetActiveScene();
        MainCamera.orthographicSize = 10;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // If the 'F' is pressed
        {
            if (followingPlayer == true) // If the camera is currently following the player
            {
                transform.position = new Vector3(0, 0, -10); // Changes the camera's position to the middle of the screen

                if (currentScene.name == "Level1")
                    MainCamera.orthographicSize = 16; // Increases the size of the camera
                else MainCamera.orthographicSize = 22; // Increases the size of the camera


                followingPlayer = false; // Updates the follwing player variable to false
            }
            else
            {
                transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z); // Changes the camera's positon to the same as the players
                MainCamera.orthographicSize = 10; // Decreases the size of the camera
                followingPlayer = true; // Updates the follwing player variable to true
            }
        }

        if (followingPlayer == true)
        {
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z); // Changes the camera's positon to the same as the players
        }

    }
}
