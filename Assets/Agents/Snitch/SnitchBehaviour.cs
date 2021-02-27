using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviour : MonoBehaviour
{

    public Rigidbody snitch;
    private float maxSpeed;
    public int scoreGriffindor;
    public int scoreSlytherin;
    private System.Random rng;
    private double meanMass;
    private double stddevMass;

    void Awake()
    {
        snitch = GetComponent<Rigidbody>();
        meanMass = 10f;
        stddevMass = 5f;
        scoreGriffindor = 0;
        scoreSlytherin = 0;
        maxSpeed = 50f;
        rng = new System.Random();
        snitch.mass = (float)SampleGaussian(rng, meanMass, stddevMass);

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 forceDir = new Vector3((float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1);
        snitch.AddForce(forceDir*10);

        RepellWalls();

        if (snitch.velocity.magnitude > maxSpeed)
        {
            snitch.velocity = Vector3.ClampMagnitude(snitch.velocity, maxSpeed);
        }
        
    }

    void RepellWalls()
    {
        // add a repulsive force from each wall that scales with 1/r^2

        float xPos = -1 / ((25 - snitch.transform.position.x)* (25 - snitch.transform.position.x));
        float xNeg = 1 / ((-25 - snitch.transform.position.x)* (-25 - snitch.transform.position.x));

        float zPos = -1 / ((25 - snitch.transform.position.z)* (25 - snitch.transform.position.z));
        float zNeg = 1 / ((-25 - snitch.transform.position.z)* (-25 - snitch.transform.position.z));

        float yPos = -1 / ((25 - snitch.transform.position.y)* (25 - snitch.transform.position.y));
        float yNeg = 1 / ((0 - snitch.transform.position.y)* (0 - snitch.transform.position.y));

        Vector3 forceDir = new Vector3(xPos + xNeg, yPos + yNeg, zPos + zNeg);
        snitch.AddForce(forceDir);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "GriffindorPlayer")
        {
            scoreGriffindor += 1;
            Debug.Log("Griffindor");
        }
        if (col.gameObject.name == "SlytherinPlayer")
        {
            scoreSlytherin += 1;
            Debug.Log("Slytherin");
        }
    }

    double SampleGaussian(System.Random rng, double mean, double stddev)
    {
        // The method requires sampling from a uniform random of (0,1]
        // but Random.NextDouble() returns a sample of [0,1).
        double x1 = 1 - rng.NextDouble();
        double x2 = 1 - rng.NextDouble();

        double y1 = System.Math.Sqrt(-2.0 * System.Math.Log(x1)) * System.Math.Cos(2.0 * System.Math.PI * x2);
        return y1 * stddev + mean;
    }
}
