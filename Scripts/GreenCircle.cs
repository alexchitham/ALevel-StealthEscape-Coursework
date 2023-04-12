using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCircle : MonoBehaviour
{
    void Start() // Start is called before the first frame update
    {
        gameObject.SetActive(false); // It deactivates the object that this script is attached to, which is the green circle
    }
    public void Activate() // Creates a public procedure called Activate that can be called from any other script in the project
    {
        gameObject.SetActive(true); // It activates the object that this script is attached to, which is the green circle
    }
    public void Deactivate() // Creates a public procedure called Deactivate that can be called from any other script in the project
    {
        gameObject.SetActive(false);
    }

}
