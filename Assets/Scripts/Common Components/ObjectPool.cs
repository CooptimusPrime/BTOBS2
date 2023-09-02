using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public GameObject prefab;
	[SerializeField] int initial=10;

	List<GameObject> pool = new List<GameObject>();

	void Start()
	{
		for(int x=0; x<initial; x++)
		{
			Create();
		}
	}

	GameObject Create()
	{
		GameObject obj=Instantiate(prefab,transform.position,transform.rotation);
		obj.SetActive(false);
		pool.Add(obj);
		return obj;
	}

	public GameObject GetObject()
	{
		GameObject obj = pool.Find(x => x.activeInHierarchy == false);

		if (obj == null)
		{
			obj = Create();
		}

		obj.SetActive(true);
		return obj;
	}
}
