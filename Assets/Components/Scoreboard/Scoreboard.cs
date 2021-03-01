using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject[] snitchTag;
    public GameObject snitch;
    public Text score;
    private SnitchBehaviour sb;
    // Start is called before the first frame update
    void Start()
    {
        snitchTag = GameObject.FindGameObjectsWithTag("snitch");
        snitch = snitchTag[0];
        sb = snitch.gameObject.GetComponent<SnitchBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Griffindor: " + sb.scoreGriffindor.ToString() + " Slytherin: " + sb.scoreGriffindor.ToString();
    }
}
