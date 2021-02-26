using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int seed;
    private System.Random rng;
    public Rigidbody physicsBody;
    public GameObject snitch;
    private float maxSpeed;



    private void Awake()
    {
        maxSpeed = 10f;
        seed = 2;
        rng = new System.Random(seed);
        // Extract rigid body
        physicsBody = GetComponent<Rigidbody>();
        //snitch = GetComponent<Rigidbody>;
    }
    // Start is called before the first frame update
    void Start()
    {
        physicsBody.mass = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, snitch.transform.position);
        Vector3 dir = (snitch.transform.position - transform.position);
        dir.Normalize();
        physicsBody.AddForce(dir * dist);

        if (physicsBody.velocity.magnitude > maxSpeed)
        {
            physicsBody.velocity = Vector3.ClampMagnitude(physicsBody.velocity, maxSpeed);
        }


    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "snitch")
        {
            physicsBody.transform.position = new Vector3(2, 2, 2);
        }
    }
}
