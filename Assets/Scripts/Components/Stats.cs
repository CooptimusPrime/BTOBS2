using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    TuningSO tuning;
    TuningHolder holder;

    bool initialized;

    /////Component Variables/////
    //These are set in the SO directly
    Dictionary<string, float> startfloatstats = new Dictionary<string, float>();
	Dictionary<string, int> startintstats = new Dictionary<string, int>();
	Dictionary<string, bool> startboolstats = new Dictionary<string, bool>();
    Dictionary<string, GameObject> startobjects = new Dictionary<string, GameObject>();
    Dictionary<string, AudioClip> startsounds = new Dictionary<string, AudioClip>();

    //These are the ones that actually get modified during the game
    Dictionary<string,float> floatstats = new Dictionary<string,float>();
    Dictionary<string,int> intstats = new Dictionary<string,int>();
    Dictionary<string,bool> boolstats = new Dictionary<string,bool>();
    Dictionary<string,GameObject> objects = new Dictionary<string, GameObject>();
    Dictionary<string,AudioClip> sounds = new Dictionary<string,AudioClip>();

    /////Events/////
    public event Action<string> FloatChanged;
    public event Action<string> IntChanged;
    public event Action<string> BoolChanged;
    public event Action<string> ObjectChanged;
    public event Action<string> SoundChanged;

	/*Copy and paste this into components that need to track several stats
    Dictionary<string,int> intstats = new Dictionary<string,int>();
    Dictionary<string,float> floatstats = new Dictionary<string,float>();
    Dictionary<string,bool> boolstats = new Dictionary<string,bool>();
    Dictionary<string,GameObject> objects = new Dictionary<string,GameObject>();
    Dictionary<string,AudioClip> sounds = new Dictionary<string,AudioClip>();
      
    public void UpdateInt(string name)
    {
        if (intstats.ContainsKey(name))
            intstats[name]=stats.GetInt(name);
    }
    public void UpdateFloat(string name)
    {
        if (floatstats.ContainsKey(name))
            floatstats[name]=stats.GetFloat(name);
    }
    public void UpdateBool(string name)
    {
        if (boolstats.ContainsKey(name))
            boolstats[name]=stats.GetBool(name);
    }
    public void UpdateObject(string name)
    {
        if (objects.ContainsKey(name))
            objects[name]=stats.GetObject(name);
    }
    public void UpdateSound(string name)
    {
        if (sounds.ContainsKey(name))
            sounds[name]=stats.GetSound(name);
    }
      
    /////This and the next part in the start function of the component/////
    stats=GetComponent<Stats>()??gameObject.AddComponent<Stats>();
    stats.IntChanged+=UpdateInt;
    stats.FloatChanged+=UpdateFloat;
    stats.BoolChanged+=UpdateBool;
    stats.ObjectChanged+=UpdateObject;
    stats.SoundChanged+=UpdateSound;
      
    /////if you don't want the whole dictionary, these methods support an optional list of strings with the stats you do want/////
    intstats=stats.SetUpInts(); 
    floatstats=stats.SetUpFloats();
    boolstats=stats.SetUpBools();
    objects=stats.SetUpObjects();
    sounds=stats.SetUpSounds();
    */

    /////Initialization/////
    void Start()
    {
        holder=GetComponent<TuningHolder>();
        tuning = holder.GetTuning();
        WeaponTuningSO wep = tuning as WeaponTuningSO;
        EnemyTuningSO enemy = tuning as EnemyTuningSO;
        PickupTuningSO pickup = tuning as PickupTuningSO;
        AllyTuningSO ally = tuning as AllyTuningSO;
        PlayerTuningSO player = tuning as PlayerTuningSO;

        if (wep!=null)
        {
            wep.CreateDictionaries();
            startfloatstats = wep.GetFloatStats();
			startintstats = wep.GetIntStats();
			startboolstats = wep.GetBoolStats();
			startobjects = wep.GetObjects();
			startsounds = wep.GetSounds();
		} 
        else if (enemy!=null)
		{
            enemy.CreateDictionaries();
            startfloatstats = enemy.GetFloatStats();
			startintstats =enemy.GetIntStats();
			startboolstats = enemy.GetBoolStats();
			startobjects = enemy.GetObjects();
			startsounds = enemy.GetSounds();
		}
        else if (pickup!=null)
		{
            pickup.CreateDictionaries();
            startfloatstats = pickup.GetFloatStats();
			startintstats = pickup.GetIntStats();
			startboolstats = pickup.GetBoolStats();
			startobjects = pickup.GetObjects();
			startsounds = pickup.GetSounds();
		}
        else if (ally!=null)
		{
            ally.CreateDictionaries();
            startfloatstats = ally.GetFloatStats();
			startintstats = ally.GetIntStats();
			startboolstats = ally.GetBoolStats();
			startobjects = ally.GetObjects();
			startsounds = ally.GetSounds();
		}
        else if (player!=null)
		{
            player.CreateDictionaries();
            startfloatstats = player.GetFloatStats();
			startintstats = player.GetIntStats();
			startboolstats = player.GetBoolStats();
			startobjects = player.GetObjects();
			startsounds = player.GetSounds();
		}
        initialized = true;
	}
	void OnEnable() //what if we don't want to reset though?
	{
        if (initialized)
        {
            floatstats = startfloatstats;
            intstats = startintstats;
            boolstats = startboolstats;
            objects = startobjects;
            sounds = startsounds;
        }
	}

	/////Setting Up Other Components/////
	public Dictionary<string,int> SetUpInts()
	{
        return intstats;
    }
    public Dictionary<string,int> SetupInts(List<string> list)
    {
        Dictionary<string,int> temp = new Dictionary<string,int>();
        foreach (string s in list)
        {
            if (intstats.ContainsKey(s))
                temp[s] = intstats[s];
        }
        return temp;
    }
    public Dictionary<string,float> SetUpFloats()
    {
        return floatstats;
    }
    public Dictionary<string,float> SetUpFloats(List<string> list) 
    { 
        Dictionary<string,float> temp = new Dictionary<string,float>();
        foreach (string s in list)
        {
            if (floatstats.ContainsKey(s))
                temp[s] = floatstats[s];
        }
        return temp;
    }
    public Dictionary<string,bool> SetUpBools() 
    {  
        return boolstats;
    }
    public Dictionary<string,bool> SetUpBools(List<string> list)
    {
        Dictionary<string,bool> temp = new Dictionary<string,bool>();
        foreach (string s in list)
            temp[s] = boolstats[s];
        return temp;
    }
    public Dictionary<string,GameObject> SetUpObjects()
    {
        return objects;
    }
    public Dictionary<string,GameObject> SetUpObjects(List<string> list) 
    {
        Dictionary<string, GameObject> temp = new Dictionary<string, GameObject>();
        foreach (string s in list)
            temp[s] = objects[s];
        return temp;
    }

    /////Altering Stats/////
	public void DoDelta(string name, int amount = 0, float amountf = 0, bool value = false, float duration = 0) 
    {
        if (amount!=0)
        {
            if (intstats.ContainsKey(name))
            {
                intstats[name] += amount;
                IntChanged?.Invoke(name);
                if (duration>0)
                {
                    StartCoroutine(UndoDelta(name, duration, -amount));
                }
            }
        }
        else if (amountf!=0) 
        {
            if (floatstats.ContainsKey(name))
            {
                floatstats[name] += amount;
                FloatChanged?.Invoke(name);
                if (duration>0)
                {
                    StartCoroutine(UndoDelta(name, duration, 0, -amountf));
                }
            }
        }
        else
        {
            if (boolstats.ContainsKey(name))
            {
                boolstats[name] = value;
                BoolChanged?.Invoke(name);
                if (duration>0)
                {
                    StartCoroutine(UndoDelta(name, duration, 0, 0, !value));
                }
            }
        }
    }

	IEnumerator UndoDelta(string name, float duration, int amount = 0, float amountf = 0, bool value = false)
	{
		yield return new WaitForSeconds(duration);
		DoDelta(name, amount, amountf, value);
	}

    //Specific kinds
    public void SetIntStat(string name, int value, float duration=0)
    {
        if (intstats.ContainsKey(name))
        {
            int old = intstats[name];
            intstats[name] = value;
            IntChanged?.Invoke(name);
            if (duration>0)
                StartCoroutine(UndoDelta(name, duration, old - value));
        }
	}
    public void SetFloatStat(string name, float value, float duration=0)
    {
        if (floatstats.ContainsKey(name)) 
        { 
            float old = floatstats[name];
            floatstats[name]=value; 
            FloatChanged?.Invoke(name);
            if (duration > 0)
                StartCoroutine(UndoDelta(name, duration, 0, old - value));
        }
    }
    //bool can just use DoDelta, but this is here in case
    public void SetBoolStat(string name, bool value, float duration=0)
    { 
        if (boolstats.ContainsKey(name))
        {
            bool old = boolstats[name];
        }
    }

    /////Getters/////
	public int GetInt(string name)
    {
        if (intstats.ContainsKey(name))
            return intstats[name];
        return 0;
    }
    public float GetFloat(string name) 
    { 
        if (floatstats.ContainsKey(name))
            return floatstats[name];
        return 0;
    }
    public bool GetBool(string name)
    {
        if (boolstats.ContainsKey(name))
            return boolstats[name];
        return false;
    }
    public GameObject GetObject(string name)
    {
        if (objects.ContainsKey(name)) 
            return objects[name];
        return null;
    }
    public AudioClip GetSound(string name)
    {
        if (sounds.ContainsKey(name))
            return sounds[name];
        return null;
    }

    /////Adding New Stats/////
    public void AddStat(string name, int value=0,float valuef=0,bool valueb=false)
    {
        if (value!=0&&!intstats.ContainsKey(name))
            intstats.Add(name, value);
        else if (valuef!=0&&!floatstats.ContainsKey(name))
            floatstats.Add(name, valuef);
        else if (!boolstats.ContainsKey(name))
            boolstats.Add(name, valueb);
    }

    //Adding new sounds and gameobjects, and removing them too I guess
}
