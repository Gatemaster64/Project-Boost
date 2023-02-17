using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{    // Script for detecting collisions using switches.
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have bumped into a friendly");
                break;

            case "Finish":
                Debug.Log("You have Finished!");
                break;

            case "Fuel":
                Debug.Log("You have picked up fuel");
                break;
            default:
                ReloadLevel();
                break;

        }
    }


     void ReloadLevel()
    {   //  Declaring a variable for the active scene, and loading the scene & Index.
        int currentSceneIndex = (SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }


}
