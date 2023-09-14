using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
	float lastshoottime=0;
	int clipammo;
	int ammo;
	int hands;

	bool emptied = false;

	[SerializeField] GameObject bulletprefab, altprefab;
	AudioSource source;
	[SerializeField] AudioClip fire, finalshot, empty, eject, reload, pull, release;
	WeaponTuningSO tuning;
	ObjectPool pool;
	GameObject muzzle;

	//stats grabbed from the WeaponTuningSO;
	Dictionary<string, int> intstats = new Dictionary<string, int>()
	{
		{"damage",0},
		{"clip",0},
		{"maxammo",0},
		{"burst",0},
		{"mode",0}, //in the future, will need to support switching mode
		{"consumption",0}
	};

	Dictionary<string, float> floatstats = new Dictionary<string, float>()
	{
		{"bulletspeed",0},
		{"reloadspeed",0},
		{"firerate",0},
		{"accuracy",0},
		{"stability",0},
		{"apenalty",0}, //if not using two hands when it is two-handed
		{"spenalty",0} //same
	};

	public int GetIntStat(string stat)
	{
		if (intstats.ContainsKey(stat))
			return intstats[stat];
		else 
			return 0;
	}
	public void DoDeltaInt(string stat,int amount)
	{
		if (intstats.ContainsKey(stat))
			intstats[stat] += amount;
	}

	public float GetFloatStat(string stat)
	{
		if (floatstats.ContainsKey(stat))
			return floatstats[stat];
		else
			return 0;
	}
	public void DoDeltaFloat(string stat,float amount)
	{
		if (floatstats.ContainsKey(stat))
			floatstats[stat] += amount;
	}

	public delegate void Fire(GameObject wep);
	public event Fire OnFire;
	public delegate void AltFire(GameObject wep);
	public event AltFire OnAltFire;
	public delegate void EmptyFire(GameObject wep);
	public event EmptyFire OnEmptyFire;
	public delegate void Empty(GameObject wep);
	public event Empty OnEmpty;
	public delegate void Reload(GameObject wep);
	public event Reload OnReload;
	public delegate void Reloaded(GameObject wep);
	public event Reloaded OnReloaded;



	void Start()
	{
		source=gameObject.AddComponent<AudioSource>();
		source.playOnAwake = false;
		//probably need to set to 3D

		//automatically assign the tuning SO
		//iterate through the int and float stats, and also set the hands int

		ammo = intstats["maxammo"];
		clipammo = intstats["clipammo"];

		pool=gameObject.AddComponent<ObjectPool>();
		pool.prefab = bulletprefab;

		//Events
		OnFire += FireBase;
		OnAltFire += AltFireBase;
		OnEmpty += EmptyBase;
		OnReload += ReloadBase;
		OnReloaded += ReloadedBase;
	}

	//OnFire event needs to check this first
	void CanFire(GameObject wep)
	{	
		if (Time.time-lastshoottime>=intstats["firerate"])
		{
			if (clipammo > 0 || intstats["consumption"] == 0)
				OnFire.Invoke(wep);
			else if (OnEmptyFire != null)
				OnEmptyFire(wep);
			else
				source.PlayOneShot(empty);
		}
	}

	//OnAltFire needs to check this first
	void CanAltFire(GameObject wep)
	{
		//might want to handle this case by case
	}

	//Base Actions
	void FireBase(GameObject wep)
	{ 
		
	}

	void AltFireBase(GameObject wep)
	{
		return;
	}

	void EmptyBase(GameObject wep)
	{
		//play emptied sound
		//set to empty
	}

	void ReloadBase(GameObject wep)
	{
		//play reload sound
		//start a timer of reloadtime and then push reloaded
	}

	void ReloadedBase(GameObject wep)
	{
		
	}

	void OnEnable()
	{
		ammo = intstats["clip"];
	}

	public void Shoot()
	{
			clipammo--;
			//push fired event
			lastshoottime = Time.time;

			GameObject bulletparent = pool.GetObject();
			GameObject bullet = bulletparent.transform.GetChild(0).gameObject;

			if (bullet.CompareTag("enemybullet"))
				bullet.GetComponent<EnemyBullet>().damage = intstats["damage"];
			else
				bullet.GetComponent<PlayerBullet>().damage = intstats["damage"];

			bulletparent.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);

			bullet.GetComponent<Rigidbody>().velocity = muzzle.transform.forward * floatstats["bulletspeed"];

			source.PlayOneShot(fire);
	}
}
