using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour

     
{

    [Range(0.1f, 10)]
    public float newTimeScale;
    // Start is called before the first frame update
    void Start()
    {
        newTimeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = newTimeScale;
    }
}
