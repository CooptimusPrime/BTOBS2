using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePickup : MonoBehaviour
{
	/////References/////
	[SerializeField] TuningSO tuning;
	Pickup pickup;
	TuningHolder holder;
	Stats stats;

	/////Initialization/////
	void Start()
	{
		holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);
		
		pickup = gameObject.AddComponent<Pickup>();
		pickup.OnPickedUp += DoEffect;

		stats = GetComponent<Stats>();
	}

	/////Component Functions/////	
	public void DoEffect(GameObject target)
	{
		target.GetComponent<Stats>().DoDelta("isinvincible",0,0,true,stats.GetFloat("duration"));
		if (TryGetComponent(out UIManager manager))
			manager.DoBuff("inv", stats.GetFloat("duration"));
	}
}
   