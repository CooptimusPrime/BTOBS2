using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Still need to handle owner, holster, and returning to holster when dropped
//Knowing how many hands are on the gun

public class Weapon : MonoBehaviour
{
	/////References/////
	Stats stats;
	ObjectPool pool, altpool;
	AudioSource source;
	AudioClip fire, emptied, empty, reload;
	GameObject primaryprefab, altprefab;
	Transform muzzle, altmuzzle; //***these must be children of the gameobject this component is attached to, not nested children***
	GameObject owner; //player, enemy, ally, or something neutral, maybe try to make a common component for them
	LineRenderer line;

	/////Component Variables/////
	float lastshoottime;
	int clipammo;
	int curammo;
	bool isempty, isfiring, isreloading;

	/////Stats that can be retuned/////
	Dictionary<string, float> floatstats;
	Dictionary<string, int> intstats;
	Dictionary<string, bool> boolstats;

	/////Events/////
	public event Action<GameObject> OnFire; //the bullet
	public event Action<GameObject> OnAltFire; //the bullet
	public event Action<GameObject,int> OnHitOther; //obj is who/ what you hit, int is the damage you dealt
	public event Action OnMiss; 
	public event Action OnEmpty; 
	public event Action<GameObject> OnEmptyFire; //the bullet
	public event Action OnReload; //initiating the reload
	public event Action OnReloaded; //when actually reloaded
	public event Action<GameObject> OnDownedOther; //who/ what we downed
	public event Action<GameObject> OnKilledOther; //who/ what we killed

	/////Initialization/////
	void Start()
	{
		//Stats component
		stats = GetComponent<Stats>() ?? gameObject.AddComponent<Stats>();
		stats.IntChanged += UpdateInt;
		stats.FloatChanged += UpdateFloat;
		stats.BoolChanged += UpdateBool;
		intstats = stats.SetUpInts();
		floatstats = stats.SetUpFloats();
		boolstats = stats.SetUpBools();
		fire = stats.GetSound("fire");
		emptied = stats.GetSound("emptied");
		empty = stats.GetSound("empty");
		reload = stats.GetSound("reload");
		primaryprefab = stats.GetObject("primaryprefab");
		altprefab = stats.GetObject("altprefab");

		//Component variables for keeping track of current state of ammo
		clipammo = stats.GetInt("clip"); //intstats["clip"];
		Debug.Log("Clipammo is"+ clipammo);
		curammo = Mathf.CeilToInt(intstats["maxammo"] * 0.4f);

		//Object Pools
		pool=gameObject.AddComponent<ObjectPool>();
		pool.prefab = primaryprefab;
		if (altprefab&&altprefab!=primaryprefab)
		{
			altpool = gameObject.AddComponent<ObjectPool>();
			altpool.prefab = altprefab;
		}

		//Audio
		source = gameObject.AddComponent<AudioSource>();

		//Muzzle for shooting
		muzzle = transform.Find("Muzzle"); 
		//altmuzzle = transform.Find("AltMuzzle");

		//Line Renderer
		if (intstats["firemode"] == 3)
		{
			line = muzzle.gameObject.AddComponent<LineRenderer>();
			//set line range
		}

		//Actions
		OnFire += BaseOnFire;
		//OnAltFire += BaseOnAltFire;
		OnHitOther += BaseOnHitOther;
		OnMiss += BaseOnMiss;
		OnEmpty += BaseOnEmpty;
		OnReload += BaseOnReload;
		OnReloaded += BaseOnReloaded;
		OnDownedOther += BaseOnDownedOther;
		OnKilledOther += BaseOnKilledOther;
	}

	void OnEnable()
	{
		if (stats)
		{
			clipammo = stats.GetInt("clip");
			curammo = Mathf.CeilToInt(stats.GetInt("maxammo") * 0.4f);
		}
	}

	/////Functions for Stats/////
	public void UpdateInt(string name)
	{
		if (intstats.ContainsKey(name))
			intstats[name] = stats.GetInt(name);
	}
	public void UpdateFloat(string name)
	{
		if (floatstats.ContainsKey(name))
			floatstats[name] = stats.GetFloat(name);
	}
	public void UpdateBool(string name)
	{
		if (boolstats.ContainsKey(name))
			boolstats[name] = stats.GetBool(name);
	}

	/////Verifiers/////
	bool CanFire() //for now, should automatically reload, but don't want that in the future
	{
		if (boolstats["canfire"] && Time.time - lastshoottime >= floatstats["firerate"] && (clipammo > 0 || OnEmptyFire != null))
			return true;
		else if (clipammo<=0)
		{
			TryReload();
			return false;
		}
		return false;
	}

	void CanAltFire()
	{
		//not in mvp
	}

	/////Listeners/////
	public void TryFire(bool start) 
	{
		if (start)
		{
			if (intstats["firemode"] == 1)
				Shoot();
			else if (!isfiring)
			{
				isfiring = true;
				StartCoroutine(Fire());
				if (intstats["firemode"] == 3)
					Laser(true);
			}
		}
		else
		{
			StopCoroutine(Fire());
			isfiring = false;
			Laser(false);
		}
    }

	public bool CheckEmpty()
	{
		return isempty; 
	}

	public void TryAltFire()
	{
		//not in mvp
	}

	public void TryReload()
	{
		if (!isreloading)
			OnReload.Invoke();
	}

	public void DoDeltaAmmo(int amount, bool clip=false) //revisit protections for this, don't exactly want it public
	{
		if (clip)
			Mathf.Clamp(clipammo += amount, 0, intstats["clip"]); //maybe take from reserves? Some cases might just want to empty the clip fully though
		else
			Mathf.Clamp(curammo += amount, 0, intstats["maxammo"]);
	}

	public void HitOther(GameObject target, int damage)
	{
		OnHitOther.Invoke(target, damage);
	}

	public void KilledOther(GameObject target)
	{
		OnKilledOther.Invoke(target);
	}

	public void DownedOther(GameObject target)
	{
		OnDownedOther.Invoke(target);
	}

	/////Base Event Functions/////
	void BaseOnFire(GameObject bullet)
	{
		//stats.DoDelta("shots", 1);
	}

	//void BaseOnAltFire(GameObject bullet)

	void BaseOnEmpty()
	{
		if(emptied!=null)
			source.PlayOneShot(emptied);
		isempty = true;
	}

	void BaseOnReload()
	{
		isreloading = true;
		stats.DoDelta("canfire", 0, 0, false);

		IEnumerator DoReload()
		{
			yield return new WaitForSeconds(floatstats["reloadtime"]);
			OnReloaded.Invoke();
		}

		StartCoroutine(DoReload());
	}

	void BaseOnReloaded() //keeping ammo infinite for now
	{
		/*int missing=0;

		if (!owner.CompareTag("Enemy"))
		{
			curammo -= (intstats["clip"] - clipammo);
			if (curammo<0)
			{
				missing = 0 - curammo;
				curammo = 0; 
			}
		}
		
		clipammo = intstats["clip"]-missing;
		*/
		clipammo = intstats["clip"]; //will need to remove this line in the future when we use finite ammo
		stats.DoDelta("canfire", 0, 0, true);
		isempty = false;
		isreloading = false;
	}

	void BaseOnHitOther(GameObject target, int damage) //damage is what was actually dealt
	{
		stats.DoDelta("hits", 1);
		stats.DoDelta("dealt", damage);
	}

	void BaseOnMiss()
	{
		//purposely blank to pass null checks
	}

	void BaseOnDownedOther(GameObject target)
	{
		stats.DoDelta("downs", 1);
	}

	void BaseOnKilledOther(GameObject target)
	{
		stats.DoDelta("kills", 1);
	}

	/////Component Functions/////
	IEnumerator Fire()
	{
		while (true)
		{
			if (intstats["firemode"] != 4)
			{
				Shoot();
				yield return new WaitForSeconds(intstats["firerate"]);
			}
			else
			{
				//start the charge animation
				//play the charge noise
				yield return new WaitForSeconds(floatstats["firerate"]);
				Shoot();
			}
		}
	}

	void Shoot() //what about firing directly from the reserves? or empty fire just coming from the reserves?
	{
		for (int i = 0; i < intstats["burst"]; i++) //probably need a small break between each run for burst
		{
			if (CanFire())
			{
				clipammo -= intstats["consumption"];

				lastshoottime = Time.time;

				GameObject bulletparent = pool.GetObject();
				GameObject bullet = bulletparent.transform.GetChild(0).gameObject; //change this to look for the name of the bullet?
				bulletparent.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
				bullet.GetComponent<Rigidbody>().velocity = muzzle.transform.forward * floatstats["bulletspeed"];
				bullet.GetComponent<Projectile>().SetStats(gameObject, intstats["damage"], floatstats["range"] / floatstats["bulletspeed"], 1, intstats["element"], intstats["piercing"], floatstats["homing"], boolstats["isexplosive"]);
				if (boolstats["isexplosive"])
					bullet.GetComponent<Explosive>().SetExplosionPrefab(stats.GetObject("explosionprefab"));

				OnFire.Invoke(bullet);
				if (intstats["firemode"]!=3)
					source.PlayOneShot(fire); //laser will want to play it as a loop while firing

				if (clipammo == 0 && !isempty)
					OnEmpty.Invoke();
				else if (clipammo < 0 && OnEmptyFire != null)
					OnEmptyFire.Invoke(bullet);
			}
			else
			{
				if (empty!=null)
					source.PlayOneShot(empty);
			}
		}
	}
	
	void Laser(bool on)
	{
		if (on)
		{
			line.enabled = true;
			source.clip = fire;
			source.loop = true;
			source.Play();
		}
		else
		{
			line.enabled = false;
			source.Stop();
		}
	}

	/////Getters/////
	public int GetAmmo()
	{
		return clipammo;
	}
}