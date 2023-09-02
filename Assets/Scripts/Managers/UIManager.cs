using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas HUD;
	[SerializeField] TMP_Text Health, Score, AmmoL, AmmoR;
	[SerializeField] Image Heart, Inf, Inv, Key1, Key2, Key3, ReloadL, ReloadR;

	[SerializeField] Canvas GameOver;
	[SerializeField] TMP_Text Min, Sec, FinalScore, BestMin, BestSec, BestScore;

    PlayerSO PlayerSO;

	// Start is called before the first frame update
	void Start()
    {
        
    }
}
