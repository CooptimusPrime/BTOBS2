using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to redo this

public class Explosion : MonoBehaviour
{
	[SerializeField] LayerMask Targets;
	[SerializeField] LayerMask Ground;
	Collider[] group = new Collider[16];

	[SerializeField] int range = 3;

	public int dmg;
	public GameObject source;

	void Start() //Need to check if this accounts for walls
	{
		int hits = Physics.OverlapSphereNonAlloc(transform.position, range, group, Targets);
		Damage(group, hits);
	}

	void Damage(Collider[] group, int hits)
	{
		for (int i = 0; i < hits; i++)
		{
			if (group[i].gameObject.activeInHierarchy)
			{
				float distance = Mathf.Abs(Vector3.Distance(transform.position, group[i].transform.position));
				if (!Physics.Raycast(transform.position, (group[i].transform.position - transform.position).normalized, distance, Ground))
				{
					float power = dmg * (1 - distance / range);
					power = Mathf.RoundToInt(power);
					group[i].GetComponent<Health>().GetHit(source,(int)power,3);
				}
			}
		}
	}
}
