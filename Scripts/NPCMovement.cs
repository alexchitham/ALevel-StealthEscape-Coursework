using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform Waypoint1; // Makes a public variable so we can access a Waypoint's position in this script
    public Transform Waypoint2;
    public Transform Waypoint3;
    public Animator animator; // Makes a public animator, so we can change the value of the parameters with in the code
    float MoveSpeed; // Makes a variable that defines the speed of the NPC
    Vector3 difference; // This will be used to determine the direction the NPC will travel in
    int headingTowards; // An integer to keep track of which waypoint the NPC is going towards
    int direction; // An integer to keep track of the direction our NPC throught the waypoints
    float npcX; // Creates a variable of type float
    float npcY;
    float wpX;
    float wpY;
    float waitTime; // Will keep track of how long the NPC could wait at the edge waypoints
    public FovRotation fovRotation; // Makes a public variable that's is visible in the editor so we can link these 2 script together
    int numWaypoints;

    void Start()
    {
        headingTowards = 1; // We initially want the NPC to go the first waypoint
        direction = 0; // This correspond to the forwards direction
        waitTime = 3; // The NPC will wait for 3 seconds at edge waypoints
        MoveSpeed = 3; // Sets the movement speed of the NPC to 3

        if (gameObject.name == "NPC1") numWaypoints = 3;
        else if (gameObject.name == "NPC2") numWaypoints = 2;

    }

    void Update()
    {
        if (headingTowards == 1) // If heading to waypoint 1
            MovingToWaypoint(Waypoint1); // Call the procedure, taking Waypoint one as the argument
        else if (headingTowards == 2) // If heading to waypoint 2
            MovingToWaypoint(Waypoint2); // Call the procedure, taking Waypoint two as the argument
        else if (headingTowards == 3) // If heading to waypoint 3
            MovingToWaypoint(Waypoint3); // Call the procedure, taking Waypoint three as the argument

        fovRotation.SetOrigin(transform.position); // Sets the origin of the FOV to the position of the NPC
        
    }

    void MovingToWaypoint(Transform Waypoint) // Defines a procedure that takes a position of a waypoint as a parameter
    {
        difference = Waypoint.position - transform.position; // Finds the vector between the NPC's and Waypoint's positions
        difference.Normalize(); // This standard Unity function makes the 'difference' vector a unit vector
        npcX = Mathf.Round(transform.position.x * 10); // Round the NPC x-coordinate to 1dp
        npcY = Mathf.Round(transform.position.y * 10); // Round the NPC y-coordinate to 1dp
        wpX = Mathf.Round(Waypoint.position.x * 10); // Round the waypoint x-coordinate to 1dp
        wpY = Mathf.Round(Waypoint.position.y * 10); // Round the waypoint y-coordinate to 1dp
        if ((npcX != wpX) || (npcY != wpY)) // If the NPC is not at the waypoint
        {
            transform.position += difference * MoveSpeed * Time.deltaTime; // This adds a proportion of the vector to the NPC's position, so moves towards Waypoint
            fovRotation.SetDirection(difference); // Calls the procedure and passes in the variable difference, so the direction of the FOV can change
            animator.SetFloat("Speed", 1); // Sets the Speed parameter to 1, meaning the NPC is moving
            animator.SetFloat("Angle", fovRotation.AngleFromVector(difference)); // Changes the vector direction to an angle in degrees, and assigns it to the Angle parameter
        }
        else
            ChangeWaypoint(); // Calls the procedure
            
    }
    void ChangeWaypoint() // Defines a procedure that takes no parameter
    {
        if (direction == 0) // If going forwards through the waypoints
        {
            if (headingTowards < numWaypoints) // If going to waypoint one or two
                headingTowards++; // Increment the variable, so it goes to the next waypoint
            else
                Waiting(); // Calls the procedure called Waiting
                
        }
        if (direction == 1) // If going backwards through the waypoints
        {
            if (headingTowards > 1) // If going to towards waypoint two or three
                headingTowards--; // Decrement the variable, so will to previous waypoint
            else
                Waiting();
        }
    }

    void Waiting() // Defines a procedure called Waiting
    {
        if (waitTime > 0) // If the variable waitTime is above 0
        {
            animator.SetFloat("Angle", fovRotation.AngleFromVector(difference)); // Changes the angle parameter again in the same way as before
            animator.SetFloat("Speed", 0); // Changes the speed parameter to 0, so the NPC is not moving
            waitTime -= Time.deltaTime; // Subtract the time since the last frame, so is like a timer
        }
        else // If the timer has run out
        {
            waitTime = 3; // Reset the wait time
            if (direction == 0)
                direction = 1; // Set the NPC to head backwards through the waypoints
            else
                direction = 0; // Changes the direction through the waypoints to forwards
        }
    }
}
