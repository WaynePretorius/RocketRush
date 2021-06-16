using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketColDetector : MonoBehaviour
{
    //be able to change the times that you wait before a scene is loaded
    [SerializeField] private float timeToWaitForSceneLoad = 1f;

    //Cashe the movement script
    Movement moveScript;
    AudioSource myAudio;

    //Particle system for crash and win
    [SerializeField] ParticleSystem parWin;
    [SerializeField] ParticleSystem parCrash;

    //create variables for different audioclips
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;

    //see if the player is in a state to play
    bool isNonPlayState = false;

    //call on functions before anything else starts
    private void Awake()
    {
        Init();
    }

    //function that declares all starting variables
    void Init()
    {
        moveScript = GetComponent<Movement>();
        myAudio = GetComponent<AudioSource>();
    }

    //detect the collision and identify it
    private void OnCollisionEnter(Collision other)
    {
        if (!isNonPlayState)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly Object");
                    break;
                case "Finish":
                    NextLevelSequence();
                    break;
                default:
                    CrashSequence();
                    break;
            }
        }
    }
    //disable all movement and start timer for the next scene
    void NextLevelSequence()
    {
        moveScript.enabled = false;
        parWin.Play();
        myAudio.Stop();
        myAudio.PlayOneShot(winSound);
        StartCoroutine(TimeToNextLevel());
        isNonPlayState = true;
    }

    //yield a certain amount of seconds before you start the next scene
    IEnumerator TimeToNextLevel()
    {
        yield return new WaitForSeconds(timeToWaitForSceneLoad);
        LoadNextLevel();
    }

    //disable movement and start time for restarting the level
    void CrashSequence()
    {
        moveScript.enabled = false;
        parCrash.Play();
        myAudio.Stop();
        myAudio.PlayOneShot(crashSound);
        StartCoroutine(TimetoRestartLevel());
        isNonPlayState = true;
    }

    //start time and then call the restart scene function
    IEnumerator TimetoRestartLevel()
    {
        yield return new WaitForSeconds(timeToWaitForSceneLoad);
        RestartLevel();
    }

    void RestartLevel()
    {
        //get the current scene index and store it
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        //restart the current scene
        SceneManager.LoadScene(sceneIndex);
    }

    void LoadNextLevel()
    {
        //get the current scene index and store it
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = sceneIndex + 1;

        //if the curren scene is the last, go to first scene
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //set the variable to the first scene
            nextSceneIndex = 0;
        }

        //load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }
}
