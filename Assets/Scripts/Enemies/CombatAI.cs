using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatAI : MonoBehaviour
{
	[SerializeField] Weapon weapon;
	[SerializeField] Transform player;
	[SerializeField] NavMeshAgent agent;

	[SerializeField] Transform muzzle;

	bool attacking;

	public float minattackrange; //must get to this to attack
	public float maxattackrange; //causes the enemy to stop shooting and then reposition

	void Start()
	{
		player = FindFirstObjectByType<PowerUps>().transform;
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
			weapon.Shoot();
	}

	void ChaseTarget()
	{
		agent.isStopped = false;
		agent.SetDestination(player.position);
	}
}
