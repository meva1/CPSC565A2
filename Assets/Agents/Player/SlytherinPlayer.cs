using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlytherinPlayer : MonoBehaviour
{
    public Rigidbody player;
    public GameObject snitch;
    private float maxSpeed;



    private void Awake()
    {
        maxSpeed = 5f;

        // Extract rigid body
        player = GetComponent<Rigidbody>();
        //snitch = GetComponent<Rigidbody>;
    }
    // Start is called before the first frame update
    void Start()
    {
        player.mass = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, snitch.transform.position);
        Vector3 dir = (snitch.transform.position - transform.position);
        dir.Normalize();
        player.AddForce(dir * dist);

        RepellWalls();

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
    }

    void RepellWalls()
    {
        // add a repulsive force from each wall that scales with 1/r^2

        float xPos = -1 / ((25 - player.transform.position.x) * (25 - player.transform.position.x) * (25 - player.transform.position.x));
        float xNeg = 1 / ((-25 - player.transform.position.x) * (-25 - player.transform.position.x) * (-25 - player.transform.position.x));

        float zPos = -1 / ((25 - player.transform.position.z) * (25 - player.transform.position.z) * (25 - player.transform.position.z));
        float zNeg = 1 / ((-25 - player.transform.position.z) * (-25 - player.transform.position.z) * (-25 - player.transform.position.z));

        float yPos = -1 / ((25 - player.transform.position.y) * (25 - player.transform.position.y) * (25 - player.transform.position.y));
        float yNeg = 1 / ((0 - player.transform.position.y) * (0 - player.transform.position.y) * (0 - player.transform.position.y));

        Vector3 forceDir = new Vector3(xPos + xNeg, yPos + yNeg, zPos + zNeg);
        player.AddForce(forceDir);
    }
}
