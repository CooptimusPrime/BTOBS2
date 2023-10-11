using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifespanOneOff : MonoBehaviour
{
	[SerializeField] float lifespan;
	float spawn;

	void Start()
	{
		spawn = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (lifespan > 0 && Time.time - spawn > lifespan)
			gameObject.SetActive(false); //might want to revisit to cause ondeath
	}
}
