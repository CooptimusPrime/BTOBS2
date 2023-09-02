using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalFix : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y!=0.75f)
            transform.position=new Vector3(transform.position.x, 0.75f, transform.position.z);
    }
}
