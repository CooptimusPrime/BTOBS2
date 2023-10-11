using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    /////References/////
    Stats stats;

    /////Component Variables/////
    float spawn;
    float lifespan;

    /////Initialization/////
    void Start()
    {
        stats = GetComponent<Stats>()??gameObject.AddComponent<Stats>();
        stats.FloatChanged += LifespanChanged;

        spawn = Time.time;
    }

	void OnEnable()
	{
		spawn = Time.time;
	}

    /////Stats/////
    void LifespanChanged(string stat)
    {
        if (stat == "lifespan")
            lifespan = stats.GetFloat("lifespan");
    }

	////Component Functions/////
	void Update()
    {
        if (lifespan>0&&Time.time-spawn > lifespan)
            gameObject.SetActive(false); //might want to revisit to cause ondeath
    }

    public void SetLifespan(float lifespan)
    {
        this.lifespan = lifespan;
    }

    //for directly altering the current life
    public void DoDelta(float delta)
    {
        spawn -= delta;
    }

    //probably will want a function to pause aging
}
