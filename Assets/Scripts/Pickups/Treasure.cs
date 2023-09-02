using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    TreasureManager TM;

    [SerializeField] Material[] Materials;

    // Start is called before the first frame update
    void Start()
    {
        TM=FindFirstObjectByType<TreasureManager>();
    }

	void OnDisable()
	{
        TM.DeactivateTreasure(gameObject);
	}
}
