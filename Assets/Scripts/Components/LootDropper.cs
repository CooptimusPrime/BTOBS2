using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour
{
    public int LootChance;
    public int Score;

    GameManager GM;
    PickupManager PM;

	private void Start()
	{
		GM=FindFirstObjectByType<GameManager>();
	}

	public void DropLoot()
    {
        GM.AddScore(Score);
        PM.SpawnPickup(LootChance, transform.position);
        gameObject.SetActive(false);
    }
}
