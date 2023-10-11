using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//needs to be more generic, right now is just made for the pickups

public class Locked : MonoBehaviour
{
    public Material unlocked; //set by prefab since it is always green
    public string colour; //set by TreasureManager when spawning

    Pickup pickup;
    SphereCollider sphere;

    void Start()
    {
        sphere =gameObject.AddComponent<SphereCollider>();
        sphere.isTrigger = true;
        sphere.radius = 5;

        pickup=GetComponent<Pickup>();
        pickup.SetLocked(true);

        GetComponent<Stats>().DoDelta("isinvincible", 0, 0, true);
    }

	void OnTriggerEnter(Collider other)
	{
        //check if they have the key
        if (other.gameObject.GetComponent<Player>()&&other.gameObject.GetComponent<UIManager>().CheckKey(colour))
        {
            other.gameObject.GetComponent<UIManager>().RemoveKey(colour);
            gameObject.GetComponent<Renderer>().material = unlocked;
            pickup.SetLocked(false);
        }
	}
}
