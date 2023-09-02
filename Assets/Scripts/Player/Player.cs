using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //these two set from the GameManager based on the GlobalSO
    public int id;
    public ClassTuningSO classso;

    Dictionary<string, int> stats = new Dictionary<string, int>()
    {
        {"score",0}, //UI manager (just this player), scoreboard
        {"kills",0}, //same
        {"assists",0}, //same
        {"bombers",0}, //just for personal stats at the end
        {"blockers",0}, //same
        {"gunnerturrets",0}, //same
        {"gunners",0}, //same
        {"kamikazes",0}, //same
        {"laserturrets",0}, //same
        {"snipers",0}, //same
        {"pickups",0}, //same
        {"healthp",0}, //same
        {"invp",0}, //same
        {"infp",0}, //same
        {"treasures",0}, //same
        {"shots",0}, //used to calculate accuracy, which is shown at the end
        {"hits",0}, //same
    };

    GameObject weaponp, weapons, weaponb;
    //need something for the weapon holsters
    float speed, cooldown, accuracy;
    //function or event for the passives and actives
    
    AudioSource effects;
    AudioSource bgm;

    void Start()
    {
        effects=gameObject.AddComponent<AudioSource>();
        //set it to not play on awake, and any other settings you need
        weaponp = Instantiate(classso.weaponp);
        weapons = Instantiate(classso.weapons);
        weaponb = Instantiate(classso.weaponb);
        //passive
        //active
    }

    public void PlayAudio(AudioClip clip, float scale)
    {
        effects.PlayOneShot(clip, scale);
    }

	public void DoDelta(string parameter, int amount)
	{
		string search = parameter.ToLower();
		if (search == "playerid" || search == "classid")
			return;
		stats[search] += amount;
	}
}
