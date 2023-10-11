using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//want to add support for specific loot tables and also needs the ability to change loot and the chances
public class LootDropper : MonoBehaviour
{   
    float lootchance;
    int score=0;

    GameManager GM;
    PickupManager PM;
    EnemyManager EM;
    Stats stats;

	void Start()
	{
		GM = FindFirstObjectByType<GameManager>();
        PM = FindFirstObjectByType<PickupManager>();
        EM = FindFirstObjectByType<EnemyManager>();
        stats = GetComponent<Stats>();
        lootchance = stats.GetFloat("lootchance");
        score = stats.GetInt("score");
	}

	public void DropLoot()
    {
        GM.AddScore(score);
        PM.SpawnPickup(lootchance, transform.position);
        if (GetComponent<Enemy>())
            EM.Deactivate(gameObject);
        else
            gameObject.SetActive(false);
    }
}
