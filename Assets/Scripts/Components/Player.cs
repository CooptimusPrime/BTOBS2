using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Revisit to finish off the rest of the systems

public class Player : MonoBehaviour
{
    /////References/////
    AudioSource source;
    Stats stats;
    Health health;
    WeaponHolder holder;
    UIManager ui;
    //PlayerShooting is currently just on the player prefab, but should be handled here

    void Start()
    {    
        source = gameObject.AddComponent<AudioSource>();
        source.spatialBlend = 1;
        source.loop = true;
        
        stats = gameObject.AddComponent<Stats>();
        health = gameObject.AddComponent<Health>();

        source.clip = stats.GetSound("background");
        source.Play();

        //UiManager right now is already on the object
        ui = GetComponent<UIManager>();
    }
}
