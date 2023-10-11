using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    Pickup pickup;
    Locked locked;
    TuningSO tuning;
    TuningHolder holder;
    Stats stats;

    [SerializeField] Material blue;
    [SerializeField] Material black;
    [SerializeField] Material purple;
    [SerializeField] Material yellow;
    [SerializeField] Material white;
    [SerializeField] Material unlocked;
    void Start()
    {
        holder = gameObject.AddComponent<TuningHolder>();
        holder.SetTuning(tuning);

        pickup = gameObject.AddComponent<Pickup>();
        pickup.OnPickedUp += DoEffect;
        //pickup.SetEffectName("treasure");

        stats = GetComponent<Stats>();
        locked = gameObject.AddComponent<Locked>();
    }

    public void SetColour(string key) //treasure manager calls this when spawning
    {
        Material chosen=null;
        if (key=="blue")
            chosen = blue;
        else if (key=="black")
            chosen = black;
        else if (key=="purple")
            chosen = purple;
        else if (key=="yellow")
            chosen = yellow;
        else if (key=="white")
            chosen = white;

        if (chosen != null)
        {
            locked.colour = key;
            GetComponent<Renderer>().material = chosen;
            locked.unlocked = unlocked;
        }
    }

    public void DoEffect(GameObject target)
    {
		//invincibility
        target.GetComponent<Stats>().DoDelta("isinvincible", 0, 0, true, stats.GetFloat("duration"));
		if (TryGetComponent(out UIManager manager))
			manager.DoBuff("inv", stats.GetFloat("duration"));

        //infinite
		bool boosted = false;

		WeaponHolder wepholder = target.gameObject.GetComponent<WeaponHolder>();
		Weapon[] weps = wepholder.GetWeapons();
		for (int i = 0; i < weps.Length; i++)
		{
			if (weps[i] != null)
			{
				weps[i].gameObject.GetComponent<Stats>().SetIntStat("consumption", 0, stats.GetFloat("duration"));
				boosted = true;
			}
		}
		if (boosted)
			manager.DoBuff("inv", stats.GetFloat("duration"));

		//health
		target.GetComponent<Health>().GetHealed(gameObject, GetComponent<Stats>().GetInt("intstrength"));
	}
}
