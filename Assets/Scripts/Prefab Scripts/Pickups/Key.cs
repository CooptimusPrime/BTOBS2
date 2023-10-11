using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Needs finishing

public class Key : MonoBehaviour
{
    [SerializeField] TuningSO tuning;
    TuningHolder holder;

    Material material;
    void Start()
    {
        holder=gameObject.AddComponent<TuningHolder>();
        holder.SetTuning(tuning);


    }
}
