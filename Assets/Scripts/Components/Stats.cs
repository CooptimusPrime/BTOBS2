using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    Dictionary<string,float> floatstats = new Dictionary<string,float>();
    Dictionary<string,int> intstats = new Dictionary<string,int>();
    Dictionary<string,bool> boolstats = new Dictionary<string,bool>();

    [SerializeField] WeaponTuningSO weptuning;
    [SerializeField] ClassTuningSO classtuning;
    [SerializeField] EnemyTuningSO enemytuning;
    //[SerializeField] PickupTuningSO pickuptuning;
    //[SerializeField] AllyTuningSO allytuning;

	public void DoDelta(string name, float duration, int amount=0, float amountf=0,bool value=false) 
    {
        if (amount!=0)
        {
            if (intstats.ContainsKey(name))
            {
                intstats[name] += amount;
                if (duration>0)
                {
                    StartCoroutine(UndoDelta(name, duration, amount));
                }
            }
        }
        else if (amountf!=0) 
        {
            if (floatstats.ContainsKey(name))
            {
                floatstats[name] += amount;
                if (duration>0)
                {
                    StartCoroutine(UndoDelta(name, duration, 0, amountf));
                }
            }
        }
        else
        {
            if (boolstats.ContainsKey(name))
            {
                boolstats[name] = value;
                if (duration>0)
                {
                    StartCoroutine(UndoDelta(name, duration, 0, 0, value));
                }
            }
        }
    }

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

    public void AddStat(string name, int value=0,float valuef=0,bool valueb=false)
    {
        if (value!=0&&!intstats.ContainsKey(name))
            intstats.Add(name, value);
        else if (valuef!=0&&!floatstats.ContainsKey(name))
            floatstats.Add(name, valuef);
        else if (!boolstats.ContainsKey(name))
            boolstats.Add(name, valueb);
    }

    IEnumerator UndoDelta(string name, float duration, int amount = 0, float amountf = 0, bool value=false)
    {
        yield return new WaitForSeconds(duration);
		if (amount != 0)
		{
			if (intstats.ContainsKey(name))
				intstats[name] -= amount;
		}
		else if (amountf != 0)
		{
			if (floatstats.ContainsKey(name))
				floatstats[name] -= amount;
		}
		else
		{
			if (boolstats.ContainsKey(name))
				boolstats[name] = !value;
		}
	}
}
