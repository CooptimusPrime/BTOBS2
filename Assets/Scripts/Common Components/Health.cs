using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	public int max=100;
	int current;

	//When damaged
	Renderer mesh;
	Color normal;
	Color flash = Color.white;
	float flashtime = 0f;
	[SerializeField] AudioClip hit, block;
	AudioSource source;
	public bool invincible;

	public UnityEvent<GameObject,int> OnHit; //what hit me, how much
	public UnityEvent<GameObject,int> OnAfterHit; //what hit me, how much, only fires if still alive after getting hit
	public UnityEvent<GameObject,int> OnHeal; //what healed me, how much
	public UnityEvent<GameObject> OnDeath; //what killed me

	public Action<GameObject, int> Hit;
	public Action<GameObject, int> AfterHit;
	public Action<GameObject, int> Heal;
	public Action<GameObject, int> Death;


	void Start()
	{
		current = max;
		mesh = gameObject.GetComponent<Renderer>();
		normal = mesh.material.color;
		source = gameObject.GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		current = max;
	}

	public void GetHit(int damage, GameObject attacker)
	{
		AudioClip sound = hit;
		if (invincible)
		{
			damage = 0;
			sound = block;
		}
		OnHit.Invoke(damage, attacker);
		source.PlayOneShot(sound);
		mesh.material.color = flash;
		flashtime = 0.3f;
		AfterHit.Invoke(damage,attacker);
	}

	public void DoDelta(int amount,GameObject source)
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
			OnHeal.Invoke(amount,source);
	}

	public void AddRegen(float duration,int amount,float tick,GameObject source)
	{
	
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
}
