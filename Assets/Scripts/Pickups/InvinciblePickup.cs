using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePickup : Pickup
{
	float InvTime = 15;
	public override void Apply(GameObject player)
	{
		player.GetComponent<Health>().invincible = true;

		//after InvTime expires, needs to not be invincible
	}
}
