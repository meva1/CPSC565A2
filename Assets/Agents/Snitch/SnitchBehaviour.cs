using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviour : MonoBehaviour
{
    public int seed;
    public Rigidbody snitch;
    private float maxSpeed;
    public int scoreGriffindor;
    public int scoreSlytherin;

    private System.Random rng;

    void Awake()
    {
        scoreGriffindor = 0;
        scoreSlytherin = 0;
        maxSpeed = 50f;
        rng = new System.Random(seed);
    }

    // Start is called before the first frame update
    void Start()
    {
        snitch = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forceDir = new Vector3((float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1);
        snitch.AddForce(forceDir);

        if (snitch.velocity.magnitude > maxSpeed)
        {
            snitch.velocity = Vector3.ClampMagnitude(snitch.velocity, maxSpeed);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "GriffindorPlayer")
        {
            scoreGriffindor += 1;
        }
        if (col.gameObject.name == "SlytherinPlayer")
        {
            scoreSlytherin += 1;
        }
    }
}
