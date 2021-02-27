using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlytherinPlayer : MonoBehaviour
{
    private Rigidbody player;
    public GameObject snitch;
    private float maxSpeed;
    private GameObject[] enemies;
    private GameObject[] friends;
    private float maxForce;
    private double meanMass;
    private double stddevMass;
    private System.Random rng;




    private void Awake()
    {
        maxSpeed = 10f;
        maxForce = 0.01f;
        meanMass = 50f;
        stddevMass = 10f;
        player = GetComponent<Rigidbody>();
        rng = new System.Random();
        player.mass = (float)SampleGaussian(rng, meanMass, stddevMass);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, snitch.transform.position);
        Vector3 dir = (snitch.transform.position - transform.position);
        dir.Normalize();
        player.AddForce(dir * dist);

        RepellWalls();
        RepellFriends();
        RepellEnemies();

        if (player.velocity.magnitude > maxSpeed)
        {
            player.velocity = Vector3.ClampMagnitude(player.velocity, maxSpeed);
        }


    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "snitch")
        {
            player.transform.position = new Vector3(-5, 2, -5);
        }
        else if(col.gameObject.name == "SlytherinPlayer")
        {
            Debug.Log("Slytherin on Slytherin Violence");
        }
        else
        {
            Debug.Log("Slytherin on Griffindor Violence");
        }
    }

    void RepellWalls()
    {
        // add a repulsive force from each wall that scales with 1/r^2

        float xPos = -1 / ((25 - player.transform.position.x));
        float xNeg = 1 / ((-25 - player.transform.position.x));

        float zPos = -1 / ((25 - player.transform.position.z));
        float zNeg = 1 / ((-25 - player.transform.position.z) );

        float yPos = -1 / ((25 - player.transform.position.y));
        float yNeg = 1 / ((0 - player.transform.position.y));

        Vector3 forceDir = new Vector3(xPos + xNeg, yPos + yNeg, zPos + zNeg);
        player.AddForce(forceDir);
    }

    void RepellFriends()
    {
        friends = GameObject.FindGameObjectsWithTag("Slytherin");
        foreach (GameObject friend in friends)
        {
            if (friend.transform != player.transform)
            {

                float dist = Vector3.Distance(transform.position, friend.transform.position);
                Vector3 dir = (transform.position - friend.transform.position);
                dir.Normalize();
                player.AddForce(dir * (maxForce));
            }
        }
    }

    void RepellEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Griffindor");
        foreach (GameObject enemy in enemies)
        {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                Vector3 dir = (transform.position - enemy.transform.position);
                dir.Normalize();
                player.AddForce(dir * (maxForce));
            
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
