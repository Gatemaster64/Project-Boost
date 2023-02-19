using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour


// PARAMETERS - for tuning, typically set in the editor.
// CACHE - e.g. references for readability or speed.
// STATE - private instance (member) variables.

{
   [SerializeField] float levelLoadDelay = 10f;
   [SerializeField] AudioClip crash;
   [SerializeField] AudioClip success;

   [SerializeField] ParticleSystem successParticles;
   [SerializeField] ParticleSystem crashParticles;


    ParticleSystem parSystem;
    AudioSource audioSource;
    BoxCollider boxCollider;

    // state between collisions.
    // If isTransitioning = true - Don't do the rest of the code block.
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parSystem = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }
    
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // Toggle collision
        }
    }



    // Script for detecting collisions using switches.
    void OnCollisionEnter(Collision other)
    {
        // if isTransitioning = true or collision is disabled.  don't do anymore code and Return. 
        if(isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have bumped into a friendly");
                break;

            case "Finish":

                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;

        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }


    // Methods to reload the level & next level.
    void ReloadLevel()
    {   //  Declaring a variable for the active scene, and loading the scene & Index.
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {    // if the level is the last in the index it will reload the first level in the index.
        
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

   
    
}
