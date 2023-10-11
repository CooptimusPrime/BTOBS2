using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] TuningSO tuning;
	TuningHolder holder;
	[SerializeField] GameObject Shield;
    
	void OnEnable()
    {
        Shield.SetActive(true);
    }

	// Start is called before the first frame update
	void Start()
	{
		holder = gameObject.AddComponent<TuningHolder>();
		holder.SetTuning(tuning);

		gameObject.AddComponent<Enemy>();
	}
}
