using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
	/////References/////
	[SerializeField] TuningSO tuning;

	/////Initialization/////
	void Start()
	{
		TuningHolder holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);

		Pickup pickup = gameObject.AddComponent<Pickup>();
		pickup.OnPickedUp += DoEffect;
	}

	public void DoEffect(GameObject target)
	{
        target.GetComponent<Health>().GetHealed(gameObject,GetComponent<Stats>().GetInt("intstrength"));
	}
}
