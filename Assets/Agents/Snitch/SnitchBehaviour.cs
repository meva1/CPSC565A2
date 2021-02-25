using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchBehaviour : MonoBehaviour
{
    public int seed;
    public Rigidbody snitch;

    private System.Random rng;

    void Awake()
    {
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
    }
}
