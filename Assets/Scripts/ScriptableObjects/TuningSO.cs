using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameObjects, AudioClips, and Images maybe should be handled elsewhere
//also need to rebuild this so the dictionaries only use ones that have values
//Inheritance for different types of things

public class TuningSO : ScriptableObject
{
	/*

	[SerializeField] Dictionary<string, int> intstats = new Dictionary<string, int>()
	{
		{"maxammo",0},
		{"consumption",0},
		{"clip",0},
		{"burst",0},
		{"firemode",0},
		{"damage",0},
		{"element",0},
		{"type",0},
		{"piercing",0},

		{"maxhealth",0},
		{"maxdowns",0},

		{"score",0},

		{"intstrength",0},
	};
	[SerializeField] Dictionary<string, float> floatstats = new Dictionary<string, float>()
	{
		{"firerate",0},
		{"bulletspeed",0},
		{"accuracy",0},
		{"accuracy2",0},
		{"stability",0},
		{"stability2",0},
		{"reloadtime",0},
		{"range",0},
		{"homing",0},

		{"lootchance",0},

		{"duration",0},
		{"floatstrength",0},

		{"speed",0},
		{"sprintspeed",0},
		{"stamina",0},

		{"lifespan",0},
		{"timefactor",0},
	};
	[SerializeField] Dictionary<string, bool> boolstats = new Dictionary<string, bool>()
	{
		{"canfire",false},
		{"isexplosive",false},

		{"isinvincible",false},
	};
	[SerializeField] Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>()
	{
		{"primaryprefab", null},
		{"altprefab", null},
		{"explosionprefab", null},

		{"deathprefab", null}
	};
	[SerializeField] Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>()
	{
		{"fire",null},
		{"reload",null},
		{"emptied",null},
		{"empty",null},

		{"hit",null},
		{"block",null},
		{"death",null},
		{"downed",null},
		{"heal",null},

		{"move",null},
		{"sprint",null},
		{"crawl",null},
		{"crouch",null},
		{"fall",null},
		{"jump",null},

		{"pickedup",null},
	};

	public Dictionary<string,int> GetIntStats()
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
	*/

	/********Int********
	 ** Weapon **
	 * maxammo
	 * consumption
	 * clip
	 * burst
	 * firemode (1 - Semi, 2 - Auto, 3 - Laser)
	 * damage
	 * element (0 - None, 
	 * type (1 - Ranged, 2 - Melee)
	 * piercing 
	 *
	 ** Health **
	 * maxhealth
	 * maxdowns
	 * 
	 ** Lootdropper **
	 * score
	 * pickupchance
	 * 
	 ** Pickup **
	 * intstrength
	*/

	/*******Float********
	 ** Weapon **
	 * firerate
	 * bulletspeed
	 * accuracy1
	 * accuracy2
	 * stability1
	 * stability2
	 * reloadtime
	 * range
	 * homing (degrees the trajectory can be adjusted by each second)
	 * 
	 ** Lootdropper **
	 * lootchance
	 * 
	 ** Pickup **
	 * duration
	 * floatstrength
	 * 
	 ** Locomotor **
	 * speed
	 * sprintspeed
	 * stamina
	 * 
	 ** Lifespan **
	 * lifespan
	 * timefactor
	*/

	/*******Bool*********
	 ** Weapon ** 
	 * canfire
	 * isexplosive
	 *
	 ** Health **
	 * isinvincible
	*/

	/*******GameObject********
	 ** Weapon **
	 * primaryprefab
	 * altprefab
	 * 
	 * effects of all kinds
	 * death
	 * hit (which may be multiple too)
	 * skill (which may be multiple)
	*/

	/*******Audio Clip********
	 ** Weapon **
	 * fire
	 * reload (just one for now since reloading is automatic)
	 * emptied
	 * empty
	 * (probably the same for alt fire too)
	 * 
	 ** Health **
	 * hit
	 * block
	 * death
	 * heal
	 * 
	 ** Locomotor **
	 * move
	 * sprint
	 * crawl
	 * crouch
	 * fall
	 * jump
	 * 
	 ** Pickup **
	 * pickedup
	*/
}
