using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
	[SerializeField] WeaponTuningSO tuning;
	TuningHolder holder;

	void Start()
	{
		holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);
		gameObject.AddComponent<Weapon>();
	}
}

