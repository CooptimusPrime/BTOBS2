using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
	//Static spawn chances for enemies when they drop a pickup
	int HealthChance = 60;
	int InfiniteChance = 30;
	int InvincibilityChance = 10;

	[SerializeField] ObjectPool[] Pools;

	public void SpawnPickup(float chance, Vector3 pos)
	{
		float drop = Random.Range(0f,100f);
		if (drop <= chance)
		{
			GameObject obj;
			int type = Random.Range(1, 101);
			if (type <= HealthChance)
				obj = Pools[0].GetObject();
			else if (type <= HealthChance + InfiniteChance)
				obj = Pools[1].GetObject();
			else
				obj = Pools[2].GetObject();

			obj.transform.position = new Vector3(pos.x, pos.y + 0.1f, pos.z);
			obj.transform.eulerAngles = new Vector3(0, 0, 90);
		}
	}
}
