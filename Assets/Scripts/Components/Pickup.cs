using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Need to come back to this to handle the UI better
//Also would prefer we add Lock directly from in here instead of on the prefab
public class Pickup : MonoBehaviour
{
    /////References/////
    Stats stats;
    Image image;

    /////Component Variables/////
    static int spinspeed = 90;
    string effectname="";
    bool locked = false;

    /////Events/////
    public event Action<GameObject> OnPickedUp;

	/////Initialization/////
	void Start()
	{
		stats = GetComponent<Stats>() ?? gameObject.AddComponent<Stats>();
        gameObject.AddComponent<Health>();
        gameObject.AddComponent<Lifespan>();
	}

	/////Component Functions/////
	void Update()
    {
        transform.Rotate(spinspeed * Time.deltaTime,0,0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!locked&&collision.gameObject.CompareTag("player")) //add support for enemies and allies too
        {
            OnPickedUp.Invoke(collision.gameObject);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(stats.GetSound("pickedup"));
            if (effectname!="")
                //collision.gameObject.GetComponent<UIManager>().AddStatusEffect(image,stats.GetFloat("duration"),effectname);
            gameObject.SetActive(false);
        }
    }

    public void SetEffectName(string name)
    { 
        effectname = name.ToLower(); 
    }

    public void SetLocked(bool islocked)
    {
        locked = islocked;
        if (locked)
            stats.DoDelta("isinvincible", 0, 0, true);
        else
            stats.DoDelta("isinvincible", 0, 0, false);
    }
}
