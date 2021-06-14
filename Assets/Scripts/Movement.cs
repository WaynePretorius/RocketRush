using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    //Function to call the movement of the rocket
    void MovePlayer()
    {
        Thrusting();
        Rotating();        
    }

    //Picks up if the player is thrusting
    void Thrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("You are thrusting");
        }
    }

    //Picks up if the player is rotating
    void Rotating()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("You are rotating right");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("You are rotating left");
        }
    }
}
