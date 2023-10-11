using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /////References/////
    Stats stats;
    NavMeshAgent agent;
    LootDropper dropper;
    WeaponHolder holder;
    Health health;
    CombatAI combat;
    
    Weapon wep;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>()??gameObject.AddComponent<AudioSource>();
        stats = gameObject.AddComponent<Stats>();
        health= gameObject.AddComponent<Health>();
        dropper = gameObject.AddComponent<LootDropper>();

        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.baseOffset = 1.25f;
        agent.acceleration = 50;
        agent.radius = 0.5f;
        agent.height = 1;
        agent.speed = stats.GetFloat("speed");

        combat = gameObject.AddComponent<CombatAI>();

        holder = gameObject.AddComponent<WeaponHolder>();
        wep = GetComponentInChildren<Weapon>(); //means this will only support one weapon for now
        holder.SetWeaponInSlot("p", wep);

        gameObject.tag = "Enemy";
    }
}
