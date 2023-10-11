using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PickupTuningSO : TuningSO
{
	/////Stats/////
	[SerializeField]
	int
	intstrength,
	maxhealth;

	[SerializeField]
	float
	duration = 0,
	floatstrength = 0,
	lifespan = 0;

	[SerializeField]
	bool
	isinvincible = true;

	[SerializeField]
	GameObject
	deathprefab = null;

	[SerializeField]
	AudioClip
	hit = null,
	death=null,
	pickedup = null;

	/////Convert them into Dictionaries/////
	Dictionary<string, int> intstats = new Dictionary<string, int>();
	Dictionary<string, float> floatstats = new Dictionary<string, float>();
	Dictionary<string, bool> boolstats = new Dictionary<string, bool>();
	Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
	Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

	void CreateIntDictionary()
	{
		intstats["maxhealth"] = maxhealth;
		intstats["intstrength"] = intstrength;
	}
	void CreateFloatDictionary()
	{
		floatstats["duration"] = duration;
		floatstats["floatstrength"] = floatstrength;
		floatstats["lifespan"] = lifespan;
	}
	void CreateBoolDictionary()
	{
		boolstats["isinvincible"] = isinvincible;
	}
	public void CreateObjectDictionary()
	{
		objects["deathprefab"] = deathprefab;
	}
	public void CreateSoundDictionary()
	{
		sounds.Add("hit", hit);
		sounds.Add("death", death);
		sounds["pickedup"] = pickedup;
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
