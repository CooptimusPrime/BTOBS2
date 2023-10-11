using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
	[SerializeField] TuningSO tuning;
	TuningHolder holder;

	void Start()
	{
		holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);
		gameObject.AddComponent<Weapon>();
	}
}

