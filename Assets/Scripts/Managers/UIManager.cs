using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//will need to redo this to run on events instead of being hardcoded
//buffs need to moved to their own component;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas HUD;
	[SerializeField] TMP_Text Health, Score, AmmoL, AmmoR;
	[SerializeField] Image Heart, ReloadL, ReloadR, inf, inv, keyblue, keyblack, keyyellow, keywhite, keypurple;
	[SerializeField] Image[] statusimages = new Image[6];
	[SerializeField] TMP_Text[] statustimers = new TMP_Text[6];

	[SerializeField] Canvas GameOver;
	[SerializeField] TMP_Text Min, Sec, FinalScore, BestMin, BestSec, BestScore;


	[SerializeField] Weapon wepl, wepr; //Remove this when you update the system to use events and get this automatically
	bool isinv = false,
	isinf = false,
	blue = false,
	black = false,
	purple = false,
	white=false,
	yellow=false;

	public void FireL(GameObject bullet)
	{
		AmmoL.SetText(wepl.GetAmmo().ToString());
	}
	public void StartReloadL()
	{
		AmmoL.gameObject.SetActive(false);
		ReloadL.gameObject.SetActive(true);
	}
	public void FinishReloadL()
	{
		ReloadL.gameObject.SetActive(false);
		AmmoL.gameObject.SetActive(true);
		AmmoL.text = wepl.GetAmmo().ToString();
	}

	public void FireR() 
	{
		AmmoR.SetText(wepr.GetAmmo().ToString());
	}
	public void StartReloadR()
	{
		AmmoR.gameObject.SetActive(false);
		ReloadR.gameObject.SetActive(true);
	}
	public void FinishReloadR()
	{
		ReloadR.gameObject.SetActive(false);
		AmmoR.gameObject.SetActive(true);
		AmmoR.text = wepl.GetAmmo().ToString();
	}

	public void DoBuff(string name, float duration)
	{
		if (name=="inv")
		{
			if (isinv)
				StopCoroutine("FadeInv");
			StartCoroutine(FadeInv(duration));
		}
		else
		{
			if (isinf)
				StopCoroutine("FadInf");
			StartCoroutine(FadeInf(duration));
		}
	}

	public void AddKey(string colour)
	{
		if (colour == "blue")
		{
			keyblue.color = Color.white;
			blue = true;
		}
		else if (colour == "black")
		{
			keyblack.color = Color.white;
			black = true;
		}
		else if (colour == "purple")
		{
			keypurple.color = Color.white;
			purple = true;
		}
		else if (colour == "white")
		{
			keywhite.color = Color.white;
			white = true;
		}
		else
		{
			keyyellow.color = Color.white;
			yellow = true;
		}
	}
	public void RemoveKey(string colour) 
	{
		Color c = Color.white;
		c.a = 0;
		if (colour == "blue")
		{
			keyblue.color = c;
			blue = false;
		}
		else if (colour == "black")
		{
			keyblack.color = c;
			black = false;
		}
		else if (colour == "purple")
		{
			keypurple.color = c;
			purple = false;
		}
		else if (colour == "white")
		{
			keywhite.color = c;
			white = false;
		}
		else
		{
			keyyellow.color = c;
			yellow = false;
		}
	}
	public bool CheckKey(string colour)
	{
		if (colour == "blue" && blue == true)
			return true;
		else if (colour == "black" && black == true)
			return true;
		else if (colour == "purple" && purple == true)
			return true;
		else if (colour == "yellow" && yellow == true)
			return true;
		else if (colour == "white"&& white==true)
			return true;
		else 
			return false;
	}

	IEnumerator FadeInv(float duration)
	{
		isinv = true;
		float end = Time.time + duration;
		Color color = inv.color;

		while (end > Time.time)
		{
			yield return new WaitForSeconds(0.3f);
			color.a = (end - Time.time) / duration;
		}
		isinv = false;
	}

	IEnumerator FadeInf(float duration)
	{
		isinf = true;
		float end = Time.time + duration;
		Color color = inf.color;

		while (end > Time.time)
		{
			yield return new WaitForSeconds(0.3f);
			color.a = (end - Time.time) / duration;
		}
		isinf = false;
	}

	//GameManager currently handles score, but it should be here
}
