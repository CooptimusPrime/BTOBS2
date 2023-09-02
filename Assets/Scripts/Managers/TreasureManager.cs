using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
	//Treasures
	int MaxTreasures = 3;
	int CurTreasures = 0;
	float MinTreasureInterval = 30f;
	float MaxTreasureInterval = 150f;
	float ChosenInterval;
	float LastTreasureSpawn;

	ObjectPool Treasures;

	void Start()
    {
        ChooseNextSpawn();
    }

	void Update()
    {
        ChosenInterval-=Time.deltaTime;

		if ( ChosenInterval <= 0 && CurTreasures<MaxTreasures)
			SpawnTreasure();
    }

	void ChooseNextSpawn()
	{
		ChosenInterval = Random.Range(MinTreasureInterval, MaxTreasureInterval);
	}
	void SpawnTreasure()
	{
		//pick a spot and spawn the treasure and turrets
		//set the type
		//start the key spawn chance

		CurTreasures++;
	}
	
	public void DeactivateTreasure(GameObject treasure)
	{
		//destroy key
		//stop key spawn chance
		CurTreasures--;
	}
}
