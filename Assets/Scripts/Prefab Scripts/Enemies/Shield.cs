using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] TuningSO tuning;
    TuningHolder holder;

    // Start is called before the first frame update
    void Start()
    {
        holder = gameObject.AddComponent<TuningHolder>();
        holder.SetTuning(tuning);

        gameObject.AddComponent<Stats>();
        gameObject.AddComponent<Health>();

        gameObject.tag = "Enemy";
    }
}
