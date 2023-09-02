using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	public int damage;
	static float lifetime = 5f;
	float shoottime;

	void OnEnable()
	{
		shoottime = Time.time;
	}

	void Update()
	{
		if (Time.time - shoottime >= lifetime)
			gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("player") && !other.CompareTag("playerbullet"))
		{
			if (GetComponent<Explosive>())
				GetComponent<Explosive>().Explode(damage);
			else if (other.GetComponent<Health>())
				other.GetComponent<Health>().GetHit(damage);
			gameObject.SetActive(false);
		}
	}
}
