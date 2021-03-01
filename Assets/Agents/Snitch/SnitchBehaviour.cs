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
    public float forceMultiplier;
    public bool lastPointGriffindor;
    public bool lastPointSlytherin;


    void Awake()
    {
        rng = new System.Random();
        snitch = GetComponent<Rigidbody>();
        scoreGriffindor = 0;
        scoreSlytherin = 0;
        maxSpeed = 50f;
        snitch.mass = 0.5f;
        forceMultiplier = 10f;
        lastPointGriffindor = false;
        lastPointSlytherin = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 forceDir = new Vector3((float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1, (float)rng.NextDouble()*2-1);
        snitch.AddForce(forceDir);
        //Debug.Log(forceDir);

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
        snitch.AddForce(10*forceDir);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Griffindor")
        {
            if (lastPointGriffindor)
            {
                scoreGriffindor += 2;
            }
            else
            {
                scoreGriffindor += 1;
            }
            Debug.Log("Point for Griffindor");
            lastPointGriffindor = true;
            lastPointSlytherin = false;
            snitch.transform.position = new Vector3(0, 12.5f, 0);
        }
        else if (col.gameObject.tag == "Slytherin")
        {
            if (lastPointSlytherin)
            {
                scoreSlytherin += 2;
            }
            else
            {
                scoreSlytherin += 1;
            }
            lastPointGriffindor = false;
            lastPointSlytherin = true;
            Debug.Log("Slytherin");
            snitch.transform.position = new Vector3(0, 12.5f, 0);
        }
        else
        {
            //Debug.Log("Wall collision");
        }
    }



    
}
