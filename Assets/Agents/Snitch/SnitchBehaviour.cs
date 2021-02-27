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
        seed = 8;
        scoreGriffindor = 0;
        scoreSlytherin = 0;
        maxSpeed = 10f;
        rng = new System.Random(seed);


    }

    // Start is called before the first frame update
    void Start()
    {
        snitch = GetComponent<Rigidbody>();
        snitch.mass = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 forceDir = new Vector3((float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1);
        snitch.AddForce(forceDir);

        RepellWalls();

        if (snitch.velocity.magnitude > maxSpeed)
        {
            snitch.velocity = Vector3.ClampMagnitude(snitch.velocity, maxSpeed);
        }
        
    }

    void RepellWalls()
    {
        // add a repulsive force from each wall that scales with 1/r^2

        float xPos = -1 / ((25 - snitch.transform.position.x) * (25 - snitch.transform.position.x));
        float xNeg = 1 / ((-25 - snitch.transform.position.x) * (-25 - snitch.transform.position.x));

        float zPos = -1 / ((25 - snitch.transform.position.z) * (25 - snitch.transform.position.z));
        float zNeg = 1 / ((-25 - snitch.transform.position.z) * (-25 - snitch.transform.position.z));

        float yPos = -1 / ((25 - snitch.transform.position.y) * (25 - snitch.transform.position.y));
        float yNeg = 1 / ((0 - snitch.transform.position.y) * (0 - snitch.transform.position.y));

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
}
