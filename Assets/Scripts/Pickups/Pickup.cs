using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    public float lifespan=60f;
    float currentlife;
    public bool active = true;
    static int spinspeed = 90;

    [SerializeField] AudioClip clip;
    [SerializeField] float volume=1;

    //probably some kind of pickup event?

    void OnEnable()
    {
        currentlife = 0;
    }

    void Update()
    {
		currentlife += Time.deltaTime;

		if (currentlife > lifespan)
			gameObject.SetActive(false);
		else
			transform.Rotate(spinspeed * Time.deltaTime, 0, 0);
	}

    public abstract void Apply(GameObject player);

    void OnCollisionEnter(Collision collision)
    {
        if (active&&collision.gameObject.CompareTag("player"))
        {
            Apply(collision.gameObject);
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<Player>().PlayAudio(clip, volume);
        }
    }
}
