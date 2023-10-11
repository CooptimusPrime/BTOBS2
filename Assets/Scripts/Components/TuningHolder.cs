using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuningHolder : MonoBehaviour
{
	TuningSO tuning;

	public void SetTuning(TuningSO tuning)
	{
		this.tuning = tuning;
	}

	public TuningSO GetTuning() 
	{
		return this.tuning;
	}
}
