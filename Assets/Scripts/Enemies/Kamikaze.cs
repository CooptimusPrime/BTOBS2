using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
	[SerializeField] GameObject HandL;
	[SerializeField] GameObject HandR;

	void Start()
	{
		HandL.SetActive(true);
		HandR.SetActive(true);
	}

	void OnEnable()
	{
		HandL.SetActive(true);
		HandR.SetActive(true);
	}

	private void Update()
	{
		if (!HandL.activeInHierarchy || !HandR.activeInHierarchy)
			Explode();
	}

	public void Explode()
	{
		GetComponent<Explosive>().Explode(20);
	}
}
