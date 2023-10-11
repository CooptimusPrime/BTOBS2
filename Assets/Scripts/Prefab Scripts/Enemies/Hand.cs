using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//just filler, should redo this
public class Hand : MonoBehaviour
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
