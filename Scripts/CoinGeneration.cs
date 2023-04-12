using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneration : MonoBehaviour
{
    public Rigidbody2D Coin; // Creates a public object, that the game object Coin is assigned to, as that's what we want to duplicate
    Rigidbody2D Coin1; // Creates a rigidbody object called Coin1
    Rigidbody2D Coin2;
    Rigidbody2D Coin3;
    void Start()
    {
        Coin1 = Instantiate(Coin); // Coin1 will be a clone of the game object given by the public object Coin
        Coin1.transform.position = new Vector3(-1, 1, 0); // Sets the transform position of the new coin to those coordinates

        Coin2 = Instantiate(Coin);
        Coin2.transform.position = new Vector3(-11, -6, 0);

        Coin3 = Instantiate(Coin);
        Coin3.transform.position = new Vector3(9, 10, 0);

    }




}
