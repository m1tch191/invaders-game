using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Variables")]
    public float thrust;
    public float controlModifier = 1f;

    private Vector3 moveDirection = Vector3.zero;

    [Header("Particle System")]
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

    //Other variables
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vertps = verticalThrusterPS.GetComponent<ParticleSystem>();
        forps = forwardThrusterPS.GetComponent<ParticleSystem>();
        revps = reverseThrusterPS.GetComponent<ParticleSystem>();
        leftps = leftThrusterPS.GetComponent<ParticleSystem>();
        rightps = rightThrusterPS.GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if(moveDirection.z > 0.1f)
        {
            forps.enableEmission = true;
            rb.AddRelativeForce(new Vector3(-thrust * controlModifier, 0f, 0f));
        }
        if (moveDirection.z < -0.1f)
        {
            revps.enableEmission = true;
            rb.AddRelativeForce(new Vector3(thrust * controlModifier, 0f, 0f));
        }
        if (moveDirection.x > 0.1f)
        {
            leftps.enableEmission = true;
            rb.AddRelativeForce(new Vector3(0f, 0f, thrust * controlModifier));
        }
        if (moveDirection.x < -0.1f)
        {
            rightps.enableEmission = true;
            rb.AddRelativeForce(new Vector3(0f, 0f, -thrust * controlModifier));
        }
        else {
            forps.enableEmission = false;
            revps.enableEmission = false;
            leftps.enableEmission = false;
            rightps.enableEmission = false;
        }

        if (Input.GetButton("Jump"))
        {
            vertps.enableEmission = true;
            rb.AddRelativeForce(new Vector3(0f, thrust, 0f));
        }
        else
        {
            vertps.enableEmission = false;
            
        }
    }
}




//public float thrust;
//public GameObject pRocket;
//ParticleSystem ps;

//Rigidbody rb;

//void Start()
//{
//    rb = GetComponent<Rigidbody>();
//    ps = pRocket.GetComponent<ParticleSystem>();

//}

//// Update is called once per frame
//void FixedUpdate()
//{
//    var emisRocket = ps.emission;

//    if (Input.GetButton("Jump"))
//    {
//        ps.Play();
//        rb.AddRelativeForce(new Vector3(0f, thrust, 0f));
//    }
//    else
//    {
//        ps.Stop();
//    }
//}