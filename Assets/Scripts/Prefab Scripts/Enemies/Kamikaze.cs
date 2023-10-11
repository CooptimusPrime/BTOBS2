using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
	[SerializeField] GameObject HandL;
	[SerializeField] GameObject HandR;
	[SerializeField] GameObject explosion;

	[SerializeField] TuningSO tuning;
	TuningHolder holder;
	Explosive explosive;

	void Start()
	{
		HandL.SetActive(true);
		HandR.SetActive(true);

		holder=gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);

		gameObject.AddComponent<Enemy>();

		explosive = gameObject.AddComponent<Explosive>();
		explosive.SetExplosionPrefab(explosion);
	}

	void OnEnable()
	{
		HandL.SetActive(true);
		HandR.SetActive(true);
	}

	void Update()
	{
		if (!HandL.activeInHierarchy || !HandR.activeInHierarchy)
			Explode();
	}

	public void Explode()
	{
		GetComponent<Explosive>().Explode(this.gameObject,30);
	}
}
