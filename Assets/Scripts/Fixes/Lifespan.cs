using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    static float Max = 4;
    float Spawn;

    // Start is called before the first frame update
    void Start()
    {
        Spawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time-Spawn > Max)
            Destroy(gameObject);
    }
}
