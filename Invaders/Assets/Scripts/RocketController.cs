using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float thrust;
    public GameObject pRocket;
    ParticleSystem ps;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = pRocket.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var emisRocket = ps.emission;

        if (Input.GetButton("Jump"))
        {
            emisRocket.rateOverTime = 10f;
            rb.AddRelativeForce(new Vector3(0f, thrust, 0f));
        }
        else
        {
            emisRocket.rateOverTime = 0f;
        }
    }
}
