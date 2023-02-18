using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMETERS - for tuning, typically set in the editor.
    // CACHE - e.g. references for readability or speed.
    // STATE - private instance (member) variables.


    // Variables for the mainThrust & rotationThrust & reference to the Rigidbody and Audiosource.
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
   
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    Rigidbody rbody;
    AudioSource audioSource;
    ParticleSystem parSystem;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody & audiosource component from the Gameobject.
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        parSystem= GetComponent<ParticleSystem>();
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
    // Particles play when pressing space or A/D.
    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
        {
            rbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
           

        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }

    }

      void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }

        

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if(!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
           
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
            
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rbody.freezeRotation = true; // Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbody.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }
}
