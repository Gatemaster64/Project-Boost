using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Variables for the mainThrust & rotationThrust & reference to the Rigidbody and Audiosource.
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    Rigidbody rbody;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody & audiosource component from the Gameobject.
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Call the method for the Thrust and Rotation.
        ProcessThrust();
        ProcessRotation();
    }

    // Declare methods for Thrust and Rotation. Get the input for the player and apply the Thrust/Rotation.
    // if the spacebar is pressed the audioclip will play.
    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
        {
            rbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
        }
        else
        {
            audioSource.Stop();
        }

    }

      void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rbody.freezeRotation = true; // Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbody.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }
}
