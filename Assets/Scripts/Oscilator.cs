using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    //The starting position of the object
    Vector3 startPosition;

    //Variables that will declare where to the object will move, and the speed
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float cycleTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //store the starting position of the object
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveObstacle();
    }

    //will move the object between two points, to and back
    void moveObstacle()
    {
        //cannot divide by zero, skip the function if the time is zero
        if(cycleTime <= Mathf.Epsilon) { return; }

        //how much time is passed in one cycle
        float cycles = Time.time / cycleTime;
        //using a sin wave, get the correct value in order to move obstacle from a to b, and back again
        const float tau = Mathf.PI * 2;
        //variable that simulates one full cycle
        float rawSinWave = Mathf.Sin(cycles * tau);
        //sine wave is between -1 and 1. Add 1 then divide by two so that our value is still 0 to 1
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
