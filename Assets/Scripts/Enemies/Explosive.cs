using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] static int range = 2;
    public int damage;
    [SerializeField] GameObject Explosion;

    public void Explode(int damage)
    {
        GameObject inst=Instantiate(Explosion,transform.position,Quaternion.identity);
        inst.GetComponent<Explosion>().dmg = damage;
    }
}
