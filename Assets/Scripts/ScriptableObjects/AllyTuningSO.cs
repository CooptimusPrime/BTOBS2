using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Needs to be done, this is still copy-pasted from Weapons

public class AllyTuningSO : TuningSO
{
	/////Stats/////
	[SerializeField]
	int
	maxhealth = 0;

	[SerializeField]
	float
	bulletspeed = 0,
	firerate = 0,
	range = 0,
	reloadtime = 0,
	accuracy = 0,
	accuracy2 = 0,
	stability = 0,
	stability2 = 0,
	homing = 0;

	[SerializeField]
	bool
	canfire = true,
	isexplosive = false;

	[SerializeField]
	GameObject
	primaryprefab = null,
	altprefab = null,
	explosionprefab = null;

	[SerializeField]
	AudioClip
	fire = null,
	reload = null,
	emptied = null,
	empty = null;

	/////Convert them into Dictionaries/////
	Dictionary<string, int> intstats = new Dictionary<string, int>();
	Dictionary<string, float> floatstats = new Dictionary<string, float>();
	Dictionary<string, bool> boolstats = new Dictionary<string, bool>();
	Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
	Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

	void CreateIntDictionary()
	{
		intstats["maxhealth"] = maxhealth;
	}
	void CreateFloatDictionary()
	{
		floatstats["bulletspeed"] = bulletspeed;
		floatstats["firerate"] = firerate;
		floatstats["range"] = range;
		floatstats["reloadtime"] = reloadtime;
		floatstats["accuracy"] = accuracy;
		floatstats["accuracy2"] = accuracy2;
		floatstats["stability"] = stability;
		floatstats["stability2"] = stability2;
		floatstats["homing"] = homing;
	}
	void CreateBoolDictionary()
	{
		boolstats["canfire"] = canfire;
		boolstats["isexplosive"] = isexplosive;
	}
	public void CreateObjectDictionary()
	{
		objects["primaryprefab"] = primaryprefab;
		objects["altprefab"] = altprefab;
		objects["explosionprefab"] = explosionprefab; //need to move this elsewhere
	}
	public void CreateSoundDictionary()
	{
		sounds["fire"] = fire;
		sounds["reload"] = reload;
		sounds["emptied"] = emptied;
		sounds["empty"] = empty;
	}

	public void CreateDictionaries()
	{
		CreateIntDictionary();
		CreateFloatDictionary();
		CreateBoolDictionary();
		CreateObjectDictionary();
		CreateSoundDictionary();
	}

	/////Getters/////
	public Dictionary<string, int> GetIntStats()
	{
		return intstats;
	}
	public Dictionary<string, float> GetFloatStats()
	{
		return floatstats;
	}
	public Dictionary<string, bool> GetBoolStats()
	{
		return boolstats;
	}
	public Dictionary<string, GameObject> GetObjects()
	{
		return objects;
	}
	public Dictionary<string, AudioClip> GetSounds()
	{
		return sounds;
	}
}
