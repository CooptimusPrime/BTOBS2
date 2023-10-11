using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePickup : MonoBehaviour
{
	/////References/////
	[SerializeField] TuningSO tuning;
	Stats stats;

	/////Initialization/////
	 void Start()
	{
		TuningHolder holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);

		Pickup pickup = gameObject.AddComponent<Pickup>();
		pickup.OnPickedUp += DoEffect;

		stats = GetComponent<Stats>();
	}

	public void DoEffect(GameObject target)
	{
		bool boosted=false;
		
		WeaponHolder wepholder = target.gameObject.GetComponent<WeaponHolder>();
		Weapon[] weps = wepholder.GetWeapons();
		for (int i = 0; i < weps.Length; i++) 
		{
			if (weps[i] != null)
			{
				weps[i].gameObject.GetComponent<Stats>().SetIntStat("consumption", 0, stats.GetFloat("duration"));
				boosted = true;
			}
		}
		if (boosted)
		{
			if (TryGetComponent(out UIManager manager))
				manager.DoBuff("inv", stats.GetFloat("duration"));
		}
	}
}
