using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
	int MaxEnemies = 20;
	int Enemies = 0;

	public int Intervals;

	//Static spawn chances for enemies
	int BlockerChance = 10;
	int BomberChance = 15;
	int GunnerChance = 45;
	int KamikazeChance = 15;
	int SniperChance = 15;

	[SerializeField] GameObject[] Spawners;
	[SerializeField] ObjectPool[] Pools;

	// Start is called before the first frame update
	void Start()
    {
		for (int i = 0; i < MaxEnemies; i++)
			SpawnEnemy();

		Enemies = 20;
	}

    // Update is called once per frame
    void Update()
    {
		MaxEnemies = Mathf.Clamp(20 + 5 * Intervals, 20, 100);

		//if not at max enemies, spawn more
		if (Enemies < MaxEnemies)
		{
			SpawnEnemy();
			Enemies++;
		}
	}

	void SpawnEnemy()
	{
		GameObject obj;
		int choice = Random.Range(1, 101);
		if (choice <= BlockerChance)
			obj = Pools[0].GetObject();
		else if (choice <= BlockerChance + BomberChance)
			obj = Pools[1].GetObject();
		else if (choice <= BlockerChance + BomberChance + GunnerChance)
			obj = Pools[2].GetObject();
		else if (choice <= BlockerChance + BomberChance + GunnerChance + KamikazeChance)
			obj = Pools[3].GetObject();
		else
			obj = Pools[4].GetObject();

		int spawner = Random.Range(0, Spawners.Length);
		GameObject chosen = Spawners[spawner];
		obj.transform.SetPositionAndRotation(chosen.transform.position, chosen.transform.rotation);
	}

	public void Deactivate(GameObject obj)
	{
		obj.GetComponent<NavMeshAgent>().isStopped = true;
		SpawnEnemy();
		obj.SetActive(false);
	}
}
