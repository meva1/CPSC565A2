using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{

    private int numPlayers;

    private System.Random rng;

    private double sMeanWeight;
    private double sStddevWeight;
    private double sMeanMaxVelocity;
    private double sStddevMaxVelocity;
    private double sMaxAggressiveness;
    private double sMeanAggressiveness;
    private double sStddevAggressiveness;
    private double sMeanMaxExhaustion;
    private double sStddevMaxExhaustion;

    private double gMeanWeight;
    private double gStddevWeight;
    private double gMeanMaxVelocity;
    private double gStddevMaxVelocity;
    private double gMaxAggressiveness;
    private double gMeanAggressiveness;
    private double gStddevAggressiveness;
    private double gMeanMaxExhaustion;
    private double gStddevMaxExhaustion;

    private Vector3 slytherinStartPoint;
    private Vector3 griffindorStartPoint;

    public GameObject SlytherinPlayer;
    public GameObject GriffindorPlayer;

    private List<GameObject> teamSlytherin;
    private List<GameObject> teamGriffindor;

    private Rigidbody rig;

    void Awake()
    {
        rng = new System.Random();

        slytherinStartPoint = new Vector3(0, 0.1f, 5);
        griffindorStartPoint = new Vector3(0, 0.1f, -5);

        numPlayers = 7;
        sMeanWeight = 85;
        sStddevWeight = 17;
        sMeanMaxVelocity = 16;
        sStddevMaxVelocity = 2;
        sMeanAggressiveness = 30;
        sStddevAggressiveness = 7;
        sMeanMaxExhaustion = 50;
        sStddevMaxExhaustion = 15;

        gMeanWeight = 75;
        gStddevWeight = 12;
        gMeanMaxVelocity = 18;
        gStddevMaxVelocity = 2;
        gMeanAggressiveness = 22;
        gStddevAggressiveness = 3;
        gMeanMaxExhaustion = 65;
        gStddevMaxExhaustion = 13;

        

}
    // Start is called before the first frame update
    void Start()
    {
        CreateGriffindor();
        CreateSlytherin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    double SampleGaussian(System.Random random, double mean, double stddev)
    {
        // The method requires sampling from a uniform random of (0,1]
        // but Random.NextDouble() returns a sample of [0,1).
        double x1 = 1 - random.NextDouble();
        double x2 = 1 - random.NextDouble();

        double y1 = System.Math.Sqrt(-2.0 * System.Math.Log(x1)) * System.Math.Cos(2.0 * System.Math.PI * x2);
        return y1 * stddev + mean;
    }

    void CreateGriffindor()
    {
        float spawnPointX;
        teamGriffindor = new List<GameObject>();
        for (int i = 0; i < numPlayers; i++)
        {
            spawnPointX = griffindorStartPoint.x + i; 
            GameObject griff = Instantiate(GriffindorPlayer, griffindorStartPoint+new Vector3(spawnPointX,0f,0f), Quaternion.identity);
            rig = griff.GetComponent<Rigidbody>();
            rig.mass = (float)SampleGaussian(rng, gMeanWeight, gStddevWeight);
            GriffindorPlayer stats = griff.GetComponent<GriffindorPlayer>();
            stats.aggressiveness = SampleGaussian(rng, gMeanAggressiveness, gStddevAggressiveness);
            stats.maxExhaustion = SampleGaussian(rng, gMeanMaxExhaustion, gStddevMaxExhaustion);
            stats.exhaustion = 0;
            stats.maxSpeed = SampleGaussian(rng, gMeanMaxVelocity, gStddevMaxVelocity);

            teamGriffindor.Add(griff);
        }
    }

    void CreateSlytherin()
    {
        float spawnPointX;
        teamSlytherin = new List<GameObject>();
        for (int i = 0; i < numPlayers; i++)
        {
            spawnPointX = slytherinStartPoint.x + i;
            GameObject sly = Instantiate(SlytherinPlayer, slytherinStartPoint+ new Vector3(spawnPointX,0f,0f), Quaternion.identity);
            rig = sly.GetComponent<Rigidbody>();
            rig.mass = (float)SampleGaussian(rng, sMeanWeight, sStddevWeight);
            SlytherinPlayer stats = sly.GetComponent<SlytherinPlayer>();
            stats.aggressiveness = SampleGaussian(rng, sMeanAggressiveness, sStddevAggressiveness);
            stats.maxExhaustion = SampleGaussian(rng, sMeanMaxExhaustion, sStddevMaxExhaustion);
            stats.exhaustion = 0;
            stats.maxSpeed = SampleGaussian(rng, sMeanMaxVelocity, sStddevMaxVelocity);

            teamSlytherin.Add(sly);
        }
    }
}
