using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
   
    
{    
   [SerializeField] float levelLoadDelay = 10f;


    // Script for detecting collisions using switches.
    void OnCollisionEnter(Collision other)
    {
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
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", levelLoadDelay);
        

    }

    void StartSuccessSequence()
    {
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
