using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] LayerMask Targets;
	[SerializeField] LayerMask Ground;
	Collider[] group = new Collider[16];

	[SerializeField] int range = 3;

	public int dmg;

	void Start()
	{
		int hits = Physics.OverlapSphereNonAlloc(transform.position, range, group, Targets);
		Damage(group, hits);
	}

	void Damage(Collider[] group, int hits)
	{
		for (int i = 0; i < hits; i++)
		{
			if (group[i].gameObject.activeInHierarchy && !group[i].GetComponent<Health>().IsDead)
			{
				float distance = Mathf.Abs(Vector3.Distance(transform.position, group[i].transform.position));
				if (!Physics.Raycast(transform.position, (group[i].transform.position - transform.position).normalized, distance, Ground))
				{
					float power = dmg * (1 - distance / range);
					power = Mathf.RoundToInt(power);
					group[i].GetComponent<Health>().GetHit((int)power);
				}
			}
		}
	}
}
