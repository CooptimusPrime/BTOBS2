using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
	/////References/////
	Weapon weaponp;
	Weapon weapons;
	Weapon weaponb;

	/////Component Functions/////
	public void SetWeaponInSlot(string slot, Weapon wep)
	{
		if (slot=="p")
			weaponp = wep;
		else if (slot=="s")
			weapons = wep;
		else 
			weaponb = wep;
	}

	public Weapon GetWeaponInSlot(string slot)
	{
		if (slot == "p")
			return weaponp;
		else if (slot == "s")
			return weapons;
		else 
			return weaponb;
	}

	public Weapon[] GetWeapons()
	{
		Weapon[] weps = new Weapon[3];
		weps[0] = weaponp;
		weps[1] = weapons;
		weps[2] = weaponb;
		return weps;
	}
}
