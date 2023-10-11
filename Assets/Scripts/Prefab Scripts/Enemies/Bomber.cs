using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
	[SerializeField] TuningSO tuning;
	TuningHolder holder;

	// Start is called before the first frame update
	void Start()
	{
		holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);

		gameObject.AddComponent<Enemy>();
	}
}
