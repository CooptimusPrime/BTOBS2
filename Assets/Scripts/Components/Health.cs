using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	int max;
	int current;
	bool invincible;
	[SerializeField] bool downable;
	bool isdowned;
	bool isdead;

	//When damaged, flash so others know
	Renderer mesh;
	Color normal;
	Color flash = Color.white;
	float flashtime = 0.3f;

	[SerializeField] AudioClip hit, block;
	AudioSource audiosource;

	public event Action<GameObject, int> OnHit;
	public event Action<GameObject, int> OnAfterHit;
	public event Action<GameObject, int> OnHealed;
	public event Action<GameObject> OnDowned;
	public event Action<GameObject> OnDeath;
	public event Action<GameObject> OnReloaded;

	void Start()
	{
		//get your max health from the stats component
		current = max;
		mesh = gameObject.GetComponent<Renderer>();
		normal = mesh.material.color;
		audiosource = gameObject.GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		//get your max from the SO
		current = max;
	}

	public void GetHit(GameObject source, int damage, string element="none")
	{
		AudioClip sound = hit;
		if (invincible)
		{
			damage = 0;
			sound = block;
		}

		//check damage type and resistances and reduce as necessary (ranged, melee, explosive, and anything else later)

		current -= damage;
		audiosource.PlayOneShot(sound);
		OnHit.Invoke(source, damage);

		if (current > 0)
			OnAfterHit.Invoke(source, damage);
		else if (downable)
			OnDowned.Invoke(source);
		else
			OnDeath.Invoke(source);
	}

	public void DoDelta(GameObject source, int amount)
	{
		current += amount;
		if (current > max)
		{
			amount = 0;
			current = max;
		}
		else if (current < 0)
		{
			current = 0;
			OnDeath.Invoke(source);
		}
		if (amount > 0)
			OnHealed.Invoke(source,amount);
	}

	void Update() //needs to be rewritten as an event
	{
		if (flashtime > 0)
		{
			flashtime -= Time.deltaTime;
			if (flashtime <= 0)
			{
				mesh.material.color = normal;
			}
		}
	}

	void BaseOnHit(GameObject source, int damage)
	{
		//get the owner of the source, let them know they hit us and for how much damage
		//flash
		mesh.material.color = flash;
		flashtime = 0.3f;
	}

	void BaseOnAfterHit(GameObject source, int damage)
	{
		//empty so it doesnt throw an error
	}

	void BaseOnHealed(GameObject source, int amount)
	{
		//get the grand owner of the source, let them know how much healing they did for us	
	}

	void BaseOnDowned(GameObject source)
	{
	
	}

	void BaseOnDeath(GameObject source)
	{

	}
}
