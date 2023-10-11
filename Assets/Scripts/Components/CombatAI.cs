using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//change it so they check to see if they actually have a clear shot
//aggro needs to be built in too
public class CombatAI : MonoBehaviour
{
	/////References/////
	Weapon weapon;
	Transform player;
	NavMeshAgent agent;
	Transform muzzle;
	Stats stats;

	/////Component Variables/////
	bool attacking;

	float minattackrange; //must get to this to attack
	float maxattackrange; //causes the enemy to stop shooting and then reposition

	void Start()
	{
		stats = GetComponent<Stats>();
		minattackrange = stats.GetFloat("minattackrange");
		
		player = FindFirstObjectByType<Player>().transform;
		agent = GetComponent<NavMeshAgent>();
		weapon = GetComponentInChildren<Weapon>(); //what if there is more than one?
		muzzle = transform.Find("Muzzle");
	}
	void Update()
	{
		Vector3 dir = (player.position - muzzle.position).normalized;
		float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

		transform.eulerAngles = Vector3.up * angle;

		float dist = Vector3.Distance(transform.position, player.position);

		if (!attacking && dist > minattackrange)
			ChaseTarget();
		else if (dist <= minattackrange)
		{
			attacking = true;
			Attack();
		}
		else if (attacking && dist <= maxattackrange)
		{
			Attack();
		}
		else
		{
			attacking = false;
			ChaseTarget();
		}
	}

	void Attack()
	{
		agent.velocity = Vector3.zero;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		agent.isStopped = true;
		if (GetComponent<Kamikaze>())
			GetComponent<Kamikaze>().Explode();
		else if (weapon)
		{
			if (weapon.CheckEmpty())
				weapon.TryReload();
			else
				weapon.TryFire(true);
		}
	}

	void ChaseTarget()
	{
		agent.isStopped = false;
		agent.SetDestination(player.position);
	}
}
