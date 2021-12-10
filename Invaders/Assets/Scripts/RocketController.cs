using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Variables")]
    public float thrust;
    public float controlModifier = 1f; //This number is mulitplied by thrust to give a lower thrust to the control thrusters

    private Vector3 moveDirection = Vector3.zero;

    [Header("Particle System")]
    // Setting up the process of being able to enable and disable the particle effect for control thrusters
    // Not working as intended, if multiple inputs are pressed all emissions stay on until nothing is pressed

    // Also there has to be a better way of setting this up because there is no way i need 3 varibles just to get access to ParticleSystem.Emission
    public GameObject verticalThrusterPS;
    public GameObject forwardThrusterPS;
    public GameObject reverseThrusterPS;
    public GameObject leftThrusterPS;
    public GameObject rightThrusterPS;

    ParticleSystem vertps;
    ParticleSystem forps;
    ParticleSystem revps;
    ParticleSystem leftps;
    ParticleSystem rightps;

    ParticleSystem.EmissionModule emissionVertical;
    ParticleSystem.EmissionModule emissionForward;
    ParticleSystem.EmissionModule emissionReverse;
    ParticleSystem.EmissionModule emissionLeft;
    ParticleSystem.EmissionModule emissionRight;

    //Other variables
    Rigidbody rb; 


    private void Start()
    {
        rb = GetComponent<Rigidbody>();     //Getting rigidbody so force can be added

        // Same with here there has got to be a better way of setting this up

        vertps = verticalThrusterPS.GetComponent<ParticleSystem>();     // Just grabbing the component off of the object, typical unity stuff
        forps = forwardThrusterPS.GetComponent<ParticleSystem>();
        revps = reverseThrusterPS.GetComponent<ParticleSystem>();
        leftps = leftThrusterPS.GetComponent<ParticleSystem>();
        rightps = rightThrusterPS.GetComponent<ParticleSystem>();

        emissionVertical = vertps.emission;
        emissionForward = forps.emission;
        emissionReverse = revps.emission;
        emissionLeft = leftps.emission;
        emissionRight = rightps.emission;
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); // Getting user input and storing into Vector (Perhaps switch this to proper keybinds and that
                                                                                                 //                                             would relieve issues with Particle System)
        if(moveDirection.z > 0.1f)
        {
            emissionForward.enabled = true;                                         // Emission on Particle System is enabled
            rb.AddRelativeForce(new Vector3(-thrust * controlModifier, 0f, 0f));    // Corresponding force is applied in the direction the control thruster is pushing
        }
        if (moveDirection.z < -0.1f)
        {
            emissionReverse.enabled = true;
            rb.AddRelativeForce(new Vector3(thrust * controlModifier, 0f, 0f));
        }
        if (moveDirection.x > 0.1f)
        {
            emissionLeft.enabled = true;
            rb.AddRelativeForce(new Vector3(0f, 0f, thrust * controlModifier));
        }
        if (moveDirection.x < -0.1f)
        {
            emissionRight.enabled = true;
            rb.AddRelativeForce(new Vector3(0f, 0f, -thrust * controlModifier));
        }
        if (Input.GetButton("Jump"))
        {
            emissionVertical.enabled = true;
            rb.AddRelativeForce(new Vector3(0f, thrust, 0f));
        }
        else
        {
            emissionForward.enabled = false;    // Disable all Particle Systems if nothing is pressed
            emissionReverse.enabled = false;
            emissionLeft.enabled = false;
            emissionRight.enabled = false;
            emissionVertical.enabled = false;
        }

       
    }
}
