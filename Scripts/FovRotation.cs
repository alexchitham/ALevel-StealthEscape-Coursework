using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FovRotation : MonoBehaviour
{
    private Mesh mesh; // Creates our custom mesh object
    public LayerMask layermask; // Creates a layermask that is defined through the editor so we can define which layers the ray collide with
    private Vector3 origin; // Makes a private variable for the origin, or where the FOV is centred around
    private float startingAngle; // Makes a variable so we can define the starting angle later
    private float fov; // This sets how wide the FOV will be


    private void Start() // Runs when the game first starts
    {
        mesh = new Mesh(); // Creates a custom object of type mesh that is called mesh
        GetComponent<MeshFilter>().mesh = mesh; // Applies our custom mesh to the mesh filter component on the FOV object
        origin = Vector3.zero; // Just for ease we set an origin at (0, 0, 0)
        fov = 90f;
    }

    void Update()
    {
        int rayCount = 50; // This sets the number of rays will shoot on top on top the first (zero) ray that shoots at the starting angle
        float currentAngle = startingAngle; // This is the starting angle that the first ray starts at (measured from positive x-axis)
        float angleIncrease = fov / rayCount; // As we want the rays even distributed through the fov, the angle between each ray is defined like this
        float viewDistance = 5f; // Sets how far the NPC will be able to see in any direction


        // The number of vertices we need is determined by the number of rays as we need a vertex at the origin, one at the end of the first ray
        // Then another vertex for each additional ray, hence rayCount +2
        Vector3[] vertices = new Vector3[rayCount + 2]; // You need vertices to create a mesh, each of which need coordinates, so we have an array of vector 3's


        // UV's are 2D coordinates and are used to apply textures to mesh using a material object
        Vector2[] uv = new Vector2[vertices.Length]; // You always have the same amount of UV vectors as vertices 


        // The triangles are used to connect the vertices to form the mesh. Each value we assign in the triangle array corresponds to a vertex.  
        // Each element in the triangles array corresponds to a vertex of a triangle that forms part of our mesh, so every 3 elements forms a complete triangle
        int[] triangles = new int[rayCount * 3]; // We want a triangle created between every adjacent ray, each triangle need 3 vertices, so 3 times the rayCount

        vertices[0] = origin; // We want the first vertex at the origin, but as this is a child object, the origin is the NPC's position

        int vertexIndex = 1; // Like the current vertex, so starts at vertex 1, which is the first one that's not the origin
        int triangleIndex = 0; // Like current triangle, but index 0 is the first vertex of the first triangle, 4 would be first vertex of second triangle etc.
        for (int i = 0; i <= rayCount; i++) // Sets up loop that repeats for each ray 
        {
            Vector3 vertex; // Defines a local vertex variable

            // Will shoot a ray from the origin, in the direction vector given by our current angle, for a set distance, only colliding with objects in specific layers in our layermask
            RaycastHit2D raycast = Physics2D.Raycast(origin, VectorFromAngle(currentAngle), viewDistance, layermask);

            if (raycast.collider == null) // If the ray didn't collide with anything
            {
                // Finds the vertex by starting at the origin and adding another vector found by our current angle, and the distance determined by the view distance
                vertex = origin + (VectorFromAngle(currentAngle) * viewDistance);
            }
            else
            {
                vertex = raycast.point; // If the ray does hit something, the vertex will be at the point where the ray collided with another object
            }
            if (raycast.collider != null) // If the collider hit something
            {
                if (raycast.transform.name == "Player") // If the object hit is called Player
                {
                    EndOfLevel.end = "loseFOV"; // Sets the static variable to loseFOV
                    SceneManager.LoadScene("EndLevelMenu"); // Loads the End Level Menu
                }
            }
            





            vertices[vertexIndex] = vertex; // Puts that vertex into our vertex array at the index specified by our vertex index 

            if (i > 0) // Need this as (vertexIndex - 1) would be negative when i = 0 as vertexIndex  = 0. And can't make a triangle when only one ray has been fired
            {
                // These 3 assignments are defining the vertices of a triangle
                triangles[triangleIndex] = 0; // We want the start of each triangle to be at the origin
                triangles[triangleIndex + 1] = vertexIndex - 1; // We want the second vertex to be the previous vertex
                triangles[triangleIndex + 2] = vertexIndex; // Third vertex is at the current vertex, so makes a triangle between 2 adjacent rays

                triangleIndex += 3; // Increment by 3, so we can define the next triangle
            }

            vertexIndex++; // Increment the current vertex so we define the triangle between each ray
            currentAngle -= angleIncrease; // Angles are measured anti-clockwise, but as we want our fov to be made clockwise, we decrease the angle
        }

        mesh.vertices = vertices; // We set our vertices that we have defined to the vertices of our custom mesh
        mesh.uv = uv; // We set our UV's that we have defined to the UV of our custom mesh (haven't defined any yet though)
        mesh.triangles = triangles; // We set our triangles that we have defined to the triangles of our custom mesh
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);

    }

    public Vector3 VectorFromAngle(float angle) // Defines a function that takes a float angle and turns into its correspoding direction vector relative to the positive x-axis
    {
        float angleRadians = angle * (Mathf.PI / 180f); // Turns the angle from degrees to radians

        // Getting the cos and sin of the angle, gives the x and y components of the vector respectively, where the vector is normalised
        return new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)); // Gives the normalised vector, where length is 1 due to pythagoras
    }

    public float AngleFromVector(Vector3 direction) // Defines a function that takes a vector, and finds its angle between the positive x-axis
    {
        direction = direction.normalized; // Makes it a unit vector
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Find the angle by doing inverse tan and converting to degrees
        if (n < 0) n += 360; // If it is negative, it finds the equivalent positive angle
        return n; // Return the angle
    }

    public void SetOrigin(Vector3 Origin) // Defines a public procedure that takes an origin as a parameter
    {
        this.origin = Origin; // It sets the origin variable on this script to the Origin argument that was passed in to this procedure
    }

    public void SetDirection(Vector3 direction) // Defines a procedure that takes a vector as a parameter
    {
        startingAngle = AngleFromVector(direction) + (fov / 2); // Finds the starting angle so that the middle of the FOV is the direction the NPC is moving
    }
    
}
