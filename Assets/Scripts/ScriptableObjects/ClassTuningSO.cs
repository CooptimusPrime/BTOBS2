using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTuningSO : ScriptableObject
{
	/*
	///Captain///
	WeaponP = Assault Rifle (2)
	WeaponS = Pistol
	WeaponB = Grenade Launcher (2)
	Max Health = 80
	Speed = 1X
	Passive = Increases the damage and reload speed by 10% of allies within 10m radius
	Active = Reinforcements

	///Engineer///
	WeaponP = Shotgun (2)
	WeaponS = Pistol
	WeaponB = Rocket Launcher (2)
	Max Health = 90
	Speed = 0.9X
	Passive = Player kills reduce the cooldown of the active skill
	Active = Turret

	///Medic///
	WeaponP = SMG (2)
	WeaponS = Pistol
	WeaponB = Laser Gun (2)
	Max Health = 110
	Speed = 1X
	Passive = After taking damage, will heal 5HP, or the damage taken, whichever is less, if not hurt for 5 seconds)
	Active = Stim Gun (Starts 2HP/ sec regen on target lasts 10 sec)

	///Renegade///
	WeaponP = Pistol
	WeaponS = Pistol
	WeaponB = Magnum
	Max Health = 100
	Speed = 1.1X
	Passive = 5% dodge chance, if successful, speed becomes 1.3X for 10 sec
	Active = Unload (unloads the whole clip at once in each gun held)

	///Sharpshooter///
	WeaponP = Sniper Rifle (2)
	WeaponS = Explosive Dart Gun
	WeaponB = Railgun (2)
	Max Health = 60
	Speed = 1.2X
	Passive = 
	Active =

	///Vanguard///
	WeaponP = Sword
	WeaponS = Throwing Knives
	WeaponB = Shield
	Max Health = 120
	Speed = 1.1X
	Passive = Heal 3HP per kill
	Active = Create a new shield, or repair the current one to full health
	*/

	public GameObject weaponp, weapons, weaponb;
	public int maxhealth;
	public float speed;
}
