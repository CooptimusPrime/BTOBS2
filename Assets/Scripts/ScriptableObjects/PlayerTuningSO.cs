using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerTuningSO : TuningSO
{
	/////Stats/////
	[SerializeField]
	int
	maxhealth = 0,
	maxdowns = 0;

	[SerializeField]
	float
	speed = 0;

	[SerializeField]
	bool
	isinvincible;

	[SerializeField]
	GameObject
	deathprefab;

	[SerializeField]
	AudioClip
	hit = null,
	block = null,
	death = null,
	heal = null,
	background=null;

	/////Convert them into Dictionaries/////
	Dictionary<string, int> intstats = new Dictionary<string, int>();
	Dictionary<string, float> floatstats = new Dictionary<string, float>();
	Dictionary<string, bool> boolstats = new Dictionary<string, bool>();
	Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
	Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

	void CreateIntDictionary()
	{
		intstats["maxhealth"] = maxhealth;
		intstats["maxdowns"] = maxdowns;
	}
	void CreateFloatDictionary()
	{
		floatstats["speed"] = speed;
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
		sounds["hit"] = hit;
		sounds["block"] = block;
		sounds["death"] = death;
		sounds["heal"] = heal;
		sounds["background"] = background;
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
