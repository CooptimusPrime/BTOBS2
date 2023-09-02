using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    int amount = 15;

	public override void Apply(GameObject player)
	{
        player.GetComponent<Health>().DoDelta(amount,gameObject);
	}
}
