using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to update this to be customizable 
//Combine Explosive and Explosion -- Explosion should just be the visuals

public class Explosive : MonoBehaviour
{
    [SerializeField] static int range = 2;
    public int damage;
    GameObject explosion;

	public void Explode(GameObject source, int damage)
    {
        GameObject inst=Instantiate(explosion,transform.position,Quaternion.identity);
        inst.GetComponent<Explosion>().source = source;
        inst.GetComponent<Explosion>().dmg = damage;
    }

    public void SetExplosionPrefab(GameObject prefab)
    {
        explosion = prefab;
    }
}
