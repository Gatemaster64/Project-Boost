using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
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
                Debug.Log("You blew up");
                break;
                
        }
    }
}

