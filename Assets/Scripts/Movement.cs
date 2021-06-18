using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //variables that defines the speed of movement for the rocket
    [SerializeField] private float thrustSpeed = 800f;
    [SerializeField] private float rotationSpeed = 100f;

    //create a variable to store the clip
    [SerializeField] AudioClip thrustAudio;

    //particle systems being cashed for the specified boosters
    [SerializeField] ParticleSystem leftUpper, leftBottom;
    [SerializeField] ParticleSystem rightUpper, rightBottom;
    [SerializeField] ParticleSystem thruster;


    //gameObject components that gets cashed
    Rigidbody myBody;
    AudioSource myAudio;

    //Function that happens as the script comes int play
    private void Awake()
    {
        Init();
    }

    //function to initialize when the script comes to play
    private void Init()
    {
        myBody = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
    }

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

    //Picks up if the player is pressing space
    void Thrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SpacePressed();
        }
        //if space is not pressed, stop the thrusting sound
        else         
        {
            myAudio.Stop();
        }
    }

    //Move the rocket up relative to its nose(forward) and play sounds
    void SpacePressed()
    {
        //Debug.Log("You are thrusting");
        myBody.AddRelativeForce(Vector3.up * Time.deltaTime * thrustSpeed);
        thruster.Play();
        if (!myAudio.isPlaying)
        {
            myAudio.clip= thrustAudio;
            myAudio.Play();
        }
    }

    //Picks up if the player is rotating
    void Rotating()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
    }

    private void RotateLeft()
    {
        //Debug.Log("You are rotating left");
        SetRotation(rotationSpeed);
        rightBottom.Play();
        rightUpper.Play();
        
    }

    private void RotateRight()
    {
        //Debug.Log("You are rotating right");
        SetRotation(-rotationSpeed);
        leftBottom.Play();
        leftUpper.Play();
    }

    void SetRotation(float rotationThrust)
    {
        //freezes physics rotation
        myBody.freezeRotation = true;

        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThrust);

        //enables physic rotation
        myBody.freezeRotation = false;
    }
}
