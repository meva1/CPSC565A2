using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffindorPlayer : MonoBehaviour
{


    private Rigidbody player;
    public GameObject snitch;
    public double maxSpeed;
    public double aggressiveness;
    public double maxExhaustion;
    public double exhaustion;

    public float snitchAttractModifier;

    private GameObject[] enemies;
    private GameObject[] friends;
    private GameObject[] snitchTag;

    public bool unconscious;
    private int exhaustCounter;
    private int restCounter;
    private float restTime;

    private float wakeUpTime;
    private float unconsciousTime;

    private System.Random rng;

    public bool blocking;
    public bool violent;

    



    private void Awake()
    {
        player = GetComponent<Rigidbody>();


    }
    // Start is called before the first frame update
    void Start()
    {
        snitchTag = GameObject.FindGameObjectsWithTag("snitch");
        snitch = snitchTag[0];
        snitchAttractModifier = 5f;
        unconscious = false;
        exhaustCounter = 0;
        wakeUpTime = 0;
        unconsciousTime = 10f;
        rng = new System.Random();
        blocking = false;
        violent = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckExhaustion();
        if (!unconscious)
        {
            Rest();
            exhaustCounter++;
            restCounter++;
            float dist = Vector3.Distance(transform.position, snitch.transform.position);
            Vector3 dir = (snitch.transform.position - transform.position);
            dir.Normalize();
            player.AddForce(snitchAttractModifier * dir * dist);

            RepellWalls();
            RepellEnemies();
            RepellFriends();
            if (blocking)
            {
                Blocking();
            }
            if (violent)
            {
                Violent();
            }

            if (player.velocity.magnitude > maxSpeed)
            {
                player.velocity = Vector3.ClampMagnitude(player.velocity, (float)maxSpeed);
            }
            if(Time.time < restTime)
            {
                player.velocity = Vector3.ClampMagnitude(player.velocity, (float)maxSpeed/4);
            }
        }

        //CheckEscape();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "snitch")
        {
            player.transform.position = new Vector3(2, 2, 2);
        }
    }

    void RepellWalls()
    {
        // add a repulsive force from each wall that scales with 1/r^2

        float xPos = -1 / ((25 - player.transform.position.x));
        float xNeg = 1 / ((-25 - player.transform.position.x));

        float zPos = -1 / ((25 - player.transform.position.z));
        float zNeg = 1 / ((-25 - player.transform.position.z));

        float yPos = -1 / ((25 - player.transform.position.y));
        float yNeg = 1 / ((0 - player.transform.position.y));

        Vector3 forceDir = new Vector3(xPos + xNeg, yPos + yNeg, zPos + zNeg);
        player.AddForce(forceDir);
    }

    void RepellEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Slytherin");
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            Vector3 dir = (transform.position - enemy.transform.position );
            dir.Normalize();
            //dir = maxRepellEnemyForce*dir;
            player.AddForce(5*dir);
        }
    }

    void RepellFriends()
    {
        friends = GameObject.FindGameObjectsWithTag("Griffindor");
        foreach (GameObject friend in friends)
        {
            if (friend.transform != player.transform)
            {
                //Debug.Log("Griffindor friend found");
                float dist = Vector3.Distance(transform.position, friend.transform.position);
                Vector3 dir = (transform.position - friend.transform.position);
                dir.Normalize();
                //dir = maxRepellFriendForce*dir;
                player.AddForce(dir);
            }
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

    void CheckEscape()
    {
        if (player.transform.position.x > 26 || player.transform.position.x < -26 || player.transform.position.y > 26 || player.transform.position.y < -1 || player.transform.position.z > 26 || player.transform.position.z < -26)
        {
            player.transform.position = new Vector3(0, 12.5f, 0);
        }
    }

    void CheckExhaustion()
    {
        if (exhaustCounter > 100 && Time.time > restTime)
        {
            exhaustCounter = 0;
            exhaustion++;
        }
        /*
        if (!unconscious && exhaustion > maxExhaustion)
        {
            unconscious = true;
            player.GetComponent<Rigidbody>().useGravity = true;
            wakeUpTime = Time.time + unconsciousTime;
        }
        */
        if (unconscious && Time.time > wakeUpTime)
        {
            unconscious = false;
            exhaustion = 0;
            player.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void Violent()
    {
        enemies = GameObject.FindGameObjectsWithTag("Slytherin");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        float dist = Vector3.Distance(transform.position, closest.transform.position);
        Vector3 dir = (closest.transform.position - transform.position);
        dir.Normalize();
        player.AddForce(10 * dir * dist);
    }

    public void CollisionUnconscious()
    {
        unconscious = true;
        wakeUpTime = Time.time + unconsciousTime;
        Debug.Log("collision caused unconscious");
        player.GetComponent<Rigidbody>().useGravity = true;
    }

    void Rest()
    {
        if(Time.time < restTime)
        {
            if(restCounter > 100)
            {
                restCounter = 0;
                exhaustion--;
            }
        }
        if(exhaustion > maxExhaustion)
        {
            restTime = Time.time + 15;
            Debug.Log("Resting for 15seconds");
        }
    }




    private void Blocking()
    {
        enemies = GameObject.FindGameObjectsWithTag("Slytherin");
        friends = GameObject.FindGameObjectsWithTag("Griffindor");

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;


        foreach (GameObject friend in friends)
        {
            if (friend.transform != player.transform)
            {
                if ((snitch.transform.position - friend.transform.position).magnitude < (snitch.transform.position - player.transform.position).magnitude) // not the closest on team to snitch
                {
                    foreach (GameObject enemy in enemies)
                    {
                        Vector3 diff = enemy.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            closest = enemy;
                            distance = curDistance;
                        }
                    }

                    float dist = Vector3.Distance(transform.position, closest.transform.position);
                    Vector3 dir = (closest.transform.position - transform.position);
                    dir.Normalize();
                    player.AddForce(dir * dist);

                }
            }
        }
    }

    


}
