using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] GlobalSO globalso;


    Dictionary<string, int> stats = new Dictionary<string, int>()
    {
        {"playerid",0}, //static once set
        {"classid",0}, //static once set
        {"maxhealth",0}, //set based on class, but open to modification from future features like buffs and debuffs
        {"health",0}, //UI managers (all players need to know each other's health)
        {"score",0}, //UI manager (just this player), scoreboard
        {"kills",0}, //same
        {"assists",0}, //same
        {"bombers",0}, //just for personal stats at the end
        {"blockers",0}, //same
        {"gunnerturrets",0}, //same
        {"gunners",0}, //same
        {"kamikazes",0}, //same
        {"laserturrets",0}, //same
        {"snipers",0}, //same
        {"pickups",0}, //same
        {"healthp",0}, //same
        {"invp",0}, //same
        {"infp",0}, //same
        {"treasures",0}, //same
        {"shots",0}, //used to calculate accuracy, which is shown at the end
        {"hits",0}, //same
    };

    string[] resets = {
        "score",
        "kills",
        "assists",
        "bombers",
        "blockers",
        "gunnerturrets",
        "gunners",
        "kamikazes",
        "laserturrets",
        "snipers",
        "pickups",
        "healthp",
        "invp",
        "infp",
        "treasures",
        "shots",
        "hits",
    };

    //Live references to the weapons they have
    public GameObject weaponp, weapons, weaponb; 

    //Stats for end game
    float accuracy;

    public void SetClass(int id)
    {
        stats["classid"] = id;
        stats["maxhealth"] = 0;
    }

    public void Start()
    {
        stats["health"] = stats["maxhealth"];

        for (int i = 0; i<resets.Length; i++) 
        {
            stats[resets[i]] = 0;
        }
    }

    public void DoDelta(string parameter, int amount)
    {
        string search=parameter.ToLower();
        if (search == "playerid" || search == "classid")
            return;
        stats[search] += amount;
        //update your listeners
    }
}
