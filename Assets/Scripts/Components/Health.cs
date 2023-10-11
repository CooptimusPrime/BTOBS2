using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need max health, current health
/*Resistances Component
 * Types 
 * 1 - Ranged
 * 2 - Melee
 * Elements
 * 0 - Normal
 * 1 - Fire
 * 2 - Ice
 * 3 - Electric
 * 4 - Corrosive
 * 5 - Explosive
*/ 

/*Downable Component -- Called from health component
 * Max Downs
 * Downed Health
 * Downed Health Drain
 * Crawl Speed
 * Revived health modifier
 * Revived speed modifier
*/ 

/*Reviver Component -- Called by doing the revive action
 * Revive speed multiplier
 * revive health multiplier
 * track revives
*/ 

/*Trackers
 * Damage Taken
 * Healing 
 * Healing Given
 * Downs
 * Deaths
*/ 

public class Health : MonoBehaviour
{
	/////References/////
	Stats stats;
	AudioSource audiosource;
	AudioClip hit, block, death, heal;	
	Renderer mesh;
	Color normal;

	/////Component-Specific Variables/////
	bool isdowned, isdead;
	int current, downs;

	/////Retunable Stats/////
	bool isinvincible;
	int maxhealth, maxdowns;

	/////Events/////
	public event Action<GameObject, int> OnHit;
	public event Action<GameObject, int> OnAfterHit;
	public event Action<GameObject, int> OnHealed;
	public event Action<GameObject> OnDowned;
	public event Action<GameObject> OnDeath;

	/////Initialization/////
	void Start()
	{
		//Stats component
		stats = GetComponent<Stats>() ?? gameObject.AddComponent<Stats>();
		isinvincible = stats.GetBool("isinvincible");
		maxhealth = stats.GetInt("maxhealth");
		stats.BoolChanged += UpdateInvincibility;
		stats.IntChanged += UpdateMaxHealth;
		hit = stats.GetSound("hit");
		block = stats.GetSound("block");
		death = stats.GetSound("death");
		heal = stats.GetSound("heal");

		//Component References and Variables//
		current = stats.GetInt("maxhealth");
		mesh = gameObject.GetComponent<Renderer>();
		normal = mesh.material.color;
		audiosource = gameObject.GetComponent<AudioSource>();

		//Set up the Actions
		OnHit += BaseOnHit;
		OnAfterHit += BaseOnAfterHit;
		OnHealed += BaseOnHealed;
		OnDowned += BaseOnDowned;
		OnDeath += BaseOnDeath;
	}

	void OnEnable()
	{
		if (stats)
		{
			current = stats.GetInt("maxhealth");
			maxdowns = stats.GetInt("maxdowns");
		}
	}

	/////Stats Component/////
	public void UpdateMaxHealth(string stat)
	{
		if (stat == "maxhealth")
			maxhealth = stats.GetInt("maxhealth");
		else if (stat == "maxdowns")
			maxdowns = stats.GetInt("maxdowns"); 
	}
	public void UpdateInvincibility(string stat)
	{
		if (stat == "isinvincible")
			isinvincible = stats.GetBool("isinvincible");
	}

	/////Listeners/////
	public void GetHit(GameObject source, int damage, int type, int element=0)
	{
		AudioClip sound = hit;
		if (isinvincible)
		{
			damage = 0;
			sound = block;
		}

		//check damage type and resistances and reduce as necessary (ranged, melee, explosive, and anything else later)

		current -= damage;

		if (sound != null)
			audiosource.PlayOneShot(sound);

		OnHit.Invoke(source, damage);

		if (current > 0)
			OnAfterHit.Invoke(source, damage);
		else if (maxdowns>0)
			OnDowned.Invoke(source);
		else if (!isdead)
			OnDeath.Invoke(source);
	}

	public void GetHealed(GameObject source, int amount) //maybe explore overhealing and temp health later
	{;
		amount = Mathf.Min(amount, maxhealth - current);
		current += amount;
		if (current > maxhealth)
			current = maxhealth;
		if (amount>0) //might decide to later include healing even if the target was at max health
			OnHealed.Invoke(source,amount);
	}

	/////Base Event Functions/////
	void BaseOnHit(GameObject source, int damage)
	{
		StartCoroutine(Flash());
		if (source.TryGetComponent(out Weapon wep))
		{
			wep.HitOther(gameObject, damage);
		}

		//if the attack wasn't from a weapon but a skill or something?

		//log in stats who hit you?
		//if you have aggro, update that
	}

	IEnumerator Flash()
	{
		mesh.material.color = Color.white;
		yield return new WaitForSeconds(0.3f);
		mesh.material.color = normal;
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
		//let source/ owner know they downed us
		//go to downed state - start bleedout, set movement to crawl, can only use secondary
	}

	void BaseOnDeath(GameObject source)
	{
		//tell source they killed us
		if (source.TryGetComponent(out Weapon wep))
			wep.KilledOther(gameObject);

		//drop loot
		if (gameObject.TryGetComponent(out LootDropper dropper))
			dropper.DropLoot();

		//play the death sound
		if (death!=null)
			audiosource.PlayOneShot(death);

		//deactivate object if not player and no lootdropper
		if (!gameObject.CompareTag("Player")&&dropper==null)
			gameObject.SetActive(false);
	}

	void BaseOnRevived(GameObject source)
	{
		//tell source they revived us
		//go back to active state
		//get your base revive health back plus any bonuses from the source
	}

	//onhealedother

	//onrevivedother
}
